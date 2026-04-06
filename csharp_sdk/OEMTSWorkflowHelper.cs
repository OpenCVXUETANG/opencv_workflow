using System.Runtime.InteropServices;

namespace qiantong_oemst
{
    public class OEMTSWorkflowHelper
    {

        [DllImport("oemts_workflow_engine.dll")]
        public static extern IntPtr workflowClassInit2();  //工作流指针
        [DllImport("oemts_workflow_engine.dll")]
        public static extern void deleWorkflowInstance(IntPtr p);//删除工作流对象
        [DllImport("oemts_workflow_engine.dll")]
        public static extern bool loadVMConfigFile(IntPtr p, char[] path, char[] key);//加载到工作流（参数1 图像处理工作流路径，参数2：权限ID）
        [DllImport("oemts_workflow_engine.dll")]
        public static extern void runWorkflow(IntPtr p, IntPtr da, IntPtr dst, int w, int h);//运行工作流（参数 工作流对象，输入图像指针，输出图像指针，输出宽，高）
        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperContoursInfo(IntPtr p, [In, Out] ContourInfoWrapper[] contoursInfo);//获取轮廓信息
        public struct ContourInfoWrapper
        {
            public float area;
            public float arc;
            public float convexity;
            public float roundness;
            public float aspect_ratio;
            public float center_x;
            public float center_y;
            public float angle;
            public float outer_radius;
            public float outer_center_x;
            public float outer_center_y;
            public float axes_long;
            public float axes_short;
            public float box_x;
            public float box_y;
            public float box_width;
            public float box_height;
            public float min_box_width;
            public float min_box_height;
        };
        [DllImport("oemts_workflow_engine.dll")]
        public static extern void run_measure(IntPtr p, IntPtr da, IntPtr dst, int w, int h, int[] shape_info);//测量工具

        // 我新添加的SDK支持，你先整合上，下周跟你一起再调通一下！
        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperBlobInfos(IntPtr p, [In, Out] PointWrapper[] contoursInfo);//获取blob结果

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperDetectCircles(IntPtr p, [In, Out] CircleInfoWrapper[] contoursInfo);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperMeasureCircles(IntPtr p, [In, Out] CircleInfoWrapper[] contoursInfo);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperCornerDetInfos(IntPtr p, [In, Out] PointWrapper[] contoursInfo);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperDetectLineInfos(IntPtr p, [In, Out] LineInfoWrapper[] contoursInfo);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getWrapperMeasureLineInfos(IntPtr p, [In, Out] LineInfoWrapper[] contoursInfo);

        [DllImport("oemts_workflow_engine.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getWrapperMatchedBoxInfos(IntPtr p, [In, Out] TplBoxInfoWrapper[] boxes);

        [DllImport("oemts_workflow_engine.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getWrapperEdgeBoxInfos(IntPtr p, [In, Out] TplBoxInfoWrapper[] boxes);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getMeasureLinePts(IntPtr p, [In, Out] float[] line_info);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern void getMeasureCircleInfo(IntPtr p, [In, Out] float[] circle_info);


        // 先获取对应的数目以后，初始化数据的数组，再获取数据
        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfBlobs(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfDetectCircles(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfMeasureCircles(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfCorners(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfContours(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfDetectLines(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfMeasureLines(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfMatchedBoxes(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfEdgeBoxes(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int getNumOfMeasureLinePts(IntPtr p);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int findNinePointsOfCircles(IntPtr da, IntPtr dst, int w, int h, [In, Out] float[] pts, bool colOrder);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int calculateCamera2RobotH(float[] camera_pts, float[] robot_pts, [In, Out] float[] matrix);

        [DllImport("oemts_workflow_engine.dll")]
        public static extern int mapPointFromCamera2Robot(float[] camera_pt, float[] matrix, [In, Out] float[] robot_pt);


        public struct PointWrapper
        {
            int cx;
            int cy;
            int radius;
        };

        public struct TplBoxInfoWrapper
        {
            public float angle; // degree
            public float score;
            public int x;
            public int y;
            public int width;
            public int height;
            public float rrt_x1;
            public float rrt_y1;
            public float rrt_x2;
            public float rrt_y2;
            public float rrt_x3;
            public float rrt_y3;
            public float rrt_x4;
            public float rrt_y4;
        };

        public struct LineInfoWrapper
        {
            float x1;
            float y1;
            float x2;
            float y2;
            float dist;
        };

        public struct DetBoxInfoWrapper
        {
            int x;
            int y;
            int width;
            int height;
            float conf;
            int class_id;
        };

        public struct CircleInfoWrapper
        {
            float cx; // 圆心.x
            float cy; // 圆心.y
            float radius; // 半径
            float arc; // 周长
            float area; // 面积
            float roundness; // 圆度
        }
    }
}
