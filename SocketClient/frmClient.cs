using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using BLL;
using Model;
using Model.SessionEventArgs;
using SocketClient.SMYHSocket.TSession;
using SocketClient.SocketBLL;
using Ulity;
using Ulity.ErrorLog;

namespace SocketClient
{
    public partial class FrmClient : Form
    {
        /// <summary>
        /// 通讯具体操作类
        /// </summary>
        public SessionBase104 Tb;
        /// <summary>
        /// 通讯执行类
        /// </summary>
        public Session Ts =new Session();
        public FrmClient()
        {
            InitializeComponent();
        } 
        /// <summary>
        /// FrmXML窗体中穿过来的IP集合
        /// </summary>
        public List<IpConfiginfo> List; 
       /// <summary>
       /// 断开连接按钮
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnBreak_Click(object sender, EventArgs e)
        {
            try
            {
                btnBreak.Enabled = false;
                btnConn.Enabled = true;
                _devList = null;
                Ts.StopConn = true; 
                Ts.ReConnThread.Abort();
                if (Ts.SoeThread != null) Ts.SoeThread.Abort();
                Ts.TsocketThread.Abort();
                Tb = null;
                var item = listBox1.Items[0];
                listBox1.Items.Clear();
                listBox1.Items.Add(item);
                foreach ( var t in DicBiaoShiInfo.dic.Keys.Select(key => DicBiaoShiInfo.dic[key].SessionBase104 as SessionBase104)
                            .Where(t => t != null))
                {
                    t.WorkSocket.Close();
                    t.MainSocket.Close();
                    t.Conn = false; 
                    t.DisPoseResource();
                    DicBiaoShiInfo.dic[t.DevCode].FirstConnect = false;
                    ShowMsg(t.DevCode, "主动关闭了连接", 0);
                }  
            }
            catch(Exception ex)
            {
                WriteErrorLog.WriteToFile(ex.Message);
            }
        }
        /// <summary>
        /// 设备编号集合
        /// </summary>
        private List<string> _devList;
        /// <summary>
        /// 连接服务器按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count==0)
                {
                    MessageBox.Show("请先添加Ip!", "提示!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                btnConn.Enabled = false;
                btnBreak.Enabled = true;
                Ts.StopConn = false;
                Ts.FaithValueName=cmbGuiyue.SelectedValue.ToString();
                _devList = GetDevBll.GetAllDevList(Convert.ToInt32(cmbGuiyue.SelectedIndex));
                for (var i = 0; i < _devList.Count; i++) //  _devList.Count
                {
                    AddDev(_devList[i]);
                    Tb = new SessionBase104();
                    Tb.ShowDevStateEvent += Tb_ShowDevStateEvent;
                    Tb.FaithValueName = cmbGuiyue.SelectedValue.ToString();
                    Tb.Ip = ((ListBoxItem)listBox1.Items[0]).ItemName;
                    Tb.DevCode = _devList[i];
                    DicBiaoShiInfo.dic[_devList[i]].SessionBase104 = Tb;
                    DicBiaoShiInfo.dic[_devList[i]].SoeCreateTime = DateTime.Now.AddYears(-1).ToString(CultureInfo.InvariantCulture);
                    var i1 = i+1;
                    var i2 = i;
                    Invoke(new MethodInvoker(delegate
                    { 
                        listBox1.Items.Add(new ListBoxItem(){ItemName = i1 + "设备:" + _devList[i2],ItemValue = _devList[i2]});
                    }));
                }
                Ts.CreateSocket();
            }
            catch (Exception ex)
            {
                WriteErrorLog.WriteToFile(ex.Message);
            }
        }
        
