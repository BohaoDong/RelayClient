using System;
using System.Net.Sockets;
using FaithProject;
using Model;
using Model.SessionEventArgs;
using SocketClient.SmyhLog;
using Ulity.ErrorLog;

namespace SocketClient.SMYHSocket.TSession
{
    public class SessionBase : SessionCoreInfo
    {
        /// <summary>
        /// 获得规约处理类
        /// </summary>
        public void LoadFaithBll()
        {
            try
            {
                FaithBll = FaithFactory.GetFaithBll(FaithValueName, DevCode);
                FaithBll.SessionReceiveReplyDevice += SessionShowMsg;//应答事件
            }
            catch (Exception ex)
            {
                WriteErrorLog.WriteToFile(ex.Message);
            }
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        public event EventHandler<SessionReceive_DeviceReply_EventArgs> ShowData;
        public void SessionShowData(object sender, SessionReceive_DeviceReply_EventArgs e)
        {
            EventHandler<SessionReceive_DeviceReply_EventArgs> handler = ShowData;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        /// <summary>
        /// 触发设备应答事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SessionShowMsg(object sender, SessionReceive_DeviceReply_EventArgs e)
        {
            SendDatagram(e.IpData.Buf); 
        }
        public string Content { get; set; } 
        IpInfo _info;
        SessionReceive_DeviceReply_EventArgs e;
        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="data"></param>
        public void SendDatagram(byte[] data)
        {
            lock (this)
            {
                try
                {
                    if (data != null)
                    {
                        MainSocket.BeginSend(data, 0, data.Length, SocketFlags.None, EndSendDatagram, this);
                        Content = BitConverter.ToString(data).Replace('-',' ');
                        //发送报文显示方法
                        _info = new IpInfo
                        {
                            Msg = Content,
                            DevCode = DevCode,
                            Index = 1
                        };
                        e = new SessionReceive_DeviceReply_EventArgs(_info);
                        SessionShowData(null, e); 
                        WriteMsgLog.WriteToFile("<==" + Content, DevCode);
                        FaithBll = null;
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorLog.WriteToFile(ex.Message);
                }
            }
        }
        /// <summary>
        /// 发送数据完成处理函数, iar 为目标客户端 Session
        /// </summary>
        private void EndSendDatagram(IAsyncResult iar)
        {
            lock (this)
            {
                try
                {
                    MainSocket.EndSend(iar);
                    iar.AsyncWaitHandle.Close();
                }
                catch (Exception ex)
                {
                    WriteErrorLog.WriteToFile(ex.Message);
                }
            }
        }

        /// <summary>
        /// 界面显示消息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="msg">消息</param>
        /// <param name="index">0系统消息,1接受的消息2,发送的消息</param>
        public void TShowData(string devCode,string msg,int index)
        { 
            IpInfo info = new IpInfo();
            info.Msg = msg; info.DevCode = devCode; info.Index = index;
            SessionReceive_DeviceReply_EventArgs e = new SessionReceive_DeviceReply_EventArgs(info);
            SessionShowData(null, e);
        }
    }
}
