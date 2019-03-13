using System;
using System.Collections.Generic;
using Model;
using Ulity;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData
{
    public  class AnalysisOther : AnalysisDataBase
    {
        public AnalysisOther(string devCode)
            : base(devCode)
        { }
        /// <summary>
        /// 类型标示
        /// </summary>
        private byte Ti { get; set; }
        /// <summary>
        /// 可变结构限定词
        /// </summary>
        private byte LimitCode { get; set; }
        /// <summary>
        /// 传送原因
        /// </summary>
        private byte Cot { get; set; }
        /// <summary>
        /// ASDU
        /// </summary>
        /// <returns></returns>
        protected override byte[] GetAsdu()
        {
            DataContent = new List<byte> {Ti, LimitCode, Cot};
            return DataContent.ToArray();
        }
        /// <summary>
        /// 合成短帧报文(解析设备编号)
        /// </summary>
        /// <param name="ctrlM">控制欲</param>
        /// <returns>合成的报文</returns>
        public byte[] SynthesisShotMsg(int ctrlM)
        {
            DataContent = new List<byte> {0x10, (byte) ctrlM};
            DataContent.AddRange(DevCodeByte.ToArray());
            DataContent.Add(Helper.MessageCheckCode(DataContent.ToArray()));
            DataContent.Add(0x16);
            return DataContent.ToArray();
        }
        /// <summary>
        /// 合成总召唤激活报文
        /// </summary>
        /// <param name="doorid"></param>
        /// <returns></returns>
        public byte[] ConfimZongZhaoJh(int doorid)
        {
            DataContent = new List<byte>();
            switch (doorid)
            {
                case 1:
                    LineCtrl = 0x28;
                    DataContent.Add(0x00);
                    break;
                case 2:
                    LineCtrl = 0x53;
                    break;
            }
            Ti = 0x64; LimitCode = 0x01; Cot = 0x07;
            DataContent.Add(0x00);
            DataContent.Add(0x00);
            DataContent.Add(0x14);
            return GetLongFrameByteContent(DataContent);
        }
        /// <summary>
        /// 合成初始化结束报文
        /// </summary>
        /// <param name="doorid"></param>
        /// <returns></returns>
        public byte[] EndLink(int doorid)
        {
            Ti = 0x46; LimitCode = 0x01; Cot = 0x04;
            DataContent = new List<byte>(); 
            switch (doorid)
            {
                case 1:
                    LineCtrl = Convert.ToInt32(0x08);
                    DataContent.Add(0x00);
                    DataContent.Add(0x00);
                    DataContent.Add(0x00);
                    break;
                case 2:
                    LineCtrl = Convert.ToInt32(DicBiaoShiInfo.dic[DevCode].LineCtrl); 
                    DataContent.Add(0x00);
                    DataContent.Add(0x00);
                    DataContent.Add(0x01);
                    break;
            }
            return GetLongFrameByteContent(DataContent);
        }

        /// <summary>
        /// 召唤命令(激活终止)_无一级数据
        /// </summary>
        /// <param name="doorid"></param>
        /// <returns></returns>
        public byte[] GetNoFirstData(int doorid)
        {
            Ti = 0x64; LimitCode = 0x01; Cot = 0x0A;
            DataContent = new List<byte> {0x00, 0x00, 0x14};
            switch (doorid)
            {
                case 1:
                    LineCtrl = 0x28;
                    break;
                case 2:
                    LineCtrl = Convert.ToInt32(DicBiaoShiInfo.dic[DevCode].LineCtrl);
                    break;
            }
            return GetLongFrameByteContent(DataContent);
        }
        /// <summary>
        /// 无二级数据_非平衡条件下
        /// </summary>
        /// <param name="doorid"></param>
        /// <returns></returns>
        public byte[] GetNoSecondData(int doorid)
        {
            return  SynthesisShotMsg(0x09); 
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void DisPoseResource()
        {
            DataContent =null ;
            LongMsg = null;
        }
    }
}
