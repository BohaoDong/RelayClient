using System;
using System.Data;
using DBUtility;
using Model;

namespace DAL
{
    public class GetDataSoedal:DbBase
    {  
        /// <summary>
        /// 获取非平衡SOE数据
        /// </summary>
        /// <param name="devCode">设备编码地址</param> 
        /// <returns>返回datatable类型</returns> 
        public static DataTable GetNBlance101DataSoe(string devCode)
        {
            try
            {
                Db = new DbHelper();
                Select =
                                //" declare @id int " +
                                //" select @id=count(ID) from View_DATA_SOE where CreateTime>'" + DicBiaoShiInfo.dic[devCode].SoeCreateTime + "'" +
                                //" if @id>0 " +
                                "	select View_DATA_SOE.DevCode,DotNum,TagValue,View_DATA_SOE.CreateTime from View_DATA_SOE	left join BASIC_TAGINFO on View_DATA_SOE.TagId=BASIC_TAGINFO.ID left join BASIC_Device d on View_DATA_SOE.DEVId=d.ID where View_DATA_SOE.CreateTime>'" + DicBiaoShiInfo.dic[devCode].SoeCreateTime + "'  " +
                                "and View_DATA_SOE.DevCode='" + devCode + "' order by CreateTime desc";
                Table = Db.GetDataTable(CommandType.Text, Select);
                Db.DisPoseResource();
                return Table;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 平衡式101获取SOE数据
        /// </summary> 
        /// <param name="time"></param>
        /// <returns></returns>
        public static DataSet GetBlance101DataSoe(DateTime time)
        {
            try
            {
                //string select = " declare @id int  " +
                //                " declare @time varchar(50) " +
                //                 " select top 1 @id=id ,@time=CONVERT(varchar(50),CreateTime, 25) from Fault_DATA_FaultSoeAnalysis where CreateTime>'" + time + "'  order by CreateTime " +
                //                 " if @id>0 " +
                //                     " select  soe.FaultDevCode,soe.FaultDevName,tag.DotNum,soe.TagValue,soe.TagName,soe.SoeTime,soe.CreateTime " +
                //                     " from Fault_DATA_FaultSoeAnalysis soe " +
                //                     " left join BASIC_TAGINFO  tag on soe.TagID=tag.ID " +
                //                     " left join BASIC_Device  dev on dev.DevCode=soe.FaultDevCode and dev.DevType=soe.FaultDevid" +
                //                     " where soe.CreateTime=@time " +
                //                     " order by soe.CreateTime" +
                //                " else" +
                //                    " print('无SOE数据') ";
                Select = "select soe.FaultDevCode,soe.FaultDevName,tag.DotNum,soe.TagValue,soe.TagName,soe.SoeTime,soe.CreateTime " +
                                     " from Fault_DATA_FaultSoeAnalysis soe " +
                                     " left join BASIC_TAGINFO  tag on soe.TagID=tag.ID " +
                                     " left join BASIC_Device  dev on dev.DevCode=soe.FaultDevCode and dev.DevType=soe.FaultDevid" +
                                     " where soe.CreateTime>'" + time + "'" +
                                     " order by soe.CreateTime "  +
                                     "select FaultDevCode from Fault_DATA_FaultSoeAnalysis where SoeTime>'" + time + "'  group by FaultDevCode  order by FaultDevCode desc";//,SoeTime
                DsDataSet= Db.GetDataSet(CommandType.Text, Select);
                Db.DisPoseResource();
                return DsDataSet;
            }
            catch {
                return null;
            }
        }  
        /// <summary>
        /// 资源清理
        /// </summary>
        public static void DisPoseResource()
        {
            DsDataSet = null;
            Select = null;
            Db = null;
            Table = null;
        }
    }
}
