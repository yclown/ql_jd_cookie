using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace JD_Get
{
    public class AccountHelp
    {
        public class Account {
            public string Login { set; get; }
            public string Password { set; get; }

        }
         
        public static List<Account> GetAccounts() {
            var list = GetAccountsStr().Split(new[] { "\r\n" }, StringSplitOptions.None);
            List<Account> account = new List<Account>() { new Account() { } }; 
            foreach (var item in list)
            {
                if(string.IsNullOrEmpty(item)) continue;    
                var info= item.Split(' ');
                Account a=new Account();
                if (info.Length > 0)
                {
                    a.Login = info[0];
                }
                if(info.Length > 1)
                {
                    a.Password = info[1];
                }
                else
                {
                    a.Password = "";
                }
                account.Add(a);
            } 
            return account;
        }

        public static string GetAccountsStr()
        {
            return ConfigHelp.GetConfig("AccountList");
        }
        public static void SaveAccountsStr(string text)
        {
             ConfigHelp.SetSetting("AccountList", text);
        }
    }
}
