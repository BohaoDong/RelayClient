using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 标示类,用来标示遥信遥测数据是否已经上传
    /// </summary>
    public class BiaoShiInfo
    {
        /// <summary>
        /// 遥信标示
        /// </summary>
        public bool YxTrue { get; set; }
        /// <summary>
        /// 遥测标示
        /// </summary>
        public bool YcTrue { get; set; }
        /// <summary>
        /// 查询遥信时间标示
        /// </summary>
        public DateTime YxCreateTime { get; set; } 
        /// <summary>
        /// 查询遥测时间标示
        /// </summary>
        public DateTime YcCreateTime { get; set; }
        /// <summary>
        /// 是否为断电第一次重连
        /// </summary>
        public bool FirstConnect { get; set; }
        /// <summary>
        /// 判断召唤是不是开始了默认为false(没开始)
        /// </summary>
        public bool ZhaoHuan { get; set; }
        /// <summary>
        /// 遥信报文
        /// </summary>
        public byte[] YxBaoWen { get; set; }
        /// <summary>
        /// 遥测报文
        /// </summary>
        public byte[] YcBaoWen { get; set; }
        /// <summary>
        /// SOE报文
        /// </summary>
        public byte[] SoeBaoWen { get; set; }
        /// <summary>
        /// SOE时间标示
        /// </summary>
        public string SoeCreateTime { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DevCode { get; set; }
        /// <summary>
        /// 控制域
        /// </summary>
        public byte LineCtrl { get; set; }
        /// <summary>
        /// 初始化结束
        /// </summary>
        public bool ChuShiHuaEnd { get; set; }
        public AsyncCallback Callback { get; set; }
        /// <summary>
        /// 总换激活(true为已激活)
        /// </summary>
        public bool ZongZhaoJh { get; set; }
        /// <summary>
        /// 发送心跳包为true链路确认为false,证明链路仍在连接,否则需要重新连接
        /// </summary>
        public bool ConnectConfirm { get; set; }
        /// <summary>
        /// 存储接受的报文
        /// </summary>
        public byte[] BttBytes { get; set; }
        /// <summary>
        /// 具体通讯类
        /// </summary>
        public object SessionBase104 { get; set; }
    }
}
