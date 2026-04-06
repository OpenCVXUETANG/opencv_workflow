#include "main_workflow.h"
#include <iostream>
#include <fstream>"

int main(int argc, char** argv) {
    // load opencv workflow sdk
    std::shared_ptr<QTongCoreCVWorkFlow> engine(new QTongCoreCVWorkFlow());
    bool succ = engine->initWorkFlow("D:/python/opencv_workflow/qtong_caliper.vm", 
                                                "c6579f12b8fd4feca4a251d33de22f0c");
    if (!succ) {
        std::cout << "Could not load workflow file here..." << std::endl;
        return -1;
    }
    //  image view
    cv::Mat frame = cv::imread("D:/images/vm_test/caliper_test.png");
    cv::imshow("OpenCV学堂-卡尺演示", frame);

    // measure with caliper sdk
    cv::Mat result;
    cv::Vec4i data(291, 254, 118, -1);
    engine->run_measure(frame, result, data);

    // query measure circle information
    cv::Vec3i cinfo = engine->getMeasureCircleInfo();
    std::cout << "圆心 X: " << cinfo[0] <<" 圆心 Y: "<<cinfo[1]<< std::endl;
    std::cout << "圆半径: " << cinfo[2] << std::endl;

    cv::imshow("OpenCV学堂-卡尺效果", result);
    cv::waitKey(0);
    cv::destroyAllWindows();
    return 0;
}
