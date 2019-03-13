using System;
using System.Collections.Generic;
using FaithProject.FTU_101.FaithStruct.Frame;
using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame;
using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData;
using Model;

namespace FaithProject.FTU_101
{
    abstract public class Faith101Base : FaithBaseBll
    {
        protected Faith101Base(int doorid)
            : base(doorid)
        {

        }
        /// <summary>
        /// 报文应答类
        /// </summary>
        public IpInfo Info;
        ///// <summary>
        ///// 二级数据类
        ///// </summary>
        //public AnalysisSoe Soe;
        /// <summary>
        /// soe报文
        /// </summary>
        public byte[] MsgSoe;
        /// <summary>
        /// 待处理报文数据池
        /// </summary>
        private List<byte> _mMessageCache = new List<byte>();
        protected string MyLock = "myLock";
        //public AnalysisOther Other;
        /// <summary>
        /// 报文缓存集合
        /// </summary>
        protected List<byte> MessageCache
        {
            get { return _mMessageCache; }
            set { _mMessageCache = value; }
        }
        /// <summary>
        /// 控制域完整报文
        /// </summary>
        public byte LineCtrl { get; set; }

        /// <summary>
        /// 平衡或非平衡时的最短帧长度分别为6、5
        /// </summary>
        protected int FrameShortLength { get; set; }

