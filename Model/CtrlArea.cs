using System;

namespace Model
{
    /// <summary>
    /// 链路控制欲各位的值
    /// </summary>
    public class CtrlArea
    {
        //下行
        public char Prm { get; set; }
        public char Fcb { get; set; }
        public char Fcv { get; set; }
        //上行
        public char Acd { get; set; }
        public char Dfc { get; set; }
        /// <summary>
        /// 十进制的链路控制吗
        /// </summary>
        public int Fcf { get; set; }
        public CtrlArea()
        { 
           Prm=Convert.ToChar(0);
           Acd = Convert.ToChar(1);
           Dfc=Convert.ToChar(0); 
        }
    }
}
