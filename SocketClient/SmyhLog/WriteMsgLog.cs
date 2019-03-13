using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SocketClient.SmyhLog
{ 
    public   class WriteMsgLog
    {
        /// <summary>
        /// 当前日志文件目录
        /// </summary>
        private static string _currentPathName =System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+ @"\SmyhBaoWenLOG\";
        /// <summary>
        /// 写日志锁
        /// </summary>
        protected static string Lock = "lock";

        public static bool WriteToFile(string message,string Devcode)
        {
            //  _currentPathName = ConfigurationManager.AppSettings["SmyhConfigLOG"].ToString();
            // 创建目录
            if (!Directory.Exists(_currentPathName))
            {
                Directory.CreateDirectory(_currentPathName);
            }
            // 根据日志文件周期生成文件名
            string fileName = null;
            fileName = string.Format("{0:D4}{1:D2}{2:D2}设备{3:X4}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Devcode);

            // 写日志
            lock (Lock)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(_currentPathName + fileName, true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("{0},{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
