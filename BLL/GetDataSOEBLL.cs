using System;
using System.Data;
using DAL;

namespace BLL
{
    public class GetDataSoebll
    {
        /// <summary>
        /// 获取SOE数据
        /// </summary>
        /// <param name="devCode">设备编码</param> 
        /// <returns>返回datatable类型</returns>
        public static DataTable GetNBlance101DataSoe(string devCode)
        {
            return GetDataSoedal.GetNBlance101DataSoe(devCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DataSet GetBlance101DataSoe(DateTime time)
        {
            return GetDataSoedal.GetBlance101DataSoe(time);
        }
        /// <summary>
        /// 资源清理
        /// </summary>
        public static void DisPoseResource()
        {
            GetDataSoedal.DisPoseResource();
        }
    }
}
