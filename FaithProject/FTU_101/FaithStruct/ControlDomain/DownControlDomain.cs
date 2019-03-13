using Ulity;

namespace FaithProject.FTU_101.FaithStruct.ControlDomain
{
    /// <summary>
    /// 下行控制域
    /// </summary>
    public class DownControlDomain : ControlDomainBase
    {
        /// <summary>
        /// 帧计数位
        /// </summary>
        public int Fcb { get; set; }

        /// <summary>
        /// 帧计数有效位 默认为0
        /// </summary>
        public int Fcv { get; set; }

        /// <summary>
        /// 下行控制域构造函数
        /// </summary>
        /// <param name="initValue">控制域值</param>
        public DownControlDomain(byte initValue)
            : base(initValue)
        {
            Prm = Helper.GetIntegerBit(ControlDomainCode, 6);
            Fcb = Helper.GetIntegerBit(ControlDomainCode, 5);
            Fcv = Helper.GetIntegerBit(ControlDomainCode, 4);
        }
    }
}
