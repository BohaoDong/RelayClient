using Ulity;

namespace FaithProject.FTU_101.FaithStruct.ControlDomain
{ 
    /// <summary>
    /// 上行控制域
    /// </summary>
    public class UpControlDomain : ControlDomainBase
    {
        /// <summary>
        /// 请求访问位 是否还有数据需请求
        /// </summary>
        public int Acd
        {
            get;
            set;
        }
        /// <summary>
        /// 数据流控制域
        /// </summary>
        public int Dfc
        {
            get;
            set;
        }
        /// <summary>
        /// 下行控制域构造函数
        /// </summary>
        /// <param name="initValue">控制域值</param>
        public UpControlDomain(byte initValue): base(initValue)
        {
            Prm = Helper.GetIntegerBit(ControlDomainCode, 6);
            Acd = Helper.GetIntegerBit(ControlDomainCode, 5);
            Dfc = Helper.GetIntegerBit(ControlDomainCode, 4);
        }
    }
}
