#include "main_workflow.h"
#include <iostream>
#include <fstream>

int main(int argc, char** argv) {
	std::shared_ptr<QTongCoreCVWorkFlow> engine(new QTongCoreCVWorkFlow());
	bool succ = engine->initWorkFlow("D:/pills.vm", "c6579f12b8fd4feca4a251d33de22f0c");
	if (!succ) {
		std::cout << "Could not load workflow file here..." << std::endl;
		return -1;
	}
	cv::Mat frame = cv::imread("D:/333.bmp");
	cv::imshow("input", frame);
	//cv::Vec4i data(20, 170, 610, 192);
	//cv::Mat frame = cv::imread("D:/kachi_circle.png");
	cv::namedWindow("OpenCV实验大师 C++工作流引擎演示", cv::WINDOW_AUTOSIZE);
	cv::Mat result;
	std::vector<std::string> logs;
	//cv::Vec4i data(255, 231, 140, -1);
	engine->run_workflow(frame, result, logs);
	cv::imshow("OpenCV实验大师 C++工作流引擎演示", result);
	cv::waitKey(0);
	cv::destroyAllWindows();
	return 0;
}