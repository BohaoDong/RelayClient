using Ulity;

namespace FaithProject.FTU_101.FaithStruct
{
    /// <summary>
    /// COT传送原因类
    /// </summary>
    public class CotCode
    {
        #region  
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initCode">默认初始化值</param>
        public CotCode(byte[] initCode)
        {
            TransferReason = initCode[0] & 0x3F;
            IsTest = Helper.GetIntegerBit(initCode[0], 7);
            IsPn = Helper.GetIntegerBit(initCode[0], 6);
            GetCotCode = initCode[0];
        }

        /// <summary>
        /// 试验标识，0未试验，1试验
        /// </summary>
        public int IsTest { get; set; }

        /// <summary>
        /// 确认标识P/N，0肯定确认，1否定确认
        /// </summary>
        public int IsPn { get; set; }

        /// <summary>
        /// 传送原因
        /// </summary>
        public int TransferReason { get; set; }

        /// <summary>
        /// 获取传送原因
        /// </summary>
        /// <returns></returns>
        public byte GetCotCode
        {
            get;
            set;
        }
    }
}
