using System;

namespace FaithProject.FTU_101.FaithStruct.Frame
{
    abstract public class FrameBase
    { 
        #region 平衡相关帧信息
        /// <summary>
        /// 请求链路状态
        /// </summary>
        public const byte PhRequestLinkState = 0xC9;
        public const byte ZzphRequestLinkState = 0x49;
        /// <summary>
        /// 复位远方链路
        /// </summary>
        public const byte PhResetFarLink = 0xC0;
        /// <summary>
        /// 响应链路启动肯定确认
        /// </summary>
        public const byte PhLinkSureConfirm = 0x8B;
        public const byte ZzphLinkSureConfirm = 0x0B;
        /// <summary>
        /// 所有平衡式肯定确认
        /// </summary>
        public const byte PhSureConfirm = 0x80;
        public const byte ZzphSureConfirm = 0x40;
        /// <summary>
        /// 总召唤或分组召唤 延时命令获得等,FCB =0时发  主站召唤时第一次需要发0xF3  也适用时钟同步 硬件来定
        /// </summary>
        public const byte PhCallSiteOne = 0xF3;

        /// <summary>
        /// 总召唤或分组召唤延时命令获得等,FCB =1时发  主站召唤时第一次需要发0xF3   也适用时钟同步 硬件来定
        /// </summary>
        public const byte PhCallSiteZero = 0xD3;

        /// <summary>
        /// 测试帧,FCB =0时发  主站测试帧第一次需要发0xF2  硬件来定
        /// </summary>
        public const byte PhTestZero = 0xF2;
        /// <summary>
        /// 测试帧,FCB =1时发  主站测试帧第一次需要发0xF2  硬件来定
        /// </summary>
        public const byte PhTestOne = 0xD2;

        public const byte ZzphTest = 0x00;

        #endregion

        #region   非平衡相关帧信息
        /// <summary>
        /// 链路初始化控制域
        /// </summary>
        public const byte SRequestLinkState = 0x49;
        /// <summary>
        /// 链路复位控制域静态字段
        /// </summary>
        public const byte SResetRemoteLink = 0x40;
        /// <summary>
        /// 召唤一级数据,FCB =0
        /// </summary>
        public const byte SCallFirstDataFcbOrZero = 0x5A;

        /// <summary>
        /// 召唤一级数据,FCB =1
        /// </summary>
        public const byte SCallFirstDataFcbOrOne = 0x7A;
        /// <summary>
        /// 召唤二级数据,FCB =0
        /// </summary>
        public const byte SCallSecondDataFcbOrZero = 0x5B;

        /// <summary>
        /// 召唤二级数据,FCB =1
        /// </summary>
        public const byte SCallSecondDataFcbOrOne = 0x7B;

        /// <summary>
        /// 总召唤或分组召唤等,FCB =1时发  站硬件召唤时第一次需要发0x73  硬件通常先发53
        /// </summary>
        public const byte SCallSiteZero = 0x53;

        /// <summary>
        /// 总召唤或分组召唤等,FCB =0时发
        /// </summary>
        public const byte SCallSiteOne = 0x73;

        /// <summary>
        /// 起始长帧固定值
        /// </summary>
        public const int LongStartCode = 0x68;
        /// <summary>
        ///  结束长帧固定值
        /// </summary>
        public const int LongEndCode = 0x16;


        /// <summary>
        /// 起始短帧固定值
        /// </summary>
        public const int ShortStartCode = 0x10;
        /// <summary>
        ///  结束短帧固定值
        /// </summary>
        public const int ShortEndCode = 0x16;

        #endregion
        /// <summary>
        /// 起始帧固定值
        /// </summary>
        public int StartCode
        {
            get;
            set;
        }
        /// <summary>
        /// 完整报文
        /// </summary>
        public byte[] Buf { get; set; }
        /// <summary>
        /// 是否需要记录报文
        /// </summary>
        public bool IsWriteLog
        {
            get;
            set;
        }
        /// <summary>
        ///  结束帧固定值
        /// </summary>
        public int EndCode
        {
            get;
            set;
        }
        /// <summary>
        /// 报文内容长度
        /// </summary>
        public byte MsgLengthCode
        {
            get;
            set;
        }
        private byte[] _linkAddress;
        /// <summary>
        /// 公共地址
        /// </summary>
        public byte[] PublicAddress { get; set; }
        /// <summary>
        /// 链路地址
        /// </summary>
        public byte[] LinkAddress
        {
            get
            {
                if (_linkAddress == null)
                {
                    _linkAddress = new byte[2];
                }
                return _linkAddress;
            }
            set
            {
                _linkAddress = value;
            }
        }
        /// <summary>
        /// 协议处理外观类
        /// </summary>
        public Faith101Base Faith101
        {
            get;
            set;
        }
        /// <summary>
        /// 判断是否为平衡式数据帧
        /// </summary>
        public bool IsBalanceFrame
        {
            get;
            set;
        }  
        /// <summary>
        /// 校验码
        /// </summary>
        public byte CheckCode
        {
            get;
            set;
        }
        /// <summary>
        /// 验证报文校验码
        /// </summary>
        /// <param name="message">校验的报文</param>
        /// <returns>校验码整齐</returns>
        public static bool IsValidMessageCheckCode(byte[] message)
        {
            //参数为null或元素为0时抛出一个异常
            if (message == null || message.Length <= 0) throw new Exception();

            byte checkCode = 0;
            //默认为短帧 校验码从第二位开始算起
            int index = 1;
            //长帧，从索引4开始（第五位）
            if (message.Length > 6) index = 4;
            for (int i = index; i < message.Length - 2; i++)
            {
                checkCode += message[i];
            }
            return checkCode == message[message.Length - 2];
        }
        /// <summary>
        /// 资源释放方法
        /// </summary>
        public virtual void MyDispose()
        {
            if (AsduCode != null)
                AsduCode.MyDispose();
            LinkAddress = null;
        }
        /// <summary>
        /// 应用服务数据单元(Application services data Unit)
        /// </summary>
        public Asdu AsduCode
        {
            get;
            set;
        }
    }
}