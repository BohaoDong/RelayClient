using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame;
using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData;
using Model;
//using EnumsInfo = Model.FaithInfo.EnumsInfo;

namespace FaithProject.FTU_101.Balance101
{
     public class FaithBalance101:Faith101Base
    {
         public FaithBalance101(int doorid,string devCode): base(doorid)
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
             SetLineCtrl(DevCode, DoorId);
             Info = new IpInfo();
             var shortMsg = new AnalysisShortFrameBase(this, bt);
             if (shortMsg.ControlDomain.Fc == 11 && bt[1] == 0x8B)
             {
                 DicBiaoShiInfo.dic[DevCode].ConnectConfirm = false; 
                 DicBiaoShiInfo.dic[DevCode].FirstConnect = false;
                 var other = new AnalysisOther(DevCode); 
                 Info = new IpInfo {Buf = other.SynthesisShotMsg(BaseHelper.s_ResetRemoteLink)};
                 other.DisPoseResource();
                 OnSessionReceive_ReplyDevice(Info); 
             }  
             else if (shortMsg.ControlDomain.Fc == 0 && bt[1] == 0x80) //接收确认帧,在此分析就是为了界面显示
             {
                 //判断是否已经第一次上电连接成功
                 if (!DicBiaoShiInfo.dic[DevCode].FirstConnect)
                 {
                     DicBiaoShiInfo.dic[DevCode].FirstConnect = true; 
                     Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.Balance);
                     //终端第一次上电需要上这一帧--初始化结束(初始化) 
                     OnSessionReceive_ReplyDevice(Info); 
                 }
                 else if (DicBiaoShiInfo.dic[DevCode].ZhaoHuan) //总招开始
                 { 
                     Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.First); //合成报文返回一级数据_如果数据都发送完全就发送激活终止报文 
                     OnSessionReceive_ReplyDevice(Info);
                     if (DicBiaoShiInfo.dic[DevCode].YxTrue == false && DicBiaoShiInfo.dic[DevCode].YcTrue == false)
                     {
                         DicBiaoShiInfo.dic[DevCode].ZhaoHuan = false;//总招结束
                     }
                 }
                 DisPoseResource(ref shortMsg);
                 Info.DisPoseResource(); 
             }
         }

         /// <summary>
         ///  长帧报文分析处理
         /// </summary>
         /// <param name="bt"></param>
         protected override void LongFrameHandler(byte[] bt)
         {
             Info = new IpInfo();
             var other = new AnalysisOther(DevCode);
             SetLineCtrl(DevCode, DoorId);
             var alf = new AnalysisLongFrameBase(this, bt);
             //总招激活
             if (alf.AsduCode.Ti == (byte) TypeIdentification.C_IC_NA_1 && alf.AsduCode.Cot.GetCotCode == 0x06)
                 //&& alf.ASDUCode.InfoBody[2] == 0x14 加上了组召唤
             {
                 if (IsHaveFirstData()) //判断是否有一级数据 DevCode传空不做标示
                 {
                     DicBiaoShiInfo.dic[DevCode].ZhaoHuan = true;
                     Info.Buf = other.SynthesisShotMsg(0x00); //合成有一级数据的报文,有回应有一级数据10 20 01 00 21 16
                     OnSessionReceive_ReplyDevice(Info);
                     Info.Buf = SynthesisLongMsg(EnumsInfo.DataType.ZongZhaoJH);
                     OnSessionReceive_ReplyDevice(Info);
                 }
                 else
                 {
                     Info.Buf = other.SynthesisShotMsg(0x00);
                     other.DisPoseResource();
                     OnSessionReceive_ReplyDevice(Info);
                 }
             }
             DisPoseResource(ref alf);
             Info.DisPoseResource();
             other.DisPoseResource();
         }
    }
}
