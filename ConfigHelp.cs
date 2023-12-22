using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD_Get
{
    public class ConfigHelp
    {
        public static string GetConfig(string key)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var c = config.AppSettings.Settings[key];
            if (c == null)
            {
                return "";
            }
            else
            {
                return c.Value;
            }


        }
    }
}
