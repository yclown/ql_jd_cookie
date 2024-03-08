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
        public static void SetSetting(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // 如果键已经存在，则更新值
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value;
                }
                // 否则，添加新的键值对
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }

                config.Save(ConfigurationSaveMode.Modified); // 保存更改
                ConfigurationManager.RefreshSection("appSettings"); // 刷新配置，使得更改立即生效
            }
            else
            {
                throw new ArgumentException("Both Key and Value must have a non-null, non-empty value.");
            }
        }

        public static bool IsValidUri(string input)
        {
            try
            {
                Uri uriResult = new Uri(input);
                // URI格式合法，但这并不能保证网址一定能够访问
                return uriResult.Scheme == "http" || uriResult.Scheme == "https" || uriResult.Scheme == "ftp";
            }
            catch (UriFormatException)
            {
                // 无法转换为有效的URI，则认为这不是一个网址
                return false;
            }
        }

    }
}
