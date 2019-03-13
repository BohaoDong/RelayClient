using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ulity
{
    public class Helper
    {
        /// <summary>
        /// 地址的高低位转换
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetHighLowAddressValue(string address)
        {
            address = AnalysisSecondHex(address);
            address = address.Substring(2) + address.Substring(0, 2);
            return address;
        }

        /// <summary>
        /// 合成2位十六进制数
        /// </summary>
        /// <param name="value">十六进制数</param>
        /// <returns>2位十六进制数(大写)</returns>
        public static string AnalysisSecondHex(string value)
        {
            int vlength = value.Length;
            if (value.Length < 4)
            {
                for (int i = 0; i < 4 - vlength; i++)
                {
                    value = "0" + value;
                }
            }
            return value.ToUpper();
        }

        /// <summary>
        /// 合成校验码
        /// </summary>
        /// <param name="message">传过来的无校验码跟结尾的报文</param>
        /// <returns>返回校验码</returns>
        public static byte MessageCheckCode(byte[] message)
        {
            //参数为null或元素为0时抛出一个异常
            if (message == null || message.Length <= 0) throw new Exception();

            byte checkCode = 0;
            //默认为短帧 校验码从第二位开始算起
            int index = 1;
            //长帧，从索引4开始（第五位）
            if (message.Length > 6) index = 4;
            for (int i = index; i < message.Length; i++)
            {
                checkCode += message[i];
            }
            return checkCode;
        }

        /// <summary>
        /// 十进制数转化成长度为2的十六进制字符串
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string TenToHexStr(int index)
        {
            return index.ToString("x2").ToUpper();
        }

        /// <summary>
        /// 给报文添加空格
        /// </summary>
        /// <param name="message">合成的报文</param>
        /// <returns>加空格的报文</returns>
        public static string AddSpace(string message)
        {
            int len = message.Length;
            string str = "";
            //两个数一组中间加空格
            for (int i = 2; i <= len; i = i + 2)
            {
                str += message.Substring(0, 2) + " ";
                message = message.Substring(2);
            }
            return str;
        }

        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = AnalysisSecondHex(hexString);
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length%2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length/2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i*2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 获取对应数据的datatable
        /// </summary>
        /// <param name="table">包含所有数据的table</param>
        /// <param name="sqlDevColumnNmaes">数据库中设备列名</param>
        /// <param name="valueStrings">要查询table中的devcode的数据</param>
        /// <returns></returns>
        public static DataTable GetTable(DataTable table, string[] sqlDevColumnNmaes, string[] valueStrings)
        {
            try
            {
                var tb = table.Clone(); //克隆的表与原来的表结构相同就是没数据 
                var sbStringBuilder = new StringBuilder();
                for (var i = 0; i < sqlDevColumnNmaes.Length; i++)
                {
                    sbStringBuilder.Append(sqlDevColumnNmaes[i] + "='"+valueStrings[i]+"' and ");
                }
                var sbString = sbStringBuilder.ToString().Substring(0, sbStringBuilder.Length - 5);

                DataRow[] drs = table.Select(sbString);
                foreach (DataRow item in drs)
                {
                    tb.Rows.Add(item.ItemArray);
                }
                return tb;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary> 
        /// 从汉字转换到16进制 
        /// </summary> 
        /// <param name="s"></param>
        /// <returns></returns> 
        public static string ToHex(string s)
        {
            // , string charset, bool fenge
            if ((s.Length % 2) != 0)
            {
                s = s.Replace(" ", "  ");
                //s += " ";//空格 
                //throw new ArgumentException("s is not valid chinese string!"); 
            }
            Encoding chs = Encoding.GetEncoding("utf-8");
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if ((i != bytes.Length - 1))
                {
                    str += string.Format("{0}", " ");
                }
            }
            return str.ToUpper();
        }

        /// <summary> 
        ///  从16进制转换成汉字 
        ///  </summary> 
        ///  <param name="hex"></param>
        /// <returns></returns> 
        public static string UnHexToCh(string hex)//, string charset
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格 
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    //throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            Encoding chs = Encoding.GetEncoding("utf-8");
            return chs.GetString(bytes);
        }
        /// <summary>
        /// 获得低字节在前高字节在后的两字节整形值
        /// </summary>
        /// <param name="low">低位</param>
        /// <param name="high">高位</param>
        /// <returns></returns>
        public static int GetHighLowByteValue(byte low, byte high)
        {
            return (high << 8) + low;
        }
        /// <summary>
        /// 获得字节数组的整型值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetByteListInt(byte[] value)
        {
            int v = (value[1] << 8) + value[0];
            return v;
        }

        public static string GetByteListString(byte[] value)
        {
            return GetByteListInt(value).ToString("X2");
        }


        /// <summary>
        /// 将16进制值的字符串转换为字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetStringByteListValue(string value)
        {
            int tmp = Convert.ToInt32(value, 16);
            return BitConverter.GetBytes(tmp);
        }
        /// <summary>
        /// 取整数的某一位的值
        /// </summary>
        /// <param name="resource">要取某一位的整数</param>
        /// <param name="bitIndex">要取的位置索引，自右至左为0-7</param>
        /// <returns>返回某一位的值（0或者1）</returns>
        public static int GetIntegerBit(int resource, int bitIndex)
        {
            return resource >> bitIndex & 1;
        }

        /// <summary>
        /// 104规约控制域发送和接收报文计算
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static byte[] GetCountNumByteValue(byte value1, byte value2)
        {
            List<byte> bCountNum = new List<byte>();
            value1 = (byte)(value1 + 2);
            bCountNum.Add(value1);
            bCountNum.Add(value2);
            byte[] bArray = bCountNum.ToArray();
            return bArray;
        }

        /// <summary>
        /// 获得低字节在前高字节在后的两字节整形值
        /// </summary>
        /// <param name="low">低位</param>
        /// <param name="mid">中位</param>
        /// <param name="high">高位</param>
        /// <returns></returns>
        public static int GetHighLowByteValue(byte low, byte mid, byte high)
        {
            return (high << 8) + (mid << 8) + low;
        }


        /// <summary>
        /// 将整数的某位置为0或1
        /// </summary>
        ///<param name="a">整数</param>
        /// <param name="bitIndex">整数的某位</param>
        /// <param name="flag">是否置1，TURE表示置1，FALSE表示置0</param>
        /// <returns>返回修改过的值</returns>
        public static int SetIntegerBit(int a, int bitIndex, bool flag)
        {
            if (flag)
            {
                a |= (0x1 << bitIndex);
            }
            else
            {
                a &= ~(0x1 << bitIndex);
            }
            return a;
        }
        /// <summary>
        /// 取整数的某一位到某一位的值
        /// </summary>
        /// <param name="resource">要获取位值的整数</param>
        /// <param name="bitIndex">要取的位置索引，自右至左为0-7</param>
        /// <param name="bitEnd">要区位置的结束点，不包括自身</param>
        /// <returns></returns>
        public static int GetIntegerBitArray(int resource, int bitIndex, int bitEnd)
        {
            BitArray bitArray = new BitArray(new[] { resource });
            int[] infoBit = new int[8];
            int index = 0;
            for (int i = 23; i < 31; i++)
            {
                infoBit[index] = bitArray[i] ? 0 : 1;
                index++;
            }

            return resource >> bitIndex & 1;
        }

        /// <summary>
        /// 将某个整数多位设置为0或1
        /// </summary>
        /// <param name="resource">整数</param>
        /// <param name="setDic">设置0或1的信息字典</param>
        /// <returns></returns>
        public static int SetIngegerArray(int resource, Dictionary<int, bool> setDic)
        {
            foreach (int key in setDic.Keys)
            {
                if (setDic[key])
                {
                    resource |= (0x1 << key);
                }
                else
                {
                    resource &= ~(0x1 << key);
                }
            }
            return resource;
        }
    }
}
