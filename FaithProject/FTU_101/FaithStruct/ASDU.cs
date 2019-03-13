using System.Collections.Generic;

namespace FaithProject.FTU_101.FaithStruct
{
    /// <summary>
    /// 应用服务数据单元类
    /// </summary>
    public class Asdu
    {
        /// <summary>
        /// 应用报文类型标示
        /// </summary>
        public byte Ti { get; set; }

        /// <summary>
        /// 可变结构限定词
        /// </summary>
        public StructLimitCode LimitCode { get; set; }

        /// <summary>
        /// 传送原因
        /// </summary>
        public CotCode Cot { get; set; }
        private byte[] _publicAddress;
        /// <summary>
        /// 应用服务数据单元公共地址
        /// </summary>
        public byte[] PublicAddress
        {
            get
            {
                if (_publicAddress == null)
                {
                    _publicAddress = new byte[2];
                }
                return _publicAddress;
            }
            set
            {
                _publicAddress = value;
            }
        }

        /// <summary>
        /// 信息体
        /// </summary>
        public List<byte> InfoBody { get; set; }
         
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">应用报文类型</param>
        /// <param name="limitCode">可变结构限定词</param>
        /// <param name="cot">传送原因</param>
        /// <param name="publicAddress">公共地址</param>
        /// <param name="infoBody">信息体</param>
        public Asdu(byte type, byte limitCode, byte[] cot, byte[] publicAddress, List<byte> infoBody)
        {
            Ti = type;
            LimitCode = new StructLimitCode(limitCode);
            Cot = new CotCode(cot);
            PublicAddress = publicAddress;
            InfoBody = infoBody;
        }

        /// <summary>
        /// 通过完整的应用数据单元字节数组，获得应用服务数据单元对象 通常解析硬件上行数据帧时使用
        /// </summary>
        /// <param name="asduBytes">ASDU数据</param>
        /// <param name="i">公共地址长度</param>
        /// <param name="j">传送原因长度</param>
        public Asdu(List<byte> asduBytes, int i, int j)
        {
            byte[] bcot;
            Ti = asduBytes[0];
            LimitCode = new StructLimitCode(asduBytes[1]);
            if (j == 1)
            {
                bcot = new[] { asduBytes[2] };
                Cot = new CotCode(bcot);
            }
            else
            {
                bcot = new[] { asduBytes[2], asduBytes[3] };
                Cot = new CotCode(bcot);
            }
            if (i == 1)
            { 
                PublicAddress[0] = asduBytes[2 + bcot.Length];
                asduBytes.RemoveRange(0, 3 + bcot.Length);

            }
            else 
            {
                PublicAddress[0] = asduBytes[2 + bcot.Length];
                PublicAddress[1] = asduBytes[3 + bcot.Length];
                asduBytes.RemoveRange(0, 4 + bcot.Length); //移除非信息体部分 
            }
            InfoBody = asduBytes;
        }

        /// <summary>
        /// 将当前应用数据单元对象 转化为字节数组  通常合成下行报文时使用
        /// </summary>
        /// <param name="i">1为非平衡 2为平衡</param>
        /// <param name="faith"></param>
        /// <returns></returns>
        public byte[] GetAsdu(int i, FaithBaseBll faith)
        {
            List<byte> asduBytes = new List<byte>();
            asduBytes.Add(Ti);
            asduBytes.Add(LimitCode.GetLimitCode);
            if (faith.CotCode == 1)
            {
                asduBytes.Add(Cot.GetCotCode);
            }
            else
            {
                asduBytes.Add(Cot.GetCotCode);
                asduBytes.Add(0x00);
            }
            if (i == 2)//平衡时的公共地址
            {
                asduBytes.Add(PublicAddress[0]);
                asduBytes.Add(PublicAddress[1]);
            }
            else
            {
                asduBytes.Add(PublicAddress[0]);
            }
            asduBytes.AddRange(InfoBody);
            return asduBytes.ToArray();
        }
        /// <summary>
        /// 资源清理
        /// </summary>
        public void MyDispose()
        {
            LimitCode = null;
            Cot = null;
            PublicAddress = null;
            InfoBody.Clear();
            InfoBody = null;
        }
    }
}
