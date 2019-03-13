using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame;
using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData;
using Model; 

namespace FaithProject.FTU_101.NoBalance101
{
    /// <summary>
    /// 非平衡式101协议基类
    /// </summary>
    public class FaithNoBalance101 : Faith101Base
    {
        public FaithNoBalance101(int doorid,string devCode)
            : base(doorid)
        {
            DevCode = devCode;
            switch (BaseHelper.LinkaddressLength)
            {
                case 1:
                    FrameShortLength = 5; 
                    IsBalanceFrame = false;
                    break;
                case 2:
                    FrameShortLength = 6; 
                    IsBalanceFrame = true;
                    break;
            }
        }
         
        /// <summary>
        /// 解析短帧报文
        /// </summary>
        protected override void ShortFrameHandler(byte[] bt)
        {
            Info = new IpInfo();
            SetLineCtrl(DevCode, DoorId);
            var shortMsg = new AnalysisShortFrameBase(this, bt);
            if (bt[1] != 0x6C)
            {
                if (shortMsg.ControlDomain.Fc == 9 && bt[1] == 0x49) //主站请求链路状态
                {
                    var other = new AnalysisOther(DevCode);
                    Info.Buf = other.SynthesisShotMsg(BaseHelper.s_ResetRemoteLinkState);
                    OnSessionReceive_ReplyDevice(Info);
                    other.DisPoseResource();
                }
                if (shortMsg.ControlDomain.Fc == 0)//复位远方链路
                {
                    var other = new AnalysisOther(DevCode);
                    DicBiaoShiInfo.dic[DevCode].ConnectConfirm = false;
                    DicBiaoShiInfo.dic[DevCode].FirstConnect = false; 
                    Info.Buf = other.SynthesisShotMsg(BaseHelper.s_ResetRemoteLinkOK);
                    OnSessionReceive_ReplyDevice(Info);
                    other.DisPoseResource();
                } 
                if (shortMsg.ControlDomain.Fc == 0x0A)//请求一级数据或者建立链路中需要的一帧
                {
                    if (!DicBiaoShiInfo.dic[DevCode].FirstConnect)// 68 0B 0B 68 08 01 46 01 04 01 00 00 00 56 16//初始化结束(初始化)（终端第一次上电需要上这一帧）
                    {
                        DicBiaoShiInfo.dic[DevCode].FirstConnect = true; 
                        Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.NoBalance);
                        OnSessionReceive_ReplyDevice(Info); 
                    }
                    else if (DicBiaoShiInfo.dic[DevCode].ZhaoHuan)//总招中的请求遥信遥测数据
                    {
                        if (DicBiaoShiInfo.dic[DevCode].ZongZhaoJh == false)
                        { 
                            Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.ZongZhaoJH);//（确认总召唤激活） "68 09 09 68 28 01 64 01 07 01 00 00 14 AA 16";// 
                            DicBiaoShiInfo.dic[DevCode].ZongZhaoJh = true;
                            OnSessionReceive_ReplyDevice(Info); 
                        }
                        else
                        { 
                            Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.First);
                            if (DicBiaoShiInfo.dic[DevCode].YxTrue == false && DicBiaoShiInfo.dic[DevCode].YcTrue == false)
                            {
                                DicBiaoShiInfo.dic[DevCode].ZhaoHuan = false;
                            }
                            OnSessionReceive_ReplyDevice(Info);
                        }
                    } 
                }
                //召唤二级数据
                else if (shortMsg.ControlDomain.Fc == 0x0B)
                {
                    //合成报文返回二级数据
                    Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.Second);
                    OnSessionReceive_ReplyDevice(Info); 
                }
            }
            Info.DisPoseResource();
            DisPoseResource(ref shortMsg);
        }
        /// <summary>
        ///  长帧报文分析处理
        /// </summary>
        /// <param name="bt"></param>
        protected override void LongFrameHandler(byte[] bt)
        {
            Info = new IpInfo();
            var other = new AnalysisOther(DevCode);
            var alf = new AnalysisLongFrameBase(this, bt); 
            //AnalyzerMessageType(alf);
            //总招激活
            if (alf.AsduCode.Ti == (byte)TypeIdentification.C_IC_NA_1 && alf.AsduCode.Cot.GetCotCode == 0x06)//&& alf.ASDUCode.InfoBody[2] == 0x14 加上了组召唤
            { 
                if (IsHaveFirstData())//判断是否有一级数据 DevCode传空不做标示
                {
                    DicBiaoShiInfo.dic[DevCode].ZhaoHuan = true;
                    DicBiaoShiInfo.dic[DevCode].ZongZhaoJh = false;
                    Info.Buf = other.SynthesisShotMsg(0x20);//合成有一级数据的报文,有回应有一级数据10 20 01 21 16 
                    OnSessionReceive_ReplyDevice(Info);
                }
                else
                {
                    Info.Buf = other.SynthesisShotMsg(0x00);//"10 00 01 21 16"; 总招激活没有数据发送0x00 
                    OnSessionReceive_ReplyDevice(Info);
                }
            }
            other.DisPoseResource();
            Info.DisPoseResource();
            DisPoseResource(ref alf);
        }
    }
}
