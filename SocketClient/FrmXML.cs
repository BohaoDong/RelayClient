using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Model;
using SocketClient.SocketBLL;

namespace SocketClient
{
    public partial class FrmXml : Form
    {
        public FrmXml()
        {
            InitializeComponent();
        } 
        public List<IpConfiginfo> list;
        OperationXml xml = new OperationXml();
             
        //创建xml文件_文件目录在\bin\debug文件下
        private void btnCreate_Click(object sender, EventArgs e)
        {
            lblFileAddress.Text = xml.CreateXml();
            Load_Data();
        }
        //添加按钮事件
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (lblFileAddress.Text == "文件地址")
            {
                MessageBox.Show("请先创建文件才能添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtInsert.Text))
            {
                MessageBox.Show("请输入IP跟端口!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IpConfiginfo info = new IpConfiginfo();
            info.Ip=txtInsert.Text;
            xml.AddXmlNodeInformation(info, lblFileAddress.Text);
            Load_Data();
            txtInsert.Text = "";
        }

        //更新按钮事件
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInsert.Text))
            {
                MessageBox.Show("双击选择修改的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(XiugaiData))
            {
                MessageBox.Show("双击选择修改的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xml.Updte_Data(lblFileAddress.Text,XiugaiData,txtInsert.Text);
            txtInsert.Text = "";
            Load_Data();
            XiugaiData = "";
        } 
        //确定按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            Address = lblFileAddress.Text;
            DialogResult = DialogResult.OK;
        }  
        public string XiugaiData="";//记录修改的内容
        /// <summary>
        /// datagridview1双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;//获取鼠标所在的行值（索引）
                XiugaiData =dataGridView1[0, row].Value.ToString();
                txtInsert.Text =dataGridView1[0, row].Value.ToString(); //获取[列值(索引),行值(索引)] 的数据
            }
            catch
            {
                // ignored
            }
        } 
        /// <summary>
        /// 记录曾经打开的ipConfig.xml文件
        /// </summary>
        public string Address { get; set; }
        private void FrmXML_Load(object sender, EventArgs e)
        {
            string path = Application.ExecutablePath;
            path = path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
            lblFileAddress.Text = path + @"\Config\ipConfig.xml";
            if (!string.IsNullOrEmpty(Address))
            {
                lblFileAddress.Text = Address;
                Load_Data();
            }
            else {
                string bb = path + @"\Config\ipConfig.xml";
                if (File.Exists(@bb))
                {
                    lblFileAddress.Text = bb;
                    Load_Data();
                } 
            }
        }
        public string ShanChuData;
        /// <summary>
        ///  删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {  
            List<string> listSelect = new List<string>();
            for (int i=0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)//必须把选中行属性改成true
                {
                    listSelect.Add(dataGridView1.Rows[i].Cells[0].Value.ToString()); 
                }
            }
            if (listSelect.Count==0)
            {
                MessageBox.Show("请选择删除的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xml.Del_Data(listSelect, lblFileAddress.Text);
            Load_Data();
        }
        /// <summary>
        /// 显示数据_把xml文件中的数据显示出来
        /// </summary>
        public void Load_Data()
        {
            list = xml.Read_Data(list, lblFileAddress.Text);
            dataGridView1.DataSource = list;
            dataGridView1.Columns["Ip"].Width = 184; 
        } 
    }
}
