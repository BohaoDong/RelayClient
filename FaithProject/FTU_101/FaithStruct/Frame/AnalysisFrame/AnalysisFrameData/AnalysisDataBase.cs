using System;
using System.Collections.Generic;
using System.Globalization;
using Model;
using Ulity;

namespace FaithProject.FTU_101.FaithStruct.Frame.AnalysisFrame.AnalysisFrameData
{
    /// <summary>
    /// 数据帧对象处理分析基类
    /// </summary>
    public class AnalysisDataBase:Base
    {
        public AnalysisDataBase(string devCode)
            : base(devCode)
        { 
        
        }
        /// <summary>
        /// 合成长帧报文
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public virtual byte[] GetLongFrameByteContent(List<byte> body)
        {
            LongMsg = new List<byte> {0x68};
            var lengthNum = (byte)(body.Count+DevLength*2+4);//链路地址长度*2+4+body.Count
            LongMsg.Add(lengthNum);
            LongMsg.Add(lengthNum);
            LongMsg.Add(0x68);//起始字节0X68
            LongMsg.Add((byte)LineCtrl);//控制域

            LongMsg.AddRange(DevCodeByte.ToArray());//链路设备地址  
            LongMsg.AddRange(GetAsdu());
            LongMsg.AddRange(DevCodeByte.ToArray());//链路设备地址   
            LongMsg.AddRange(body.ToArray());//信息
            LongMsg.Add(Helper.MessageCheckCode(LongMsg.ToArray()));//获取校验码 
            LongMsg.Add(0x16);//结束字节0X16
            //ResetFCB(controlCode, Faith101);//判断是否需要翻转并翻转位   
            return LongMsg.ToArray();  
        } 
        /// <summary>
        /// 添加ASDU（类型标识，结构限定词，传送原因）
        /// </summary>
        /// <returns></returns>
        protected virtual byte[] GetAsdu()
        {
            return null;
        }
        /// <summary>
        /// 验证报文校验码
        /// </summary>
        /// <param name="message">校验的报文</param>
        /// <returns>校验码整齐</returns>
        public bool IsValidMessageCheckCode(byte[] message)
        {
            //参数为null或元素为0时抛出一个异常
            if (message == null || message.Length <= 0) throw new Exception();

            byte checkCode = 0;
            //默认为短帧 校验码从第二位开始算起
            int index = 1;
            //长帧，从索引4开始（第五位）
            if (message.Length > 6) index = 4;
            for (int i = index; i < message.Length - 2; i++)
            {
                checkCode += message[i];
            }
            return checkCode == message[message.Length - 2];
        }
         /// <summary>
         /// 合成校验码
         /// </summary>
         /// <param name="message">传过来的无校验码跟结尾的报文</param>
         /// <returns>返回校验码</returns>
        public string MessageCheckCode(byte[] message)
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
            return checkCode.ToString("x2").ToUpper();
        }
        /// <summary>
        /// 判断YX数据是否已经上传
        /// </summary>
        /// <param name="dbTime">实时数据中该设备的最新数据时间</param>
        /// <param name="ip">请求设备的ip+端口</param>
        /// <returns>true没有上传false已经上传</returns>
        public bool YxIsUnloadTime(string dbTime, string ip)
        {
            if (ip != "")//在校验是否有一级数据的时候不要判断是否已经上传过.所以Ip传的空
            {
                try
                {
                    if (DicBiaoShiInfo.dic[ip].YxCreateTime < Convert.ToDateTime(dbTime))
                    {
                        DicBiaoShiInfo.dic[ip].YxCreateTime = Convert.ToDateTime(dbTime);
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断YC数据是否已经上传
        /// </summary>
        /// <param name="dbTime">实时数据中该设备的最新数据时间</param>
        /// <param name="ip">请求设备的ip+端口</param>
        /// <returns>true没有上传false已经上传</returns>
        public bool YcIsUnloadTime(string dbTime, string ip)
        { 
            if (ip != "")//在校验是否有一级数据的时候不要判断是否已经上传过.所以Ip传的空
            {
                try
                {
                    if (DicBiaoShiInfo.dic[ip].YcCreateTime < Convert.ToDateTime(dbTime))
                    {
                        DicBiaoShiInfo.dic[ip].YcCreateTime = Convert.ToDateTime(dbTime);
                        return true;
                    }
                    return false;
                }
                catch {
                    return false;
                }
            }
            return true;
        } 
         /// <summary>
        /// 合成7位时标
         /// </summary>
         /// <param name="time"></param>
         /// <returns></returns>
        public string GetTimeScale(DateTime time)
        {
            string year = time.Year.ToString();
            year = year.Substring(2);
            string month = time.Month.ToString();
            int day =Convert.ToInt32(time.Day.ToString());
            string hour = time.Hour.ToString();
            string minute = time.Minute.ToString();
            string second = time.Second.ToString();
            int millisecond = time.Millisecond; 
            int week = 0;
            switch (time.DayOfWeek.ToString())
            {
                case "Monday":
                    week = 1 << 5;
                    break;
                case "Tuesday":
                    week = 2 << 5;
                    break;
                case "Wednesday":
                    week = 3 << 5;
                    break;
                case "Thursday":
                    week = 4 << 5;
                    break;
                case "Friday":
                    week = 5 << 5;
                    break;
                case "Saturday":
                    week = 6 << 5;
                    break;
                case "Sunday":
                    week = 7 << 5;
                    break;
            }
            string timeScale = Helper.GetHighLowAddressValue(Convert.ToString(Convert.ToInt32(second) * 1000 + millisecond, 16)) +
                SecondHex(minute) +
                SecondHex(hour) + 
                SecondHex((week+day).ToString()) +
                SecondHex(month) +
                SecondHex(year);
            return timeScale;
        } 
         /// <summary>
         /// 合成1位十六进制数
         /// </summary>
         /// <param name="value"></param>
         /// <returns></returns>
        public string SecondHex(string value)
        {
            //value = Convert.ToString(Convert.ToInt32(value), 16);
            //return value.Length < 2 ? "0" + value.ToUpper() : value.ToUpper();
             return Convert.ToInt32(value).ToString("X2");
        }
        /// <summary>
        /// 合成2位十六进制数
        /// </summary>
        /// <param name="value">十六进制数</param>
        /// <returns>2位十六进制数(大写)</returns>
        public string AnalysisSecondHex(int value)
        {
            return value.ToString("X4").ToUpper();
        }
        /// <summary>
        /// 遥测的标度化转换
        /// </summary>
        /// <param name="tagValue">真实值</param>
        /// <param name="modulus">系数</param>
        /// <param name="bianBi">变比</param>
        /// <returns>十六进制遥测值</returns>
        public string GetNegativeNum(string tagValue, string modulus, string bianBi)
        {
            float x = float.Parse(tagValue);
            float y = float.Parse(modulus);
            float z = float.Parse(bianBi);
            float value = x * y / z;//带符号的数 
            if (value < 0)
            {
                Int16 xx = (short)Math.Abs(value);
                Int16 aa = (short)(~xx + 1);//取反加1 
                return Helper.GetHighLowAddressValue(aa.ToString("x4").ToUpper());//GetHighLowValue(AnalysisSecondHex(Convert.ToString((short)aa, 16)));
            }
            return Helper.GetHighLowAddressValue(AnalysisSecondHex((short)value));//Convert.ToString((short)(value), 16).ToUpper()
        }
    }
}
