using System;
using System.Linq;
using FaithProject.FTU_101.FaithStruct.ControlDomain;
using Model;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame
{
    /// <summary>
    /// 长帧解析类
    /// </summary>
    internal class AnalysisLongFrameBase : AnalysisFrameBase
    {
        /// <summary>
        /// 根据硬件上行报文构建帧对象
        /// </summary>
        /// <param name="faith101"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public AnalysisLongFrameBase(Faith101Base faith101, byte[] message)
        {
            Buf = message;
            var linkAdd = Faith101Base.GetByteWeiZhi(EnumsInfo.FrameStruct.LinkAddress, faith101);
            var pubAdd = Faith101Base.GetByteWeiZhi(EnumsInfo.FrameStruct.PublicAddress, faith101);
            var cotCount = Faith101Base.GetByteWeiZhi(EnumsInfo.FrameStruct.CotCode, faith101);
            var messageBody = new byte[message.Length - 7 - linkAdd[0]];
            Array.Copy(message, 5 + linkAdd[0], messageBody, 0, messageBody.Length);
            AsduCode = new Asdu(messageBody.ToList(), pubAdd[0], cotCount[0]);
            MsgLengthCode = message[1];
            //获得控制域对象
            ControlDomain = new UpControlDomain(message[4]);
            switch (linkAdd[0])
            {
                case 1:
                    LinkAddress[0] = message[linkAdd[1]];
                    break;
                case 2:
                    LinkAddress[0] = message[linkAdd[1]];
                    LinkAddress[1] = message[linkAdd[1] + 1];
                    break;
            }
            //WriteDeviceFrameLog(this.LinkAddress, message);
        }

        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource()
        {
            Buf = null;
            AsduCode = null; 
        }
    }
}
