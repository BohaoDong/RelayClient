using System.Collections.Generic;
using System.Data;
using Model;
using Ulity;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData
{
    public class Base
    {
        /// <summary>
        /// 控制欲
        /// </summary>
        public int LineCtrl { get; set; }
        /// <summary>
        /// 报文
        /// </summary>
        public List<byte> DataContent { get; set; }
        public List<byte> LongMsg { get; set; }
        public DataTable TableYc { get; set; }
        public DataTable TableYx { get; set; }
        public DataTable TableSoe { get; set; }
        public DataSet BanlansSoeDataSet { get; set; }

        public Base(string devCode)
        {
            DevCode = devCode;
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DevCode { get; set; }

        private int _devLength;
        /// <summary>
        /// 设备编号的长度是两个字节还是一个字节
        /// </summary>
        public int DevLength
        {
            get
            {
                _devLength = BaseHelper.LinkaddressLength;
                return _devLength;
            }
            set { _devLength = value; }
        }

        private List<byte> _devCodeByte;
        /// <summary>
        /// 字节数据的设备编号
        /// </summary>
        public List<byte> DevCodeByte {
            get
            {
                DevLength = BaseHelper.LinkaddressLength;
                _devCodeByte = new List<byte>();
                switch (DevLength)
                {
                    case 1:
                        _devCodeByte.Add(Helper.StrToHexByte(DevCode)[1]);
                        break;
                    case 2:
                        _devCodeByte.Add(Helper.StrToHexByte(DevCode)[1]);
                        _devCodeByte.Add(Helper.StrToHexByte(DevCode)[0]);
                        break;
                }
                return _devCodeByte;
            } 
        } 
    }
}
