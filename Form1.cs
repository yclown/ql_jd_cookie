﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace JD_Get
{
    public partial class Form1 : Form
    {
        

        public QLHelp ql { set; get; }
        public List<string> needCookieName { set; get; }
        //https://bean.m.jd.com/bean/signIndex.action
        public string LoginUrl = "https://home.m.jd.com/myJd/home.action";

        public bool Auto = false;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            GetQLConfig(); 

        }


        public void GetQLConfig()
        {
            ql = new QLHelp( );
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string auto = ConfigHelp.GetConfig("Auto");
            if (!string.IsNullOrEmpty(auto))
            {
                Auto = Convert.ToBoolean(auto);

            }
            this.checkBox1.Checked = Auto;
            webView21.Source = new Uri(LoginUrl);
            LoginInitAsync();
            InitAccount();
         
            this.Location = Properties.Settings.Default.FormLocation;
            this.Size = Properties.Settings.Default.FormSize;
            this.Text = this.Text + "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            #if DEBUG
            this.label1.Text = "123";
            #endif
            
            webView21.NavigationCompleted += NavigationCompleted;
            webView21.SourceChanged += SourceChanged;
        }

        private void SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            var web = (Microsoft.Web.WebView2.WinForms.WebView2)sender;

        }

        private void NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            var web= (Microsoft.Web.WebView2.WinForms.WebView2)sender;

            if(web.Source.AbsoluteUri.Contains("nopasswordcu")){
                if (Auto)
                {
                    string script = "";
                    script += $@"setTimeout(function() {{ 
                                {GetLoginScript()} 
                        }},2000)";
                    web.ExecuteScriptAsync(script);
                }
            }
            else if(web.Source.AbsoluteUri.Contains("myJd/home.action"))
            {
                GetCookiesAsync();
            }
           
        }



        private void LoginInitAsync()
        {
            //webView21.Source = new Uri(LoginUrl);

            //webView21.ExecuteScriptAsync("document.cookie='';");
            webView21.ExecuteScriptAsync("window.location.reload();");
            webView21.ExecuteScriptAsync($"window.location.href='{LoginUrl}'");


        }
       
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            GetCookiesAsync();
            
        }

        /// <summary>
        /// cookie 格式化 
        /// 因为是异步获取所以采用追加到文本框的方式
        /// </summary>
        /// <param name="cookie"></param>
        

      
       
        private void button2_Click(object sender, EventArgs e)
        {
            string pt_pin = this.label1.Text;
            if (string.IsNullOrEmpty(ql.ClientSecret) || string.IsNullOrEmpty(ql.ClientID) || string.IsNullOrEmpty(ql.Url))
            {
                MessageBox.Show("青龙面板配置不完整，点击青龙配置按钮，输入完成参数");
                return;
            }
            
            if (string.IsNullOrEmpty(pt_pin))
            {
                MessageBox.Show("未获取到cookie 请先登录后点击获取Cookies按钮");
                return;
            }
            string value = this.textBox1.Text;
            if (comboBox1.SelectedItem != null)
            {
                var selected = (AccountHelp.Account)comboBox1.SelectedItem;
                if (selected.Login != null)
                {
                    value += $";from_jdget={selected.Login};";
                }
            }

            var res= Send(pt_pin, value); 
            this.textBox2.Text += ( res+ "\r\n" );
        }
        /// <summary>
        /// 发送cookies到青龙
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="key"></param>
        private string Send(string pin, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(ql.Token))
                {
                    ql.Login();
                }
               
                string id = ql.GetEnvs("pt_pin="+pin);
                if (string.IsNullOrEmpty(id))
                {
                    ql.AddEnvs(pin,key);
                }
                else
                {
                    ql.UpdateEnvs(id, key);
                    ql.EnableEnvs(new List<string>() { id });
                }
                MessageBox.Show("发送成功");
                return $"[{DateTime.Now.ToString("HH:mm:ss")}] pt_pin为{ pin }发送成功";
            }
            catch(Exception e)
            {
                ql.Token = "";
                LogHelper.Error(e,"发送日志：");
                MessageBox.Show("发送失败："+e.Message);
                //return "pin为" + pin + "发送失败 失败原因"+ e.Message;
                return $"[{DateTime.Now.ToString("HH:mm:ss")}] pt_pin为{ pin }发送失败 请查看日志";
            }
          
           
        }

        private void ClearCookie()
        {
             
            webView21.CoreWebView2.Profile.ClearBrowsingDataAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var ShowReLoginSure = ConfigHelp.GetConfig("ShowReLoginSure");
                if (string.IsNullOrEmpty(ShowReLoginSure))
                {
                    DialogResult AF = MessageBox.Show("您确定重新登录？", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (AF == DialogResult.OK)
                    {
                        ClearCookie();
                        LoginInitAsync();
                        this.textBox1.Text = "";
                        this.label1.Text = "";
                    }
                }
                else
                {
                    ClearCookie();
                    LoginInitAsync();
                    this.textBox1.Text = "";
                    this.label1.Text = "";
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditQL popup = new EditQL(this); 
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AccountForm popup = new AccountForm(this);
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog(this);
        }
        List<AccountHelp.Account> accounts = new List<AccountHelp.Account>();
        public void InitAccount()
        { 
            accounts= AccountHelp.GetAccounts();
            //comboBox1.DisplayMember = "Login";
            //comboBox1.ValueMember = "Login"; 
            // 将整个列表绑定到ComboBox的DataSource
             
            comboBox1.DataSource = accounts;
            //comboBox1.SelectedIndex = -1;

        }
        /// <summary>
        /// 直接输入账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CompleteAP();
            var select = (System.Windows.Forms.ComboBox)sender;


            if (this.checkBox1.Checked)
            {
                if (select.SelectedIndex > 0)
                {
                    ClearCookie();
                    LoginInitAsync();
                    this.textBox1.Text = "";
                    this.label1.Text = "";
                }
            }


        }

      
        public void CompleteAP() {
            var LoginScript = GetLoginScript();
            if (!string.IsNullOrEmpty(LoginScript))
            { 
                try
                {
                    this.webView21.ExecuteScriptAsync(LoginScript);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex, "自动输入账号");
                }
            } 
        }

        public string GetLoginScript()
        {
            var account = (AccountHelp.Account)this.comboBox1.SelectedItem;
            if (account != null)
            {
                String execJs = "(function() {";
                execJs += "if(!document.getElementsByClassName('policy_tip-checkbox')[0].checked) { document.getElementsByClassName('policy_tip-checkbox')[0].click(); }";
                execJs += "if(document.getElementById('username').closest('div').style.display=='none'){ document.getElementsByClassName('planBLogin')[0].click(); }";
                
                execJs += "var account='" + account.Login + "';";
                execJs += "var password='" + account.Password + "';";
                execJs += "var evt=new InputEvent('input',{inputType:'insertText',data:account,dataTransfer:null,isComposing:false});";
                execJs += "document.getElementById('username').value=account;";
                execJs += "document.getElementById('username').dispatchEvent(evt);";
                execJs += @"
                    var evt=new InputEvent('input',{inputType:'insertText',data:password,dataTransfer:null,isComposing:false});
                    document.getElementById('pwd').value=password;
                    document.getElementById('pwd').dispatchEvent(evt);
                ";
                execJs += "document.querySelector('#app>div>a').click();";
                //execJs += "alert('ok');";
                execJs += "})();";
                return execJs;
            }
            return "";
        }
   


        /// <summary>
        /// 获取cookie
        /// </summary>
        private async Task GetCookiesAsync()
        {
            this.textBox1.Text = "";
            this.label1.Text = "";  

            var cookieManager = webView21.CoreWebView2.CookieManager; 
            var cookiesCollection =await cookieManager.
                GetCookiesAsync(webView21.Source.AbsoluteUri);

            foreach (var cookie in cookiesCollection)
            { 
                if (cookie.Name == "pt_key" || cookie.Name == "pt_pin")
                {
                    this.textBox1.Text += $"{cookie.Name}={cookie.Value};";
                }
                if (cookie.Name == "pt_pin")
                {
                    this.label1.Text = cookie.Value;
                }
            }
        }
        /// <summary>
        /// 保存窗口属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Location.X > 0 && this.Location.Y > 0)
            {
                Properties.Settings.Default.FormLocation = this.Location;
            }
            if (this.Size.Width > 0 && this.Size.Height > 0)
            {
                Properties.Settings.Default.FormSize = this.Size;
            }
          
            Properties.Settings.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Auto = checkBox1.Checked;
            ConfigHelp.SetSetting("Auto", checkBox1.Checked.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CompleteAP();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ql.Token))
            {
                ql.Login();
            }
            var all= ql.GetAllJDCookieEnvs();
            string pattern = @"from_jdget=(.+);";

            List<string> allDisable = new List<string>();
            foreach (var item in all.Where(n => n.status == 1))
            {
                //item.value.re
                Match match = Regex.Match(item.value, pattern);
                if (match.Success)
                {
                    allDisable.Add("账号："+ match.Groups[1]);
                }
                else
                {
                    allDisable.Add("cookie：" + item.value);
                }
            }
            textBox3.Text = string.Join("\r\n\r\n", allDisable);
        }


    }
}
