using Ulity;

namespace FaithProject.FTU_101.FaithStruct
{
    /// <summary>
    /// 可变结构限定词类
    /// </summary>
    public class StructLimitCode
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initCode">初始化可变结构限定词</param>
        public StructLimitCode(int initCode)
        {
            Sq = Helper.GetIntegerBit(initCode, 7);
            InfoPointCount = initCode & 0x7F;
            GetLimitCode = (byte)initCode;
        }

        /// <summary>
        /// SQ连续还是不连续
        /// </summary>
        public int Sq { get; set; }

        /// <summary>
        /// 信息点数量
        /// </summary>
        public int InfoPointCount { get; set; }

        /// <summary>
        /// 获取可变结构限定词
        /// </summary>
        /// <returns></returns>
        public byte GetLimitCode
        {
            get;
            set;
        }
    }
}
