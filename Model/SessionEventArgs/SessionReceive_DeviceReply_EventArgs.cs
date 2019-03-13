using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SessionEventArgs
{
    /// <summary>
    /// 设备应答参数
    /// </summary>
    public class SessionReceive_DeviceReply_EventArgs : EventArgs
    {
        public IpInfo IpData;
        public SessionReceive_DeviceReply_EventArgs(IpInfo IpData)
        {
            this.IpData = IpData;
            if (IpData.Index == null)
            {
                IpData.Index = 1;
            }
        }
        /// <summary>
        /// 获得应答报文
        /// </summary>
        /// <returns></returns>
        public IpInfo GetReplyData()
        {
            return this.IpData;
        } 
    }
}
