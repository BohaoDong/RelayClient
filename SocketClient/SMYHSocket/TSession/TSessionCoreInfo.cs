using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using FaithProject;

namespace SocketClient.SMYHSocket.TSession
{
    public class SessionCoreInfo
    { 
        public FaithBaseBll FaithBll;
        /// <summary>
        /// 选择的规约ID
        /// </summary>
        public int FaithSelectValue;
        /// <summary>
        /// 规约下拉列表框中的Value
        /// </summary>
        public string FaithValueName;
        /// <summary>
        /// 以线程同步的方式创建一个字典;//记载了没有连接成功的连接
        /// </summary>
        public static Dictionary<string, string> ReConnectDataDictionary = new Dictionary<string, string>();
        /// <summary>
        /// 主要的socket集合
        /// </summary>
        public Socket MainSocket;
        /// <summary>
        /// 连接的标志
        /// </summary>
        public bool Conn; 
        /// <summary>
        /// 接收的十六进制报文
        /// </summary>
        public string SixteenStr { get; set; }
        /// <summary>
        /// Ip地址跟端口的数组
        /// </summary>
        public string[] Ipstr { get; set; }

        /// <summary>
        /// 连接设备的IP+端口
        /// </summary>
        public string Ip = string.Empty;
        /// <summary>
        /// 要发给对应的socket在mainSocket中的index值
        /// </summary>
        public int MianSocketIndex { get; set; }
        /// <summary>
        /// byte的报文_承载心跳包
        /// </summary>
        public List<byte> ByteContent { get; set; }
        /// <summary>
        /// 设备编号集合
        /// </summary>
        public List<string> DevList { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DevCode { get; set; }
        /// <summary>
        /// 接受的byte[]数据(应用在接受方法里面)
        /// </summary>
        public byte[] Btt { get; set; }
        /// <summary>
        /// 创建连接后跟服务端通讯的socket
        /// </summary>
        public Socket WorkSocket { get; set; }
        /// <summary>
        /// 接受服务端发送过来的数据
        /// </summary>
        public static byte[] DataBuffer { get; set; }

        public delegate void ShowDevStateDelegate(bool devState, string devCode);
        /// <summary>
        /// 显示未上线的设备
        /// </summary>
        public event ShowDevStateDelegate ShowDevStateEvent;

        /// <summary>
        /// 显示设备未上线
        /// </summary> 
        /// <param name="devState"></param>
        /// <param name="devCode"></param>
        protected virtual void OnShowDevStateEvent(bool devState, string devCode)
        {
            var handler = ShowDevStateEvent;
            if (handler != null) handler(devState,devCode);
        }
    }
}
