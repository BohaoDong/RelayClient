using System;
using Model;
using Model.SessionEventArgs;

namespace FaithProject
{
    public abstract class FaithBaseBll 
    {
        public FaithBaseBll(int doorid)
        {
            DoorId = doorid;

        } 
        /// <summary>
        /// 链路地址长度
        /// </summary>
        public int LinkCount = 1;
        /// <summary>
        /// 公共地址长度
        /// </summary>
        public int PublicLinkCount = 1;
        /// <summary>
        /// 传送原因长度
        /// </summary>
        public int CotCode = 1;
        /// <summary>
        /// 协议所在通道ID
        /// </summary>
        public int DoorId
        {
            get;
            set;
        }
        public int MsgNum { get; set; }
        /// <summary>
        /// 设备编号_十六进制的设备编号_长度不定
        /// </summary>
        public string DevCode { get; set; }

        /// <summary>
        /// 应答事件
        /// </summary>
        public event EventHandler<SessionReceive_DeviceReply_EventArgs> SessionReceiveReplyDevice;

        /// <summary>
        /// 触发应答设备事件
        /// </summary>
        /// <param name="ipData"></param>
        protected virtual void OnSessionReceive_ReplyDevice(IpInfo ipData)
        {
            EventHandler<SessionReceive_DeviceReply_EventArgs> handler = SessionReceiveReplyDevice;
            if (handler != null)
            {
                SessionReceive_DeviceReply_EventArgs e = new SessionReceive_DeviceReply_EventArgs(ipData);
                handler(this, e);
            }
        }

        /// <summary>
        /// 接收硬件报文数据入口，根据报文类型分发到各方法中解析执行，获得结果后，触发相关事件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        abstract public void ReceiveDeviceData(byte[] data);

         /// <summary>
         /// 判断是否为平衡式数据帧
         /// </summary>
         public bool IsBalanceFrame
         {
             get;
             set;
         }
    }
}
