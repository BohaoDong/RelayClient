using System.Data;
using DBUtility;

namespace DAL
{
    public class DbBase
    {
        public static string Select { get; set; }  
        public static DataTable Table { get; set; }
        public static DataSet DsDataSet { get; set; }
        public static DbHelper Db { get; set; } 
    }
}