        /// <summary>
        /// 显示未上线的设备
        /// </summary>
        /// <param name="devState"></param>
        /// <param name="devCode"></param>
        private void Tb_ShowDevStateEvent(bool devState, string devCode)
        {
            if (devCode != null)
            {
                Invoke(new MethodInvoker(delegate
                {
                    for (var i = 0; i < listBox1.Items.Count; i++)
                    {
                        var item = listBox1.Items[i] as ListBoxItem;
                        if (item != null)
                        {
                            if (item.ItemName.Contains("在线") || item.ItemName.Contains("离线"))
                            {
                                item.ItemName = item.ItemName.Substring(0, item.ItemName.Length - 2);
                            }
                            if (item.ItemValue == devCode)
                            {
                                if (devState)
                                    listBox1.Items[i] = new ListBoxItem()
                                    {
                                        ItemName = item.ItemName + "在线",
                                        ItemValue = item.ItemValue
                                    };
                                else
                                    listBox1.Items[i] = new ListBoxItem()
                                    {
                                        ItemName = item.ItemName + "离线",
                                        ItemValue = item.ItemValue
                                    };
                                listBox1.SelectedIndex = i;
                            }
                        } 
                    }
                }));
            }
        }

        #region 发送消息
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMsgToServer.Text != "")
            {
                try
                {
                    if (listBox1.SelectedIndex != 0)
                    {
                        var sendData = Helper.StrToHexByte(txtMsgToServer.Text);
                        var item = listBox1.Items[listBox1.SelectedIndex] as ListBoxItem;
                        if (item != null)
                        {
                            var sessionBase104 = DicBiaoShiInfo.dic[item.ItemValue].SessionBase104 as SessionBase104;
                            if (sessionBase104 != null) sessionBase104.SendDatagram(sendData);
                            ShowMsg(item.ItemValue, txtMsgToServer.Text, 1);
                            txtMsgToServer.Text = "";
                        } 
                    }
                }
                catch
                {
                    if (chbHexI.Checked)
                    {
                        MessageBox.Show("请输入正确的十六进制数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        #endregion

        #region 显示消息
        public int Num;

        /// <summary>
        /// 显示消息
        /// </summary> 
        /// <param name="devCode"></param>
        /// <param name="msgg"></param>
        /// <param name="index"></param>
        public void ShowMsg(string devCode, string msgg, int index)
        {
            if (!ckbDebugMode.Checked) return;
            Invoke(new MethodInvoker(delegate
            {
                if (Num == 1000)
                {
                    txtContent.Text = "";
                    Num = 0;
                }
                Num++;
                string msg = msgg;
                string time = DateTime.Now.ToString("T");
                switch (index)
                {
                    case 0://连接(不)成功 
                        if (devCode == "")
                        {
                            txtContent.AppendText(Num + " " + time + ":" + msg + "\r\n");
                        }
                        else
                        {
                            txtContent.AppendText(Num + " " + time + ":【" + devCode + "】" + msg + "\r\n");
                        }
                        break;
                    case 1: //发送 
                        txtContent.AppendText(Num + " " + time + ":" + "我对【" + devCode + "】说:" + msg + "\r\n");
                        break;
                    case 2://接受   
                        txtContent.AppendText(Num + " " + time + ":【" + devCode + "】对我说:" + msg + "\r\n");
                        break;
                }
            }));
        }
        #endregion

        private void ShowData(object sender, SessionReceive_DeviceReply_EventArgs e)
        {
            ShowMsg(e.IpData.DevCode, e.IpData.Msg, e.IpData.Index);
        } 

        #region 窗体加载事件
        //加载客户端Ip(窗体加载)
        private void Client_Load(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);//得到IP地址
                txtContent.Text += "所在电脑IP:" + ip + "\r\n";
            }
            catch
            {
                // ignored
            }
            OperationXml xml = new OperationXml(); 
            List = null; 
            DicBiaoShiInfo.dic.Clear();
            if (List != null && List.Count > 0)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    listBox1.Items.Add(List[i].Ip); 
                }
            }
            dock_from();
            InitSocketMethod();
            //给规约下拉列表框添加内容
            AddGuiYue();
            listBox1.ValueMember = "ItemVale";
            listBox1.DisplayMember = "ItemName";
        }
        /// <summary>
        /// 在规约下拉列表框中添加规约数据
        /// </summary>
        public void AddGuiYue()
        {
            List<CmbGuiYueInfo> list = new List<CmbGuiYueInfo>();
            var info = new CmbGuiYueInfo()
            {
                GuiYueName = "网口非平衡",
                GuiYueValue = "F101"
            };
            list.Add(info);
            info = new CmbGuiYueInfo()
            {
                GuiYueName = "网口平衡",
                GuiYueValue = "F101_Balance"
            };
            list.Add(info);
            cmbGuiyue.DataSource = list;
            cmbGuiyue.DisplayMember = "GuiYueName";
            cmbGuiyue.ValueMember = "GuiYueValue";
            cmbGuiyue.SelectedIndex = 0;
        }
        #endregion

