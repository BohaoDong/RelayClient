using System;
using System.Collections.Generic;
using System.Data;
using DBUtility;
using Ulity.ErrorLog;

namespace DAL
{
    public class GetDevDal
    {
        public static DbHelper Db = new DbHelper();

        /// <summary>
        /// 获取遥信数据
        /// </summary> 
        /// <param name="doorId"></param>
        /// <returns></returns>
        public static List<string> GetAllDevData(int doorId)
        {
            try
            {
                List<string> allDevList = new List<string>();
                string select = string.Empty;
                switch (doorId)
                {
                    case 0:
                        @select = "select ID,DevCode,DevType from  dbo.BASIC_Device where DevType=1";
                        break;
                    case 1:
                        @select = "select ID,DevCode,DevType from  dbo.BASIC_Device where DevType=7";
                        break;
                }
                var table = Db.GetDataTable(CommandType.Text, @select);
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    allDevList.Add(table.Rows[i]["DevCode"].ToString());
                }
                Db.DisPoseResource();
                return allDevList;
            }
            catch (Exception ex)
            {
                WriteErrorLog.WriteToFile(ex.Message);
                return null;
            }
        }
    }
}