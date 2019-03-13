using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class GetDevBll
    { 
        public static List<string> GetAllDevList(int doorId)
        {
            return GetDevDal.GetAllDevData(doorId);
        }
    }
}
