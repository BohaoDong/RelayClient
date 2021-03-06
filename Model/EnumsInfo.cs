﻿namespace Model
{
    public class EnumsInfo
    {
        /// <summary>
        /// 规约类型
        /// </summary>
        public enum FaithType
        {
            /// <summary>
            /// FTU非平衡式 101规约
            /// </summary>
            F101,
            /// <summary>
            /// FTU平衡式 101规约
            /// </summary>
            F101Balance,
            /// <summary>
            /// F104规约
            /// </summary>
            F104,
            /// <summary>
            /// TTU 376.1规约
            /// </summary>
            F376_1,
            /// <summary>
            /// LTU 101规约
            /// </summary>
            Ltu101,
            /// <summary>
            /// 无功补偿 376.1规约
            /// </summary>
            F3761Wgbc,
            ModBus,
            /// <summary>
            /// DTU规约
            /// </summary>
            Dtu101
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// 一级数据
            /// </summary>
            First,
            /// <summary>
            /// 没有一级数据
            /// </summary>
            NoFirst,
            /// <summary>
            /// 二级数据
            /// </summary>
            Second,
            /// <summary>
            /// 召唤激活
            /// </summary>
            ZongZhaoJH,
            /// <summary>
            /// 平衡101初始化结束帧
            /// </summary>
            Balance,
            /// <summary>
            /// 非平衡101初始化结束帧
            /// </summary>
            NoBalance

        }
        /// <summary>
        /// 客户端断开原因
        /// </summary>
        public enum DisconnectType
        {
            Normal,     // 正常退出
            Timeout,    // 超时
            Exception   // 异常
        }
        /// <summary>
        /// 客户端状态
        /// </summary>
        public enum TSessionState
        {
            Active,    // 激活
            Inactive,  // 非激活
            Shutdown,  // 被关闭的
            Closed     // 关闭的
        }

        public enum TSessinoType
        {
            /// <summary>
            /// 用户
            /// </summary>
            User,
            /// <summary>
            /// 硬件
            /// </summary>
            Device,
            /// <summary>
            /// 未知
            /// </summary>
            Unknown
        }

        public enum SoeType
        {
            遥信变位,
            遥测越限,
            开关动作,
            SOE
        }
        /// <summary>
        /// 用户控制命令类型
        /// </summary>
        public enum DevUserControlType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 对时
            /// </summary>
            DuiShi = 1,
            /// <summary>
            /// 总召
            /// </summary>
            ZongZhao = 2,
            /// <summary>
            /// 事件总召
            /// </summary>
            EventZongZhao = 3,
            /// <summary>
            /// 定值查询
            /// </summary>
            DingZhiSelect = 4,
            /// <summary>
            /// 定值设置
            /// </summary>
            DingZhiSheDing = 5,
            /// <summary>
            /// 遥控
            /// </summary>
            YaoKong = 6,
            /// <summary>
            /// 合闸预置
            /// </summary>
            HeZaYuZhi = 7,
            /// <summary>
            /// 合闸执行
            /// </summary>
            HeZhaZhiXing = 8,
            /// <summary>
            /// 合闸撤销
            /// </summary>
            HeZhaCheXiao = 9,
            /// <summary>
            /// 分闸预置
            /// </summary>
            FenZhaYuZhi = 10,
            /// <summary>
            /// 分闸执行
            /// </summary>
            FenZhaZhiXing = 11,
            /// <summary>
            /// 分闸撤销
            /// </summary>
            FenZhaCheXiao = 12,
            /// <summary>
            /// 变比查询
            /// </summary>
            BianBiChaXun = 13,
            /// <summary>
            /// 变比设置
            /// </summary>
            BianBiSheDing = 14

        }
        public enum FrameStruct
        {
            /// <summary>
            /// 链路地址
            /// </summary>
            LinkAddress,
            /// <summary>
            /// 公共地址
            /// </summary>
            PublicAddress,
            /// <summary>
            /// 传送原因
            /// </summary>
            CotCode
        }
    }
}
