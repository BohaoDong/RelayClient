using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SocketClient.BLL;

namespace SocketClient
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }
        private Socket mainSocket;
        public String time = null;
        private AsyncCallback callBack = null;
        private ArrayList list;
        ToHexBLL c = new ToHexBLL();
        private void Server_Load(object sender, EventArgs e)
        {

        }
        public void loda_data()
        {
            list = ArrayList.Synchronized(new ArrayList());//以线程同步的方式创建一个arraylist
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, int.Parse(txtPoint.Text.ToString()));
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//ProtocolType.Tcp协议的类型，还有UTP协议
            mainSocket.Bind(ip);
            mainSocket.Listen(500);
            mainSocket.BeginAccept(OnClientConnection, null);

        }
        //处理过来的连接
        private void OnClientConnection(IAsyncResult ir)
        {
            try
            {
                if (mainSocket != null)
                { 
                    Socket workSocket = mainSocket.EndAccept(ir);
                    if (workSocket.RemoteEndPoint != null)
                    {
                        list.Add(workSocket);
                        ShowMsg(workSocket.RemoteEndPoint.ToString(), "连接成功", 0);
                    }
                    mainSocket.BeginAccept(OnClientConnection, null);
                    WaitForData(workSocket);
                }
            }
            catch { }
        }
        //处理过来的数据
        private void WaitForData(Socket socket)
        {
            if (callBack == null)//和某个客户端第一次连接的时候为空
            {
                callBack = OnDataReceived;
            }
            SocketBuffer buffer = new SocketBuffer();
            buffer.WorkSocket = socket;
            socket.BeginReceive(buffer.DataBuffer, 0, buffer.DataBuffer.Length, SocketFlags.None, callBack, buffer);
        }
        private void OnDataReceived(IAsyncResult ir)
        {
            SocketBuffer buffer = ir.AsyncState as SocketBuffer;
            if (buffer.WorkSocket.Connected == true)
            {
                try
                {
                    if (buffer == null)
                    {
                        return;
                    }
                    int length = buffer.WorkSocket.EndReceive(ir);//可以得到DataBuffer存放了多少数据
                    char[] ch = new char[length];
                    Decoder dec = Encoding.Default.GetDecoder();//解码器
                    dec.GetChars(buffer.DataBuffer, 0, length, ch, 0);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < ch.Length; i++)
                    {
                        sb.Append(ch[i]);
                    }
                    String s = sb.ToString();
                    ShowMsg(buffer.WorkSocket.RemoteEndPoint.ToString(), s, 2);
                    if (mainSocket != null)
                    {
                        WaitForData(buffer.WorkSocket);
                    }
                }
                catch
                {
                    ShowMsg("", "对方关闭了连接", 0);//buffer.WorkSocket.RemoteEndPoint.ToString()
                    return;
                }
            }
        }
        //发送
        private void btnSend_Click(object sender, EventArgs e)
        {
            string MsgToClient=txtMsgToClient.Text;
            if (MsgToClient != "")
            { 
                try
                {
                    Socket socket = list[0] as Socket;
                    if (socket != null)
                    {
                        byte[] data = null;
                        if (chbHexI.Checked == true)
                        {
                            data = c.strToToHexByte(MsgToClient);
                        }
                        else
                        {
                            data = Encoding.Default.GetBytes(MsgToClient);
                        }
                        socket.Send(data);
                    }
                    time = DateTime.Now.ToString("T");
                    ShowMsg(socket.RemoteEndPoint.ToString(), txtMsgToClient.Text, 1);
                    //txtMsgToClient.Text = "";
                }
                catch
                {
                    MessageBox.Show("连接已经关闭不能发送数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        public void ShowMsg(string ip, string msgg, int index)
        {
            string msg = msgg; 
            string time = DateTime.Now.ToString("T");
            switch (index)
            {
                case 0://连接成功
                    Invoke(new MethodInvoker(delegate()
                    {
                        if (ip == "")
                        {
                            txtContent.AppendText(time + ":" +msg + "\r\n");
                        }
                        else
                        {
                            txtContent.AppendText(time + ":【" + ip + "】" + msg + "\r\n");
                        }
                    }));
                    break;
                case 1: //发送
                    txtContent.AppendText(time + ":" + "我对【" + ip + "】说:" + msg + "\r\n");
                    break;
                case 2://接受
                    Invoke(new MethodInvoker(delegate()
                    {
                        if (chbHexO.Checked == true)
                        {
                            msg = c.ToHex(msgg);
                        }
                        txtContent.AppendText(time + ":【" + ip + "】对我说:" + msg + "\r\n");
                    }));
                    break;
            }
        } 
        
        public class SocketBuffer
        {
            public Socket WorkSocket;
            public byte[] DataBuffer = new byte[1024 * 1024];//存放从客户端传输过来的数据
        }
        //开启服务
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPoint.Text) > 0)
            {
                try
                {
                    loda_data();
                    ShowMsg("", "开始监听", 0);
                }
                catch
                {
                    ShowMsg("", "该端口号已经存在并且已经开启服务了", 0);
                    return;
                }
            }
            else
            {
                MessageBox.Show("端口号填写不正确");
                return;
            }
            btnStartServer.Enabled = false;
            btnCloseServer.Enabled = true;
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Socket sk = list[i] as Socket;
                        if (sk != null)
                        {
                            sk.Shutdown(SocketShutdown.Both);
                            sk.Close();
                            sk = null;
                        }
                    }
                }
                if (mainSocket != null)
                {
                    mainSocket.Close();
                    mainSocket = null;
                }
            }
            catch { }
        }

        private void btnCloseServer_Click(object sender, EventArgs e)
        {
            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Socket sk = list[i] as Socket;
                    if (sk != null)
                    {
                        sk.Shutdown(SocketShutdown.Both);
                        sk.Close();
                        sk = null;
                    }
                }
            }
            if (mainSocket != null)
            {
                mainSocket.Close();
                mainSocket = null;
            }
            btnStartServer.Enabled = true;
            btnCloseServer.Enabled = false;
            ShowMsg("", "关闭了服务", 0); 
        }
    }
}