        /// <summary>
        /// 接收硬件报文
        /// </summary> 
        /// <param name="buf"></param>
        public override void ReceiveDeviceData(byte[] buf)
        {
            #region

            lock (MyLock)
            {
                //把数组缓存到List中
                MessageCache.AddRange(buf);
                //短帧报文长度为5个字节
                while (MessageCache.Count >= FrameShortLength)
                {
                    //68开头 可变长帧
                    if (FrameBase.LongStartCode == MessageCache[0] && FrameBase.LongStartCode == MessageCache[3])
                    {
                        //获取报文长度
                        int msgLength = MessageCache[1];
                        //前4后2
                        if (MessageCache.Count >= msgLength + 6)
                        {
                            //定义从缓冲区接收报文的数组
                            byte[] data = new byte[msgLength + 6];
                            //将缓存区中的数组保存到定义的数组中
                            MessageCache.CopyTo(0, data, 0, data.Length);

                            //判断结束帧 0x16
                            if (FrameBase.LongEndCode == data[data.Length - 1])
                            {
                                //校验码验证
                                if (FrameBase.IsValidMessageCheckCode(data))
                                {
                                    //删除取出数据
                                    MessageCache.RemoveRange(0, data.Length);
                                    //长帧报文分析
                                    LongFrameHandler(data);
                                    //显示完整报文事件
                                    //OnSessionReceive_DisplayDevice(data, DevCode);
                                }
                                else //长帧校验码错误
                                {
                                    //删除取出数据
                                    MessageCache.RemoveRange(0, data.Length);
                                    //this.OnInformHanlder(this, new InformEventArgs(data, "校验码错误"));
                                }
                            }
                            else //长帧结束字符错误
                            {
                                MessageCache.RemoveRange(0, data.Length);
                                //this.OnInformHanlder(this, new InformEventArgs(data, "结束帧错误"));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (FrameBase.ShortStartCode == MessageCache[0])
                    {
                        byte[] data = new byte[FrameShortLength];
                        MessageCache.CopyTo(0, data, 0, data.Length);
                        if (FrameBase.ShortEndCode == data[data.Length - 1])
                        {
                            //判断校验码
                            if (FrameBase.IsValidMessageCheckCode(data))
                            {
                                //短报文分析
                                ShortFrameHandler(data);
                                //显示完整报文事件
                                //OnSessionReceive_DisplayDevice(data, DevCode);
                                //将已经取出的数据删除
                                MessageCache.RemoveRange(0, data.Length);
                            }
                            else
                            {
                                //将已经取出的数据删除
                                MessageCache.RemoveRange(0, data.Length);
                                //this.OnInformHanlder(this, new InformEventArgs(data, "校验码错误"));
                            }
                        }
                        else
                        {
                            MessageCache.RemoveRange(0, data.Length);
                            //this.OnInformHanlder(this, new InformEventArgs(data, "短针结束帧错误"));
                        }
                    }
                    else
                    {
                        //报文开头既不是0x68也不是0x10则删除第一个字节
                        MessageCache.RemoveAt(0);
                    }
                }
            }

            #endregion
        }
        /// <summary>
        /// 验证报文校验码
        /// </summary>
        /// <param name="message">校验的报文</param>
        /// <returns>校验码整齐</returns>
        public static bool IsValidMessageCheckCode(byte[] message)
        {
            //参数为null或元素为0时抛出一个异常
            if (message == null || message.Length <= 0) throw new Exception();

            byte checkCode = 0;
            //默认为短帧 校验码从第二位开始算起
            int index = 1;
            //长帧，从索引4开始（第五位）
            if (message.Length > 6) index = 4;
            for (int i = index; i < message.Length - 2; i++)
            {
                checkCode += message[i];
            }
            return checkCode == message[message.Length - 2];
        }

        /// <summary>
        ///  长帧报文处理
        /// </summary> 
        /// <param name="data"></param>
        abstract protected void LongFrameHandler( byte[] data);

        /// <summary>
        ///  短帧报文处理
        /// </summary> 
        /// <param name="data"></param>
        abstract protected void ShortFrameHandler(byte[] data);
         
        /// <summary>
        /// 根据要判断的字节类型及定义长度，判断其在长短帧中所在位置。0位放置长度 1位放置起始位置
        /// </summary> 
        /// <param name="fs"></param>
        /// <param name="fbb"></param>
        /// <returns></returns>
        public static int[] GetByteWeiZhi(EnumsInfo.FrameStruct fs, FaithBaseBll fbb)
        {
            int[] resulet = new int[2];
            switch (fs)
            {
                case EnumsInfo.FrameStruct.LinkAddress:
                    resulet[0] = fbb.LinkCount;
                    resulet[1] = 5;//固定一直是第五位
                    break; 
                case EnumsInfo.FrameStruct.PublicAddress:
                    resulet[0] = fbb.PublicLinkCount;
                    resulet[1] = 4 + fbb.LinkCount + fbb.CotCode + 3;
                    break;
                case EnumsInfo.FrameStruct.CotCode:
                    resulet[0] = fbb.CotCode;//传送原因占几个字节
                    resulet[1] = 4 + fbb.LinkCount + 3;//从第几位开始
                    break;
            }
            return resulet;
        }

        /// <summary>
        /// 分析报文类型
        /// </summary> 
        /// <param name="af"></param>
        protected void AnalyzerMessageType(AnalysisFrameBase af)
        {
            switch (af.AsduCode.Ti)
            {
                case (byte)TypeIdentification.M_SP_NA_1:
                case (byte)TypeIdentification.M_DP_NA_1:
                    //Device_YX(af); 
                    break; 
            }
        }

        /// <summary>
        /// 给对应的遥信遥测设置控制域
        /// </summary> 
        /// <param name="devCode"></param>
        /// <param name="doorId"></param>
        public void SetLineCtrl(string devCode, int doorId)
        {
            switch (doorId)
            {
                case 1: //非平衡101 
                    DicBiaoShiInfo.dic[devCode].LineCtrl = 0x28;
                    //switch (DicBiaoShiInfo.dic[devCode].LineCtrl)
                    //    {
                    //        case 0x28:
                    //            break;
                    //        case 0x08:
                    //            DicBiaoShiInfo.dic[devCode].LineCtrl = 0x28;
                    //            break;
                    //        default:
                    //            DicBiaoShiInfo.dic[devCode].LineCtrl = 0x08;
                    //            break;
                    //    }
                    break;
                case 2: //平衡101
                    switch (DicBiaoShiInfo.dic[devCode].LineCtrl)
                    {
                        case 0x73:
                            DicBiaoShiInfo.dic[devCode].LineCtrl = 0x53;
                            break;
                        case 0x53:
                            DicBiaoShiInfo.dic[devCode].LineCtrl = 0x73;
                            break;
                        default:
                            DicBiaoShiInfo.dic[devCode].LineCtrl = 0x53;
                            break;
                    }
                    break;
            }
        }

        #region 处理101数据
        /// <summary>
        /// 判断是否有一级数据,并存储一级数据
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFirstData()//string DevCode, int doorid
        {
            //判断是否有遥信遥测数据 
            var yx = new AnalysisYx(DevCode); 
            var yc = new AnalysisYc(DevCode);
            var MsgNum = 0;
            switch (DoorId)
            {
                case 1:
                    DicBiaoShiInfo.dic[DevCode].YxBaoWen = yx.SuccessMsg(DicBiaoShiInfo.dic[DevCode].LineCtrl, DevCode, DoorId, ref MsgNum);
                    DicBiaoShiInfo.dic[DevCode].YcBaoWen = yc.SuccessMsg(DicBiaoShiInfo.dic[DevCode].LineCtrl, DevCode, DoorId, ref MsgNum);
                    break;
                case 2:
                    DicBiaoShiInfo.dic[DevCode].LineCtrl = DicBiaoShiInfo.dic[DevCode].LineCtrl == (byte)0x73 ? (byte)0x53 : (byte)0x73;
                    DicBiaoShiInfo.dic[DevCode].YxBaoWen = yx.SuccessMsg(DicBiaoShiInfo.dic[DevCode].LineCtrl, DevCode, DoorId, ref MsgNum);
                    DicBiaoShiInfo.dic[DevCode].LineCtrl = DicBiaoShiInfo.dic[DevCode].LineCtrl == (byte)0x73 ? (byte)0x53 : (byte)0x73;
                    DicBiaoShiInfo.dic[DevCode].YcBaoWen = yc.SuccessMsg(DicBiaoShiInfo.dic[DevCode].LineCtrl, DevCode, DoorId, ref MsgNum);
                    break;
            }
            yx.DisPoseResource();
            yc.DisPoseResource();
            return DicBiaoShiInfo.dic[DevCode].YxBaoWen != null || DicBiaoShiInfo.dic[DevCode].YcBaoWen != null;
        } 
        /// <summary>
        /// 判断遥信遥测数据是否都已经发完了
        /// </summary>
        /// <param name="devCode"></param>
        /// <returns></returns>
        public bool IsHaveFirstYxyc(string devCode)
        {
            //判断是否有遥信遥测数据 
            return DicBiaoShiInfo.dic[devCode].YxBaoWen != null && DicBiaoShiInfo.dic[devCode].YxBaoWen.Length != 0 ||
                   DicBiaoShiInfo.dic[devCode].YcBaoWen != null && DicBiaoShiInfo.dic[devCode].YcBaoWen.Length != 0;
        }

        /// <summary>
        /// 合成长帧报文
        /// </summary>
        /// <param name="layer">是否为一级数据</param> 
        /// <returns>合成的完整报文</returns>
        public byte[] SynthesisLongMsg(EnumsInfo.DataType layer)
        {
            byte[] message = null;
            var other = new AnalysisOther(DevCode);
            other.LineCtrl = DicBiaoShiInfo.dic[DevCode].LineCtrl;
            switch (layer)
            {
                case EnumsInfo.DataType.First:
                    message = GetFistData(DoorId, other);
                    break;
                case EnumsInfo.DataType.NoFirst:
                    message = other.GetNoFirstData(DoorId);
                    break;
                case EnumsInfo.DataType.Second:
                    message = GetNBlance101SecondData(other);
                    break;
                case EnumsInfo.DataType.ZongZhaoJH:
                    message = other.ConfimZongZhaoJh(DoorId);
                    break;
                case EnumsInfo.DataType.Balance:
                case EnumsInfo.DataType.NoBalance:
                    message = other.EndLink(DoorId);
                    break;
            }
            other.DisPoseResource();
            return message;
        }

        /// <summary>
        /// 获取一级数据
        /// </summary>
        /// <param name="doorid"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public byte[] GetFistData(int doorid, AnalysisOther other)
        {  
            if (IsHaveFirstYxyc(DevCode))
            {
                if (!DicBiaoShiInfo.dic[DevCode].YxTrue)
                {
                    DicBiaoShiInfo.dic[DevCode].YxTrue = true;
                    return DicBiaoShiInfo.dic[DevCode].YxBaoWen;
                }
                if (!DicBiaoShiInfo.dic[DevCode].YcTrue)
                {
                    DicBiaoShiInfo.dic[DevCode].YcTrue = true;
                    return DicBiaoShiInfo.dic[DevCode].YcBaoWen;
                }
                DicBiaoShiInfo.dic[DevCode].YxTrue = false;
                DicBiaoShiInfo.dic[DevCode].YcTrue = false;
                DicBiaoShiInfo.dic[DevCode].YxBaoWen = null;
                DicBiaoShiInfo.dic[DevCode].YcBaoWen = null;
                return other.GetNoFirstData(doorid);
            }
            DicBiaoShiInfo.dic[DevCode].YxTrue = false;
            DicBiaoShiInfo.dic[DevCode].YcTrue = false;
            return other.GetNoFirstData(doorid); 
        }
        /// <summary>
        /// 获取二级数据_soe(针对于非平衡的处理)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public byte[] GetNBlance101SecondData(AnalysisOther other)
        {
            var soe = new AnalysisSoe(DevCode);  
            MsgSoe = soe.SynthesisNBlance101SoeMsg(DevCode);
            if (MsgSoe == null || MsgSoe.Length==0)
            { 
                return other.GetNoSecondData(DoorId); 
            }
            return MsgSoe;
        }
        #endregion

        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource<T>(ref T analysisFrameBase)
        {
            if (analysisFrameBase is AnalysisShortFrameBase)
            {
                var tt = analysisFrameBase as AnalysisShortFrameBase;
                tt.DisPoseResource();
                analysisFrameBase = default(T);
            }
            else if (analysisFrameBase is AnalysisLongFrameBase)
            {
                var tt = analysisFrameBase as AnalysisLongFrameBase;
                tt.DisPoseResource();
                analysisFrameBase = default(T);
            } 
        }
    }
}
