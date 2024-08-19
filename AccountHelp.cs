using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Text.Json;

namespace JD_Get
{
    public class AccountHelp
    {
        public class Account {
            public string Login { set; get; }
            public string Password { set; get; }
            public string Memo { set; get; }

            public override string ToString()
            {
                return Login+" "+Memo;
            }
        }
         
        public static List<Account> GetAccounts() {
            
            //var list = GetAccountsStr().Split(new[] { "\r\n" }, StringSplitOptions.None);
            try
            {
                var list = JsonSerializer.Deserialize<List<Account>>(GetAccountsStr());

                return list;
            }
            catch
            {
                return new List<Account>();
            }
            
           
        }

        public static string GetAccountsStr()
        {
            return ConfigHelp.GetConfig("newAccountList");
        }
        public static void SaveAccountsStr(string text)
        {
             ConfigHelp.SetSetting("newAccountList", text);
        }

        /// <summary>
        /// 从老数据格式
        /// </summary>
        /// <param name="text"></param>
        public static void FormatFromAccountsStr()
        {
            var list = ConfigHelp.GetConfig("AccountList").Split(new[] { "\r\n" }, StringSplitOptions.None);

            List<Account> account = new List<Account>() { new Account() { } };
            foreach (var item in list) {

                //var list = GetAccountsStr().Split(new[] { "\r\n" }, StringSplitOptions.None);
                if (string.IsNullOrEmpty(item)) continue;
                var info = item.Split(' ');
                Account a = new Account();
                if (info.Length > 0)
                {
                    a.Login = info[0];
                }
                if (info.Length > 1)
                {
                    a.Password = info[1];
                }
                else
                {
                    a.Password = "";
                }
                account.Add(a); 
            }
            SaveAccountsStr( JsonSerializer.Serialize(account));
        }


    }
}
