using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData;
using Model;
using Model.SessionEventArgs;

namespace SocketClient.SMYHSocket.TSession
{
    public class Session
    {
        /// <summary>
        /// 停止建立后续连接
        /// </summary>
        public bool StopConn;
        /// <summary>
        /// 选择的规约 F101_非平衡101 F101_Balance_平衡101
        /// </summary>
        public string FaithValueName;
        public event EventHandler<SessionReceive_DeviceReply_EventArgs> ShowData;
        /// <summary>
        /// 创建通讯连接线程
        /// </summary>
        public Thread TsocketThread; 
        /// <summary>
        /// 断线重连线程
        /// </summary>
        public Thread ReConnThread;
        /// <summary>
        /// soe线程
        /// </summary>
        public Thread SoeThread;
        public AsyncEventHandler Asy;
        /// <summary>
        /// 创建socket连接
        /// </summary>
        public void CreateSocket()
        {
            TsocketThread = new Thread(Tdsocket) { IsBackground = true };
            TsocketThread.Start();
            ReConnThread = new Thread(ReConnectAction) { IsBackground = true };
            ReConnThread.Start();
            if (FaithValueName == "F101_Balance_")
            {
                Asy = AsyncEvent1;
                SoeThread = new Thread(SendSecondSoeData) { IsBackground = true };
                SoeThread.Start();
            }
        }
        /// <summary>
        /// 创建socket
        /// </summary>
        public void Tdsocket()
        {
            foreach (
                var t in
                    DicBiaoShiInfo.dic.Keys.Select(key => DicBiaoShiInfo.dic[key].SessionBase104 as SessionBase104)
                        .Where(t => t != null))
            {
                if (!StopConn)
                {
                    t.ShowData += ShowData;
                    t.CreateSocket();
                    Thread.Sleep(500);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        public void SessionClose()
        {
            TsocketThread.Abort();ReConnThread.Abort();
            SessionCoreInfo.ReConnectDataDictionary.Clear();
            foreach (var t in DicBiaoShiInfo.dic.Keys.Select(key => DicBiaoShiInfo.dic[key].SessionBase104 as SessionBase104).Where(t => t != null))
            {
                t.WorkSocket.Close();
                t.MainSocket.Close();
            }
        }  
        /// <summary>
        /// 断线重连
        /// </summary>
        private static void ReConnectAction()
        {
            while (true)
            {
                Thread.Sleep(300 * 1000);
                if (SessionCoreInfo.ReConnectDataDictionary.Count > 0)
                {
                    var dicList = SessionCoreInfo.ReConnectDataDictionary.ToArray();
                    for (var i = 0; i < dicList.Length; i++)
                    {
                        var sessionBase104 = DicBiaoShiInfo.dic[dicList[i].Key].SessionBase104 as SessionBase104;
                        if (sessionBase104 != null) sessionBase104.CreateSocket();
                        SessionCoreInfo.ReConnectDataDictionary.Remove(dicList[i].Key);
                    } 
                }
            } 
            // ReSharper disable once FunctionNeverReturns
        }
        /// <summary>
        /// 记录soe发送的时间
        /// </summary>
        public DateTime CreateTime = DateTime.Now.AddYears(-1);
        /// <summary>
        /// 平衡101自动发送二级SOE数据
        /// </summary>
        protected void SendSecondSoeData()
        { 
            var soe = new AnalysisSoe("");
            Thread.Sleep(180*1000);//先静默10秒是为了让最初运行的交互运行完
            while (true)
            {
                var soeInfo = new SoeInfo();
                soeInfo.CreateTime = CreateTime;
                soeInfo.SoeDataDictionary = new Dictionary<string, byte[]>();
                soe.SynthesisBlance101SoeMsg(ref soeInfo);
                CreateTime = soeInfo.CreateTime;
                foreach (var key in soeInfo.SoeDataDictionary.Keys)
                {
                    if (DicBiaoShiInfo.dic.ContainsKey(key))
                    {
                        var sessionBase104 = DicBiaoShiInfo.dic[key].SessionBase104 as SessionBase104;
                        IAsyncResult ia = Asy.BeginInvoke(sessionBase104, soeInfo.SoeDataDictionary[key], null, null);
                        Asy.EndInvoke(ia);
                    }
                }
                soeInfo.SoeDataDictionary = null;
                soe.DisPoseResource();
                Thread.Sleep(BaseHelper.SendBlance_101SOETime);//扫描时间间隔
            }
            // ReSharper disable once FunctionNeverReturns
        }
        /// <summary>
        /// 定义异步委托
        /// </summary>
        /// <param name="sessionBase104"></param>
        /// <param name="bytes"></param>
        public delegate void AsyncEventHandler(SessionBase104 sessionBase104,byte[] bytes);
        /// <summary>
        /// 异步委托的方法
        /// </summary>
        /// <param name="sessionBase104"></param>
        /// <param name="bytes"></param>
        void AsyncEvent1(SessionBase104 sessionBase104, byte[] bytes)
        {
            if (sessionBase104 != null) sessionBase104.SendDatagram(bytes);
        }
    }
}
