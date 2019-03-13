using System;
using System.Collections.Generic; 

namespace Model
{
    public class SoeInfo 
    {
        /// <summary>
        /// 设备编号,跟数据库中一样的十六进制字符串
        /// </summary>
        public string DevCode { get; set; }

        /// <summary>
        /// 设备的soe报文
        /// </summary>
        public Dictionary<string, byte[]> SoeDataDictionary { get; set; }
        /// <summary>
        /// soe的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
