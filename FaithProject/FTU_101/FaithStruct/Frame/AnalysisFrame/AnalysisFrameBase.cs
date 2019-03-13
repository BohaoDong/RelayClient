using FaithProject.FTU_101.FaithStruct.ControlDomain;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame
{
    public class AnalysisFrameBase : FrameBase
    {
        /// <summary>
        /// 上行控制域
        /// </summary>
        public UpControlDomain ControlDomain
        {
            get;
            set;
        }


        public override void MyDispose()
        {
            ControlDomain = null;
        }
    }
}
