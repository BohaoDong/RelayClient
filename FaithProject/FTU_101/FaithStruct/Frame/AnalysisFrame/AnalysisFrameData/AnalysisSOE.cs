using System;
using System.Collections.Generic;
using System.Data;
using BLL;
using Model;
using Ulity;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData
{
    public class AnalysisSoe : AnalysisDataBase
    {
        public AnalysisSoe(string devCode)
            : base(devCode)
        {
        }

        /// <summary>
        /// 报文信息体
        /// </summary>
        public string MessageSoe;

        /// <summary>
        /// 信息体数量
        /// </summary>
        public int MessageNum;

        /// <summary>
        /// 合成SOE控制欲
        /// </summary>
        public int CtrlNum;

        /// <summary>
        /// 获取非平衡SOE报文_请求一条发送一条不多发送
        /// </summary>
        /// <returns></returns>
        public byte[] SynthesisNBlance101SoeMsg(string devCode)
        {
            LineCtrl = 0x08;
            DevCode = devCode;
            TableSoe = GetDataSoebll.GetNBlance101DataSoe(DevCode);
            GetDataSoebll.DisPoseResource();
            DataContent = new List<byte>();
            if (TableSoe != null && TableSoe.Rows.Count > 0)
            {
                DicBiaoShiInfo.dic[DevCode].SoeCreateTime = TableSoe.Rows[0]["CreateTime"].ToString();//soe时间
                foreach (DataRow row in TableSoe.Rows)
                {
                    DataContent.Add(Helper.StrToHexByte(row["DotNum"].ToString())[1]);
                    DataContent.Add(Helper.StrToHexByte(row["DotNum"].ToString())[0]);
                    DataContent.Add((byte) Convert.ToInt32(row["TagValue"]));
                    DataContent.AddRange(Helper.StrToHexByte(GetTimeScale(Convert.ToDateTime(row["CreateTime"])))); 
                }
                if (DataContent.Count > 0)
                {
                    MessageNum = TableSoe.Rows.Count; 
                    return GetLongFrameByteContent(DataContent);
                }
            }
            return null;
        }

        /// <summary>
        /// ASDU
        /// </summary>
        /// <returns></returns>
        protected override byte[] GetAsdu()
        {
            DataContent = new List<byte>();
            DataContent.Add(BaseHelper.SOE);
            DataContent.Add((byte) (MessageNum));
            DataContent.Add(0x03);
            return DataContent.ToArray();
        }

        /// <summary>
        /// 平衡101soe_有多少发送多少
        /// </summary> 
        /// <param name="soeInfo"></param>
        /// <returns></returns>
        public void SynthesisBlance101SoeMsg(ref SoeInfo soeInfo)
        {
            LineCtrl = 0x53;
            BanlansSoeDataSet = GetDataSoebll.GetBlance101DataSoe(soeInfo.CreateTime);
            GetDataSoebll.DisPoseResource();
            if (BanlansSoeDataSet != null && BanlansSoeDataSet.Tables[1].Rows.Count > 0)
            {
                DataContent = new List<byte>();
                foreach (DataRow tableRow in BanlansSoeDataSet.Tables[1].Rows)//遍历设备跟时间
                {
                    DevCode = tableRow["FaultDevCode"].ToString();
                    //得到设备的soe信息
                    TableSoe = Helper.GetTable(BanlansSoeDataSet.Tables[0], new[] { "FaultDevCode" }, new[] { tableRow["FaultDevCode"].ToString() }); //SOE数据表
                    soeInfo.CreateTime = Convert.ToDateTime(TableSoe.Rows[0]["CreateTime"]);//soe时间
                    foreach (DataRow tableDataRow in TableSoe.Rows) //遍历soe数据
                    {
                        DataContent.Add(Helper.StrToHexByte(tableDataRow["DotNum"].ToString())[1]);
                        DataContent.Add(Helper.StrToHexByte(tableDataRow["DotNum"].ToString())[0]);
                        DataContent.Add((byte) Convert.ToInt32(tableDataRow["TagValue"]));
                        DataContent.AddRange(Helper.StrToHexByte(GetTimeScale(Convert.ToDateTime(TableSoe.Rows[0]["SoeTime"]))));
                    }
                    //添加soe时间
                    MessageNum = TableSoe.Rows.Count;
                    soeInfo.SoeDataDictionary[DevCode] = GetLongFrameByteContent(DataContent); //只存当前soe的最新数据
                }
            } 
        } 

        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource()
        {
            DataContent = null;
            MessageSoe = null;
            LongMsg = null;
            TableSoe = null;
            BanlansSoeDataSet = null;
        }
    }
} 