        #region 创建程序停靠任务栏
        /// <summary>
        /// 创建程序停靠任务栏
        /// </summary>
        public void dock_from()
        {
            //生成4个菜单项对象，显示文本分别为"显示窗口"、"隐藏窗口"、"执行程序"、"退出程序"  
            MenuItem menuItem1 = new MenuItem("显示窗口");
            MenuItem menuItem2 = new MenuItem("隐藏窗口");
            MenuItem menuItem4 = new MenuItem("退出程序");
            //分别为4个菜单项添加Click事件响应函数  
            menuItem1.Click += menuItem1_Click;
            menuItem2.Click += menuItem2_Click;
            menuItem4.Click += menuItem4_Click;
            //设置NotifyIcon对象的ContextMenu属性为生面的弹出菜单对象  
            转发系统.ContextMenu = new ContextMenu(new[] { menuItem1, menuItem2, menuItem4 });
            //当用户双击程序图标时将执行相应的函数  
            转发系统.DoubleClick += notifyIcon1_DoubleClick;
        }
        private void menuItem1_Click(object sender, EventArgs e)//“显示窗口”菜单的响应方法  
        {
            if (Visible == false) Visible = true;//假如当前窗口没显示则显示当前窗口  
        }
        private void menuItem2_Click(object sender, EventArgs e)//"隐藏窗口"菜单的响应方法  
        {
            if (Visible) Visible = false;//假如当前窗口为显示的则隐藏窗口  
        }
        private void menuItem4_Click(object sender, EventArgs e)//“退出程序”菜单的响应方法  
        {
            Close();//关闭当前对象(即窗体)  
            Application.Exit();//通过Application类的静态方法Exit()退出应用程序  
        }

        #endregion

        #region 按钮
        /// <summary>
        /// ipConfig.xml文件的地址
        /// </summary>
        public string Address;
        /// <summary>
        /// 配置xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXML_Click(object sender, EventArgs e)
        {
            FrmXml frm = new FrmXml();
            frm.Address = Address;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Address = frm.Address;
                List = null;
                List = frm.list;
                listBox1.Items.Clear(); 
                DicBiaoShiInfo.dic.Clear();
                if (List != null && List.Count > 0)
                {
                    for (int i = 0; i < List.Count; i++)
                    { 
                        listBox1.Items.Add(new ListBoxItem(){ItemName = List[i].Ip, ItemValue = List[i].Ip}) ; 
                    }
                }
            }
        }
        /// <summary>
        /// 十六进制输入复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbHexI_CheckedChanged(object sender, EventArgs e)
        {
            if (chbHexI.Checked)
            {
                txtMsgToServer.Text = Helper.ToHex(txtMsgToServer.Text);
            }
            else
            {
                txtMsgToServer.Text = Helper.UnHexToCh(txtMsgToServer.Text);
            }
        }
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)//当用户点击窗体右上角X按钮或(Alt + F4)时 发生          
            {
                e.Cancel = true;
                Hide();
            }
        }
        /// <summary>
        /// 停靠图标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
        }
        #endregion
        /// <summary>
        /// 订阅事件
        /// </summary>
        private void InitSocketMethod()
        {
            Ts.ShowData += ShowData;
        }

        /// <summary>
        /// 给每个DevCode创造自己的标示
        /// </summary> 
        /// <param name="devCode"></param>
        public void AddDev(string devCode)
        {
            try
            {
                var bsi = new BiaoShiInfo();
                DicBiaoShiInfo.dic[devCode]= bsi;
            }
            catch
            {
                // ignored
            }
        }
        /// <summary>
        /// 允许Ctrl+A全选所有文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }  
        }
    }
}
