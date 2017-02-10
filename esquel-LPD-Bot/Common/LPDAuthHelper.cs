using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Common
{
    public class LPDAuthHelper
    {
        public string UserName
        {
            get
            {
                return Common.ConfigHelper.GetConfigValue("LPD-Auth-UserName");
            }
        }

        public string PassWord
        {
            get
            {
                return Common.ConfigHelper.GetConfigValue("LPD-Auth-PassWord");
            }
        }
    }
}