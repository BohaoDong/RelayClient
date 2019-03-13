using System.Data;
using DBUtility;

namespace DAL
{
    public class GetDataYxdal : DbBase
    {
        //public static DbHelper Db = new DbHelper();

        /// <summary>
        /// 获取遥信数据
        /// </summary>
        /// <param name="devCode">设备地址</param>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public static DataTable GetDataYx(string devCode, int doorId)
        { 
            Db = new DbHelper();
            switch (doorId)
            {
                case 1:
                    Select = "select r.[DotNum],r.[TagName],b.Modulus,b.BianBi,[TagValue],[CreateTime] from " +
                             "dbo.View_DATA_RealTime r " +
                             "left join dbo.BASIC_TAGINFO b on r.DotNum=b.DotNum " +
                             "where r.TagTypeId=1 and b.DevTypeID=1 and r.devtype=1 and DevCode='" + devCode + "'";
                    Table = Db.GetDataTable(CommandType.Text, Select);
                    break;
                case 2:
                    Select = "select r.[DotNum],r.[TagName],b.Modulus,b.BianBi,[TagValue],[CreateTime] from " +
                             "dbo.View_DATA_RealTime r " +
                             "left join dbo.BASIC_TAGINFO b on r.DotNum=b.DotNum " +
                             "where r.TagTypeId=1 and b.DevTypeID=7 and r.devtype=7 and DevCode='" + devCode + "'";
                        //1001
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
