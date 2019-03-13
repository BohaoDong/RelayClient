using Model;

namespace FaithProject.FTU_101.FaithStruct.ControlDomain
{
    /// <summary>
    /// 控制域类
    /// </summary>
    public class ControlDomainBase
    {
        /// <summary>
        /// 给对应的遥信遥测设置控制域
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="doorId"></param>
        public void SetLineCtrl(string ip, int doorId)
        {
            switch (doorId)
            {
                case 1: //非平衡101
                    DicBiaoShiInfo.dic[ip].LineCtrl = BaseHelper.LineCtrl28;
                    break;
                case 2://平衡101
                    if (DicBiaoShiInfo.dic[ip].LineCtrl == BaseHelper.LineCtrl73)
                    {
                        DicBiaoShiInfo.dic[ip].LineCtrl = BaseHelper.LineCtrl53;
                    }
                    else
                    {
                        DicBiaoShiInfo.dic[ip].LineCtrl = BaseHelper.LineCtrl73;
                    }
                    break;
            }

        }

        public ControlDomainBase(byte initValue)
        {
            ControlDomainCode = initValue;
            Fc = (ControlDomainCode & 0x0F);//取后四位获得链路功能码 控制域 0x0F 00001111按位与运算
        }
        /// <summary>
        /// 保留位(bit 8)
        /// </summary>
        public int Res
        {
            get { return 0; }
        }
        /// <summary>
        /// 启动方向位,等于0，表示由设备发出 1为主站发出
        /// </summary>
        public int Prm
        {
            get;
            set;
        }
        /// <summary>
        /// 链路功能码
        /// </summary>
        public int Fc
        {
            get;
            set;
        }
        /// <summary>
        /// 合成的控制域
        /// </summary>
        /// <returns></returns>
        public byte ControlDomainCode
        {
            get;
            set;
        }
    }
}