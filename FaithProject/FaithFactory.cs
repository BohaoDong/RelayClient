using System;
using System.Configuration;
using FaithProject.FTU_101.Balance101;
using FaithProject.FTU_101.NoBalance101;
using Model;

namespace FaithProject
{
    /// <summary>
    /// 简单工厂类，根据设备规约类型，获得其对应处理类F:\水木源华\工作目录\数据中心104\FaithProject\FaithProject\FaithFactory.cs
    /// </summary>
    public class FaithFactory
    {
        public static FaithBaseBll GetFaithBll(string faithType,string devCode)
        {
            FaithBaseBll faithBll = null;
            switch (faithType)
            {
                   
                case "F101"://非平衡101
                    BaseHelper.LinkaddressLength = Convert.ToInt32(ConfigurationManager.AppSettings["FB101LinkaddressLength"]);
                    faithBll = new FaithNoBalance101(1, devCode);
                    faithBll.LinkCount=faithBll.PublicLinkCount = BaseHelper.LinkaddressLength; 
                    break;
                case "F101_Balance"://平衡101
                    BaseHelper.LinkaddressLength = Convert.ToInt32(ConfigurationManager.AppSettings["B101LinkaddressLength"]);
                    faithBll = new FaithBalance101(2, devCode);
                    faithBll.LinkCount = faithBll.PublicLinkCount = BaseHelper.LinkaddressLength;
                    break;
            }
            return faithBll;
        }
    }
}
