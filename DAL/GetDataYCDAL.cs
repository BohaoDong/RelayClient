using System.Data;
using DBUtility;

namespace DAL
{
    public class GetDataYcdal:DbBase
    { 
        /// <summary>
        /// 获取遥测数据
        /// </summary>
        /// <param name="devCode">设备编码地址</param>
        /// <param name="doorId"></param>
        /// <returns>返回datatable类型</returns> 
        public static DataTable GetDataYc(string devCode, int doorId)
        { 
            Db = new DbHelper();
            switch (doorId)
            {
                case 1:
                    Select = "select r.[DotNum],r.[TagName],b.Modulus,b.BianBi,[TagValue],[CreateTime] from " +
                             "dbo.View_DATA_RealTime r " +
                             "left join dbo.BASIC_TAGINFO b on r.DotNum=b.DotNum " +
                             "where r.TagTypeId=2 and b.DevTypeID=1 and r.devtype=1 and DevCode='" + devCode + "'"; //12
                    Table = Db.GetDataTable(CommandType.Text, Select);
                    break;
                case 2:
                    Select = "select r.[DotNum],r.[TagName],b.Modulus,b.BianBi,[TagValue],[CreateTime] from " +
                             "dbo.View_DATA_RealTime r " +
                             "left join dbo.BASIC_TAGINFO b on r.DotNum=b.DotNum " +
                             "where r.TagTypeId=2 and b.DevTypeID=7 and r.devtype=7 and DevCode='" + devCode +
                             "' order by b.DotNum"; //1001
                    Table = Db.GetDataTable(CommandType.Text, Select);
                    break;
            }
            Db.DisPoseResource();
            return Table;
        }
        /// <summary>
        /// 资源清理
        /// </summary>
        public static void DisPoseResource()
        {
            Select = null;
            Db = null;
            Table = null;
        }
    }
}
