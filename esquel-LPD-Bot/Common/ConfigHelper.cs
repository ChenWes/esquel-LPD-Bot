using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Common
{
    public static class ConfigHelper
    {
        /// <summary>
        /// get config value from key
        /// add by WesChen 2017-02-10
        /// </summary>
        /// <param name="pi_Key">config key</param>
        /// <returns></returns>
        public static string GetConfigValue(string pi_Key)
        {
            string l_getValue = string.Empty;
            try
            {
                l_getValue = ConfigurationManager.AppSettings[pi_Key];
            }
            catch (Exception)
            {

            }

            return l_getValue;
        }
    }
}