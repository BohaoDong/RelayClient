using FaithProject.FTU_101.FaithStruct.ControlDomain;
using Model;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame
{
    /// <summary>
    /// 短帧解析类
    /// </summary>
    public class AnalysisShortFrameBase : AnalysisFrameBase
    {  
        /// <summary>
        /// 根据硬件上行报文构建帧对象
        /// </summary>
        /// <param name="faith101"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public AnalysisShortFrameBase(Faith101Base faith101, byte[] message) 
        {
            Buf = message;
            int[] linkAdd = Faith101Base.GetByteWeiZhi(EnumsInfo.FrameStruct.LinkAddress, faith101);
            //获得短帧控制域对象
            ControlDomain = new UpControlDomain(message[1]);
            switch (linkAdd[0])
            {
                case 1:
                    LinkAddress[0] = message[2];
                    break;
                case 2:
                    LinkAddress[0] = message[2];
                    LinkAddress[1] = message[3];
                    break;
            } 
        }
        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource()
        {
            Buf = null;
            ControlDomain = null;
        }
    }
}
