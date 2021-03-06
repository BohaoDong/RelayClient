﻿using System.Data;
using DAL;

namespace BLL
{
    public class GetDataYxbll
    {  
        /// <summary>
        /// 获取遥测数据
        /// </summary>
        /// <param name="devCode">设备编码地址</param>
        /// <param name="doorid"></param>
        /// <returns>返回datatable类型</returns>
        public static DataTable GetDataYx(string devCode, int doorid)
        {
            return GetDataYxdal.GetDataYx(devCode, doorid);
        }  
        /// <summary>
        /// 资源清理
        /// </summary>
        public static void DisPoseResource()
        {
            GetDataYxdal.DisPoseResource();
        }
    }
}
