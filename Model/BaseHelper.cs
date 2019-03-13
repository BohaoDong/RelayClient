using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public static class BaseHelper
    {
        #region   非平衡相关帧信息
        /// <summary>
        /// 链路初始化控制域
        /// </summary>
        public const byte s_RequestLinkState = 0x49;
        /// <summary>
        /// 链路复位控制域静态字段
        /// </summary>
        public const byte s_ResetRemoteLink = 0x40;
        /// <summary>
        /// 链路复位控制域确认静态字段
        /// </summary>
        public const byte s_ResetRemoteLinkOK = 0x20;
        /// <summary>
        /// 请求链路状态 
        /// </summary>
        public const byte s_ResetRemoteLinkState = 0x0B;
        /// <summary>
        /// 召唤一级数据,FCB =0
        /// </summary>
        public const byte s_CallFirstDataFCBOrZero = 0x5A;
        /// <summary>
        /// 召唤一级数据,FCB =1
        /// </summary>
        public const byte s_CallFirstDataFCBOrOne = 0x7A;
        /// <summary>
        /// 召唤二级数据,FCB =0
        /// </summary>
        public const byte s_CallSecondDataFCBOrZero = 0x5B;
        /// <summary>
        /// 召唤二级数据,FCB =1
        /// </summary>
        public const byte s_CallSecondDataFCBOrOne = 0x7B;
        /// <summary>
        /// 总召唤或分组召唤等,FCB =1时发  站硬件召唤时第一次需要发0x73  硬件通常先发53
        /// </summary>
        public const byte s_CallSiteZero = 0x53;
        /// <summary>
        /// 总召唤或分组召唤等,FCB =0时发
        /// </summary>
        public const byte s_CallSiteOne = 0x64;
        /// <summary>
        /// 起始长帧固定值
        /// </summary>
        public const int Long_startCode = 0x68;
        /// <summary>
        ///  结束长帧固定值
        /// </summary>
        public const int Long_endCode = 0x16;
        /// <summary>
        /// 起始短帧固定值
        /// </summary>
        public const int Short_startCode = 0x10;
        /// <summary>
        ///  结束短帧固定值
        /// </summary>
        public const int Short_endCode = 0x16;
        /// <summary>
        ///  心跳包控制欲
        /// </summary>
        public const int Short_heartBeatCode = 0x6C;

        #endregion

        #region 非平衡链路控制码的默认值
        public const int PRMX = 1;
        /// <summary>
        /// 判断FCB位是否翻转,决定是否重发上一帧
        /// </summary>
        public const int FCB = 0;//
        /// <summary>
        /// ==0 决定FCB位是无效的,==1有效
        /// </summary>
        public const int FCV = 0;//
        public const int PRMS = 0;
        public const int ACD = 1;//==1表示有1级数据
        public const int DFC = 1;//==1表示被控站不能接受后续数据
        /// <summary>
        /// 复位远方电路
        /// </summary>
        public const int Ctrl0 = 0;//
        /// <summary>
        /// 请求链路状态
        /// </summary>
        public const int Ctrl9 = 9;//
        /// <summary>
        /// 请求一级数据
        /// </summary>
        public const int Ctrl10 = 10;//
        /// <summary>
        /// 请求二级数据
        /// </summary>
        public const int Ctrl11 = 11;//
        /// <summary>
        /// 处理心跳包
        /// </summary>
        public const int Ctrl12 = 12;//
        #endregion 

        #region 平衡相关帧信息
        /// <summary>
        /// 请求链路状态
        /// </summary>
        public const byte PH_RequestLinkState = 0xC9;
        /// <summary>
        /// 复位远方链路
        /// </summary>
        public const byte PH_ResetFarLink = 0xC0;
        /// <summary>
        /// 响应链路启动肯定确认
        /// </summary>
        public const byte PH_LinkSureConfirm = 0x8B;
        /// <summary>
        /// 所有平衡式肯定确认
        /// </summary>
        public const byte PH_SureConfirm = 0x80;
        /// <summary>
        /// 总召唤或分组召唤 延时命令获得等,FCB =0时发  主站召唤时第一次需要发0xF3  也适用时钟同步 硬件来定
        /// </summary>
        public const byte PH_CallSiteOne = 0xF3;

        /// <summary>
        /// 总召唤或分组召唤延时命令获得等,FCB =1时发  主站召唤时第一次需要发0xF3   也适用时钟同步 硬件来定
        /// </summary>
        public const byte PH_CallSiteZero = 0xD3;

        /// <summary>
        /// 测试帧,FCB =0时发  主站测试帧第一次需要发0xF2  硬件来定
        /// </summary>
        public const byte PH_TestZero = 0xF2;
        /// <summary>
        /// 测试帧,FCB =1时发  主站测试帧第一次需要发0xF2  硬件来定
        /// </summary>
        public const byte PH_TestOne = 0xD2;



        #endregion

        #region 控制欲常量
        /// <summary>
        /// 测量值，规一化值 （遥测）
        /// </summary>
        public const byte LineCtrl73 = 0x73;
        /// <summary>
        /// 测量值，规一化值 （遥测）
        /// </summary>
        public const byte LineCtrl53 = 0x53;/// <summary>
        /// 测量值，规一化值 （遥测）
        /// </summary>
        public const byte LineCtrl28 = 0x28;
        /// <summary>
        /// 测量值，规一化值 （遥测）
        /// </summary>
        public const byte LineCtrl7308 = 0x08;
        #endregion

        #region 类型标示常量 
        /// <summary>
        /// 单点信息（单点遥信） 
        /// </summary>
        public const byte M_SP_NA_1 = 0x01;
        /// <summary>
        /// 测量值，规一化值 （遥测）
        /// </summary>
        public const byte M_ME_NA_1 = 0x09;
        /// <summary>
        /// 测量值，标度化值（遥测）
        /// </summary>
        public const byte M_ME_NB_1 = 0x0B;
        public const byte SOE = 0x1E;
        #endregion

        #region 传送原因常量
        /// <summary>
        /// 响应站召唤
        /// </summary>
        public const byte introgen = 0x14;
        #endregion

        #region 链路地址的位数1位八进制数还是2位八进制数
        public static int LinkaddressLength;
        /// <summary>
        /// 链路地址的位数1位八进制数还是2位八进制数_非平衡101
        /// </summary>
        public static int FB101LinkaddressLength = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FB101LinkaddressLength"]);
        /// <summary>
        /// 平衡101地址的长度
        /// </summary>
        public static int B101LinkaddressLength = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["B101LinkaddressLength"]);//地址的长度
        public static int ReConnectTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ReConnectTime"]);
        public static int HeartText = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HeartText"]);
        public static int SendBlance_101SOETime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SendBlance_101SOETime"]);
        #endregion
    } 
}
