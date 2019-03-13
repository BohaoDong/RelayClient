using System.Data;
using DAL;

namespace BLL
{
    public static class GetDataYcbll
    {
        /// <summary>
        /// 获取遥测数据
        /// </summary>
        /// <param name="devCode">设备编码地址</param>
        /// <param name="doorid"></param>
        /// <returns>返回datatable类型</returns>
        public static DataTable GetDataYc(string devCode, int doorid)
        {
            return GetDataYcdal.GetDataYc(devCode, doorid);
        }
        /// <summary>
        /// 资源清理
        /// </summary>
        public static void DisPoseResource()
        {
            GetDataYcdal.DisPoseResource();
        }
    }
}
