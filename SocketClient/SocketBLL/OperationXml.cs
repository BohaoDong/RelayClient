using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Model;

namespace SocketClient.SocketBLL
{
    public class OperationXml
    { 
        /// <summary>
        /// ip xml文件的路径
        /// </summary>
        private string _path = "";
        /// <summary>
        /// 创建XML文件
        /// </summary>
        /// <returns>返回文件的地址</returns>
        public string CreateXml()
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            _path = Application.ExecutablePath;
            _path=_path.Substring(0,_path.LastIndexOf("\\", StringComparison.Ordinal));
            XmlWriter writer = XmlWriter.Create(_path + "\\Config\\ipConfig.xml", xws); 
            writer.WriteStartElement("Roots");
            writer.WriteEndElement();
            writer.Close();
            return _path + "\\Config" + "\\ipConfig.xml";
        }

        /// <summary>
        /// 读取xml文件中的数据
        /// </summary>
        /// <param name="list">传的空list</param>
        /// <param name="address">xml文件的地址</param>
        public List<IpConfiginfo> Read_Data(List<IpConfiginfo> list, string address)
        {
            try
            {
                XmlReader reader = XmlReader.Create(address);//"ipConfig.xml"
                list = new List<IpConfiginfo>();   //xml通过reader读取出来的数据就相当于list 类就相当于类，子标签内容就相当于类的内容
                while (reader.Read())//读取的时候是一个标签一个标签读取的
                {
                    if (string.Equals(reader.Name, "Ip"))
                    {
                        var info = new IpConfiginfo();
                        info.Ip = reader.ReadElementContentAsString();//.Value;  readelementstring()
                        list.Add(info);
                    }
                }
                reader.Close();
                return list;
            }
            catch
            {
                return list;
            }
        } 
        /// <summary>
        /// 修改XML文件
        /// </summary>
        /// <param name="address">文件的地址</param>
        /// <param name="xiugaiData">修改前的数据</param>
        /// <param name="content">修改后的数据</param>
        public void Updte_Data(string address, string xiugaiData, string content)
        {
            XmlDocument doc = new XmlDocument();//节点
            doc.Load(address);
            XmlNode roots = doc["Roots"];
            for (int i = 0; i < roots.ChildNodes.Count; i++)
            {
                if (string.Equals(roots.ChildNodes[i].ChildNodes[0].InnerText, xiugaiData))
                { 
                    roots.ChildNodes[i].ChildNodes[0].InnerText = content;
                }
            }
            doc.Save(address); 
        }
        /// <summary>
        /// 删除XML数据
        /// </summary>
        /// <param name="list">要删除的数据集合</param>
        /// <param name="address">文件地址</param>
        public void Del_Data(List<string> list, string address)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(address);
            XmlNode roots = doc["Roots"]; 
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < roots.ChildNodes.Count; i++)
                {
                    if (string.Equals(roots.ChildNodes[i].ChildNodes[0].InnerText, list[j]))
                    {
                        roots.RemoveChild(roots.ChildNodes[i]);//必须同过跟节点删除子节点，必须他杀不能自杀。 
                        i--;
                    }
                } 
            }
            doc.Save(address);
        }     
        /// <summary>
        /// 向xml文件中添加节点
        /// </summary>
        /// <param name="info"></param>
        /// <param name="xmlPath"></param>
        public bool AddXmlNodeInformation(IpConfiginfo info, string xmlPath)
        {
            try
            {
                //定义并从xml文件中加载节点（根节点）
                XElement rootNode = XElement.Load(xmlPath);
                //定义一个新节点
                XElement newNode = new XElement("Ip", info.Ip);
                //将此新节点添加到根节点下
                rootNode.Add(newNode);
                //保存对xml的更改操作
                rootNode.Save(xmlPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
    }
}
