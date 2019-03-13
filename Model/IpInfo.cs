namespace Model
{
    /// <summary>
    /// 应答传送对象类
    /// </summary>
    public class IpInfo
    {
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 字符串消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 消息的类型,系统消息0,发送消息为1,接收消息为2
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 字节消息
        /// </summary>
        public byte[] Buf { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DevCode { get; set; }

        public void DisPoseResource()
        {
            Buf = null;
            Msg = null;
            Ip = null;
        }
    }
}
