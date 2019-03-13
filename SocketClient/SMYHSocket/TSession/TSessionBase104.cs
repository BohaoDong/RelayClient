using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;  
using Model;
using SocketClient.SmyhLog;
using Ulity;
using Ulity.ErrorLog;

namespace SocketClient.SMYHSocket.TSession
{
    public class SessionBase104 : SessionBase
    {
        /// <summary>
        /// 合成发送心跳包
        /// </summary>
        public void SendHeartBeat()
        {
            #region 心跳包的合成

            ByteContent = new List<byte>();
            switch (FaithValueName)
            {
                case "F101":
                    ByteContent.Add(0x10);
                    ByteContent.Add(0x6C);
                    switch (BaseHelper.FB101LinkaddressLength)
                    {
                        case 1:
                            ByteContent.Add(Helper.StrToHexByte(DevCode)[1]);
                            ByteContent.Add((byte) (ByteContent[1] + ByteContent[2]));
                            break;
                        case 2:
                            ByteContent.Add(Helper.StrToHexByte(DevCode)[1]);
                            ByteContent.Add(Helper.StrToHexByte(DevCode)[0]);
                            ByteContent.Add((byte) (ByteContent[1] + ByteContent[2] + ByteContent[3]));
                            break;
                    }
                    ByteContent.Add(0x16);
                    break;
                case "F101_Balance":
                    ByteContent.Add(0x10);
                    ByteContent.Add(0x49);
                    switch (BaseHelper.B101LinkaddressLength)
                    {
                        case 1:
                            ByteContent.Add(Helper.StrToHexByte(DevCode)[1]);
                            ByteContent.Add((byte) (ByteContent[1] + ByteContent[2]));
                            break;
                        case 2:
                            ByteContent.Add(Helper.StrToHexByte(DevCode)[1]);
                            ByteContent.Add(Helper.StrToHexByte(DevCode)[0]);
                            ByteContent.Add((byte) (ByteContent[1] + ByteContent[2] + ByteContent[3]));
                            break;
                    }
                    ByteContent.Add(0x16);
                    break;
            }
            SendDatagram(ByteContent.ToArray());

            #endregion
        }

        /// <summary>
        /// 创建Socket
        /// </summary>
        public void CreateSocket()
        {
            try
            {
                lock (this)
                { 
                    Conn = true;
                    MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Ipstr = Ip.Split(':');
                    MainSocket.BeginConnect(IPAddress.Parse(Ipstr[0]), int.Parse(Ipstr[1]), ConnectCallback, MainSocket);
                }
            }
            catch (Exception ex)
            {
                OnShowDevStateEvent(false,DevCode);
                ReConnectDataDictionary[DevCode]= Ip;
                WriteErrorLog.WriteToFile("创建连接失败:" + ex.Message);
            }
        }

        /// <summary>
        /// (异步连接回调的函数)
        /// </summary>
        /// <param name="ir"></param>
        private void ConnectCallback(IAsyncResult ir)
        {
            Socket client = (Socket) ir.AsyncState; //获得连接
            try
            {
                client.EndConnect(ir); //结束挂起连接
                TShowData(DevCode, "连接成功", 0);
                OnShowDevStateEvent(true,DevCode); 
                //DicBiaoShiInfo.dic[DevCode].SoeCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DicBiaoShiInfo.dic[DevCode].BttBytes = new byte[1024];
                client.BeginReceive(DicBiaoShiInfo.dic[DevCode].BttBytes, 0, DicBiaoShiInfo.dic[DevCode].BttBytes.Length,
                    SocketFlags.None, OnDataReceived, client);
                SendHeartBeat();
            }
            catch (SocketException ex)
            {
                TShowData(DevCode, "连接不成功", 0);
                OnShowDevStateEvent(false,DevCode);
                ReConnectDataDictionary[DevCode] = Ip;
                WriteErrorLog.WriteToFile("连接回调失败" + ex.Message);
            }
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="ir"></param>
        private void OnDataReceived(IAsyncResult ir)
        {
            lock (this)
            {
                try
                {
                    WorkSocket = ir.AsyncState as Socket;
                    if (WorkSocket != null)
                    {
                        var length = WorkSocket.EndReceive(ir);
                        ir.AsyncWaitHandle.Close();
                        if (length == 0 || length == -1) //0:客户端断开  -1:网络中断 
                        {
                            if (WorkSocket != null)
                            {
                                TShowData(WorkSocket.RemoteEndPoint.ToString(), length == 0 ? "客户端断开连接" : "网络中断", 0);
                                OnShowDevStateEvent(false,DevCode);
                                DicBiaoShiInfo.dic[DevCode].SessionBase104 = null;//清除存储的socket连接类
                                ReConnectDataDictionary[DevCode]=WorkSocket.RemoteEndPoint.ToString();
                                WorkSocket.Close();
                            }
                            WriteErrorLog.WriteToFile(DevCode + (length == 0 ? "客户端断开连接" : "网络中断"));
                            return;
                        }
                        Btt = new byte[length];
                        Array.Copy(DicBiaoShiInfo.dic[DevCode].BttBytes, 0, Btt, 0, length);
                        SixteenStr = BitConverter.ToString(Btt).Replace('-', ' ');
                        LoadFaithBll();
                        TShowData(DevCode, SixteenStr, 2);
                        WriteMsgLog.WriteToFile("==>" + SixteenStr, DevCode); //接受报文的日志
                        FaithBll.ReceiveDeviceData(Btt);
                        WorkSocket.BeginReceive(DicBiaoShiInfo.dic[DevCode].BttBytes, 0,
                            DicBiaoShiInfo.dic[DevCode].BttBytes.Length, SocketFlags.None, OnDataReceived, WorkSocket);
                    }
                }
                catch (Exception ex)
                {
                    if (Conn)
                    {
                        TShowData(DevCode, "服务器断开连接", 0);
                    }
                    WriteErrorLog.WriteToFile(DevCode + "接受数据失败" + ex.Message);
                }
            }
        }  

        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource()
        {
            ByteContent = null;
            Ipstr = null;
            Btt = null;
            SixteenStr = null;
            Ip = null; 
        }
    }
}
