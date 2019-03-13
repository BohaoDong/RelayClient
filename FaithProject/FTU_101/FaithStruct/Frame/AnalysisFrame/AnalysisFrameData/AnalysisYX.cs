using System;
using System.Collections.Generic;
using System.Data;
using BLL;
using Model;
using Ulity;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData
{
    public class AnalysisYx : AnalysisDataBase
    {
        public AnalysisYx(string devCode)
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
        public string MessageYx;
        /// <summary>
        /// 信息体数量
        /// </summary>
        public int MessageNum;
        #endregion 
        /// <summary>
        /// 获取遥测报文
        /// </summary>
        /// <param name="lineCtrl">控制欲</param>
        /// <param name="devCode">DevCode</param>
        /// <param name="doorId">通道ID</param>
        /// <param name="msgNum">信息体的数量</param>
        /// <returns></returns>
        public byte[] SuccessMsg(int lineCtrl,string devCode,int doorId,ref int msgNum)
        {
            try
            {
                LineCtrl = 0x28;
                MessageNum = 0; 
                TableYx = GetDataYxbll.GetDataYx(devCode, doorId);
                GetDataYxbll.DisPoseResource();
                DataContent = new List<byte>();
                if (TableYx != null && TableYx.Rows.Count > 0)
                {   //判断数据是否已经发过
                    if (YxIsUnloadTime(TableYx.Rows[0]["CreateTime"].ToString(), devCode))
                    {
                        DataContent.Add(Helper.StrToHexByte(TableYx.Rows[0][0].ToString())[1]);
                        DataContent.Add(Helper.StrToHexByte(TableYx.Rows[0][0].ToString())[0]);
                        foreach (DataRow row in TableYx.Rows)
                        {
                            DataContent.Add((byte)Convert.ToInt32(row["TagValue"])); 
                        } 
                    } 
                    if (DataContent.Count > 0)
                    {
                        msgNum = TableYx.Rows.Count;
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
            DataContent.Add(BaseHelper.M_SP_NA_1);
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
            MessageYx = null;
            TableYx = null;
            LongMsg = null;
        }
    }
}


