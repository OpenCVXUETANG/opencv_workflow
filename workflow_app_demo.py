import sys
from PyQt5.QtWidgets import QApplication, QMainWindow, QVBoxLayout, QWidget, QPushButton, QFileDialog, QLabel
from PyQt5.QtGui import QImage, QPixmap
import cv2 as cv
from vmcore.project_data_model import ProjectDataModel
from vmcore.project_persister import VMProjectPersister
from vmcore.image_process_task import ImageProcessTask

class MyWindow(QMainWindow):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("OCR SDK 工作流演示代码")
        self.setGeometry(100, 100, 800, 600)

        # 创建一个布局器
        layout = QVBoxLayout()

        # 创建一个QLabel用于显示图像
        self.image_label = QLabel()
        self.image_label.setScaledContents(True)  # 使标签适应窗口大小
        layout.addWidget(self.image_label)

        # 创建两个按钮
        self.image_button = QPushButton("选择图像文件")
        self.image_button.clicked.connect(self.select_image)
        layout.addWidget(self.image_button)

        self.text_button = QPushButton("选择工作流文件")
        self.text_button.clicked.connect(self.select_text)
        layout.addWidget(self.text_button)

        self.run_button = QPushButton("运行")
        self.run_button.clicked.connect(self.run_workflow)
        layout.addWidget(self.run_button)

        # 创建一个容器窗口并将布局器添加到其中
        container = QWidget()
        container.setLayout(layout)

        # 将容器窗口设置为主窗口的内容
        self.setCentralWidget(container)
        self.image_path = ""
        self.vm_path = ""

    def run_workflow(self):
        persister = VMProjectPersister()
        model = ProjectDataModel()
        project_type = persister.query_project_type(self.vm_path)
        if project_type == 1:
            persister.load(model, self.vm_path)
            print("exectue opencv workflow here......")
            log_txts = persister.silence_run_process(model, self.image_path)
            index = 0
            result_img = None
            for t in model.tasks:
                if isinstance(t, ImageProcessTask):
                    result_img = t.get_output_image()
                    index += 1

            if result_img is not None:
                h, w, ch = result_img.shape
                bytes_per_line = ch * w
                qt_image = QImage(result_img.data, w, h, bytes_per_line, QImage.Format_BGR888)
                pixmap = QPixmap.fromImage(qt_image)
                self.image_label.setPixmap(pixmap)

    def select_image(self):
        # 使用文件对话框选择图像文件
        file_dialog = QFileDialog()
        file_path, _ = file_dialog.getOpenFileName(self, "选择图像文件", "", "Images (*.png *.bmp *.jpg)")

        if file_path:
            # 读取选择的图像并显示在QLabel上
            self.image_path = file_path
            image = cv.imread(file_path)
            if image is not None:
                h, w, ch = image.shape
                bytes_per_line = ch * w
                qt_image = QImage(image.data, w, h, bytes_per_line, QImage.Format_BGR888)
                pixmap = QPixmap.fromImage(qt_image)
                self.image_label.setPixmap(pixmap)

    def select_text(self):
        # 使用文件对话框选择文本文件
        file_dialog = QFileDialog()
        file_path, _ = file_dialog.getOpenFileName(self, "保存文本文件", "", "Text Files (*.vm)")

        if file_path:
            self.vm_path = file_path

if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MyWindow()
    window.show()
    sys.exit(app.exec_())
