namespace Model
{
    /// <summary>
    /// 类型标识符
    /// </summary>
    public enum TypeIdentification : byte
    {
        #region 监视方向过程信息(上行报文)

        /// <summary>
        /// 无效类型标识符
        /// </summary>
        IsInvalid = 0x00,
        /// <summary>
        /// 单点遥信，带品质描述，不带时标
        /// </summary>
        M_SP_NA_1 = 0x01,
        /// <summary>
        /// 双点遥信，带品质描述，不带时标
        /// </summary>
        M_DP_NA_1 = 0x03,
        /// <summary>
        /// 归一化遥测值，带品质描述，不带时标
        /// </summary>
        M_ME_NA_1 = 0x09,
        /// <summary>
        /// 标度化遥测值，带品质描述，不带时标
        /// </summary>
        M_ME_NB_1 = 0x0B,
        /// <summary>
        /// 标度化遥测值，带品质描述，带时标
        /// </summary>
        M_ME_NB_1_Time = 0x22,
        /// <summary>
        /// 单点遥信(SOE)，带品质描述，带绝对时标
        /// </summary>
        M_SP_TB_1 = 0x1E,
        /// <summary>
        /// 双点遥信(SOE)，代码品质描述，带绝对时标
        /// </summary>
        M_DP_TB_1 = 0x1F,
        /// <summary>
        /// 遥设成功时硬件返回确认  
        /// </summary>
        M_ME_NA_1_YS = 0x30,
        /// <summary>
        /// 遥脉报文
        /// </summary>
        M_ME_NA_1_YM = 0x0F,

        #endregion

        #region 控制方向过程信息(上、下行)

        /// <summary>
        /// 单点遥控，每个报文只能包含一个遥控信息体
        /// </summary>
        C_SC_NA_1 = 0x2D,

        #endregion

        #region 监视方向系统命令(上行)
        /// <summary>
        /// 初始化结束  0x46  70
        /// </summary>
        M_EI_NA_1 = 0x46,

        #endregion

        #region 控制方向系统命令(上、下行)

        /// <summary>
        /// 站召唤命令，带不同的限定词，可以用于组召唤
        /// </summary>
        C_IC_NA_1 = 0x64,

        /// <summary>
        /// 遥脉召唤
        /// </summary>
        C_IC_NA_1_YM = 0x65,
        /// <summary>
        /// 时钟同步,需要通过测量通道延时加以校正   103   0x67
        /// </summary>
        C_CS_NA_1 = 0x67,
        /// <summary>
        /// 时钟延时获得与延时传递 0x6A
        /// </summary>
        YSCD_C_CD_NA_1 = 0x6A,
        /// <summary>
        /// 平衡式测试帧
        /// </summary>
        BalanceTestFrame=0x68,
        /// <summary>
        /// 平衡式标度化遥设报文类型 49 0x31
        /// </summary>
        BalanceYSFrameType= 0x31,
        #endregion
    }
}
