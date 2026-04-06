using OpenCvSharp;
using qiantong_oemst;

class OpenCVWorkFlowDemo
{
    static void Main(string[] args)
    {

        // 1. create intances
        IntPtr myInstance = OEMTSWorkflowHelper.workflowClassInit2();

        // 2. load workflow vm file
        bool succ = OEMTSWorkflowHelper.loadVMConfigFile(myInstance,
            "find_defect.vm".ToCharArray(),
            "c6579f12b8fd4feca4a251d33de22f0c".ToCharArray());

        // 3. run workflow with input image
        Mat src = Cv2.ImRead("4156.bmp");
        Mat dst = new Mat(src.Size(), src.Type());

        int w = src.Cols;
        int h = src.Rows;
        OEMTSWorkflowHelper.runWorkflow(myInstance, src.CvPtr, dst.CvPtr, w, h);

        Cv2.ImShow("OpenCV学堂-OpenCV C#工作流演示", dst);
        Cv2.WaitKey(0);
        Cv2.DestroyAllWindows();

        // 4. destory instance and others
        src.Dispose();
        dst.Dispose();
        OEMTSWorkflowHelper.deleWorkflowInstance(myInstance);
    }
}
