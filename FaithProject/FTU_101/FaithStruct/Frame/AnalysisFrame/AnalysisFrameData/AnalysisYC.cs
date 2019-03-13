using System.Collections.Generic;
using System.Data;
using BLL;
using Model;
using Ulity;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData
{
    public class AnalysisYc : AnalysisDataBase
    {
        public AnalysisYc(string devCode)
            : base(devCode)
        { }
        #region 变量
        /// <summary>
        /// 设备编码-两个字节(十六进制数)
        /// </summary>
        public int Address;
        /// <summary>
        /// 报文信息体
        /// </summary>
        public string MessageYc;
        /// <summary>
        /// 信息体数量
        /// </summary>
        public int MessageNum;
        #endregion 
        /// <summary>
        /// 获取遥测报文
        /// </summary>
        /// <param name="lineCtrl"></param>
        /// <param name="devCode"></param>
        /// <param name="doorId"></param>
        /// <param name="msgNum"></param>
        /// <returns></returns>
        public byte[] SuccessMsg(int lineCtrl, string devCode, int doorId, ref int msgNum)
        {
            try
            {
                LineCtrl = lineCtrl;
                MessageNum = 0; 
                TableYc = GetDataYcbll.GetDataYc(devCode, doorId);
                GetDataYcbll.DisPoseResource();
                if (TableYc != null && TableYc.Rows.Count > 0)
                {
                    DataContent = new List<byte>();
                    //判断数据是否已经发过
                    if (YcIsUnloadTime(TableYc.Rows[0]["CreateTime"].ToString(), devCode))
                    {
                        DataContent.Add(Helper.StrToHexByte(TableYc.Rows[0][0].ToString())[1]);
                        DataContent.Add(Helper.StrToHexByte(TableYc.Rows[0][0].ToString())[0]);
                        //string DotNum = GetHighLowValue(table.Rows[0][0].ToString());
                        foreach (DataRow row in TableYc.Rows)
                        {
                            DataContent.AddRange(Helper.StrToHexByte(GetNegativeNum(row["TagValue"].ToString(), row["Modulus"].ToString(), row["BianBi"].ToString())));
                            DataContent.Add(0x00);
                        }
                    }
                    if (DataContent.Count > 0)
                    {
                        msgNum = TableYc.Rows.Count;
                        MessageNum = msgNum;
                        return GetLongFrameByteContent(DataContent);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// ASDU
        /// </summary>
        /// <returns></returns>
        protected override byte[] GetAsdu()
        {
            DataContent = new List<byte>();
            DataContent.Add(BaseHelper.M_ME_NB_1);
            DataContent.Add((byte)(MessageNum + 128));
            DataContent.Add(BaseHelper.introgen);
            return DataContent.ToArray();
        }
        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource()
        {
            DataContent = null;
            MessageYc = null;
            TableYx = null;
            LongMsg = null;
        }
    }
}
