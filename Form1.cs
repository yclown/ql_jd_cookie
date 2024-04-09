using CefSharp;
using CefSharp.DevTools.Network;
using CefSharp.WinForms;
using Newtonsoft.Json.Linq;
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
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace JD_Get
{
    public partial class Form1 : Form
    {
        

        public QLHelp ql { set; get; }
        public List<string> needCookieName { set; get; }
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            GetQLConfig();
            
        }
        public void GetQLConfig()
        {
            ql = new QLHelp( );
           
            //ql.Login();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.chromiumWebBrowser1.Load("https://bean.m.jd.com/bean/signIndex.action");
            LoginInitAsync();
            InitAccount();
        }

        private void LoginInitAsync()
        { 
           var res= this.chromiumWebBrowser1.LoadUrlAsync("https://bean.m.jd.com/bean/signIndex.action").Result; 
           this.chromiumWebBrowser1.ExecuteScriptAsync("document.querySelector(\"#app > div > p.policy_tip > input\").click();");
          

        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            var cookieManager = this.chromiumWebBrowser1.GetCookieManager();
            var visitor = new CookieVisitor();
            visitor.SendCookie += visitor_SendCookie;
            cookieManager.VisitAllCookies(visitor);
          
        }

        /// <summary>
        /// cookie 格式化 
        /// 因为是异步获取所以采用追加到文本框的方式
        /// </summary>
        /// <param name="cookie"></param>
        private void visitor_SendCookie(CefSharp.Cookie cookie)
        {
            if (cookie.Name == "pt_key" || cookie.Name == "pt_pin") {
                this.textBox1.Text += $"{cookie.Name}={cookie.Value};";
            } 
            if(cookie.Name == "pt_pin")
            {
                this.label1.Text = cookie.Value;
            } 
        }

        private class CookieVisitor : ICookieVisitor
        {
            //public List<CefSharp.Cookie> AllCookies { get; } = new List<CefSharp.Cookie>();
            public event Action<CefSharp.Cookie> SendCookie;
            public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
            {
                deleteCookie = false;
                if (SendCookie != null)
                {
                    SendCookie(cookie);
                }
                //AllCookies.Add(cookie);
                return true;
            }

            public void Dispose()
            {
                // Dispose相关资源
            }

           
        }



     

        private void label1_Click(object sender, EventArgs e)
        {

        }

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
            Send(pt_pin, this.textBox1.Text);

        }
        /// <summary>
        /// 发送cookies到青龙
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="key"></param>
        private void Send(string pin, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(ql.Token))
                {
                    ql.Login();
                }
               
                string id = ql.GetEnvs(pin);
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
            }
            catch(Exception e)
            {
                ql.Token = "";
                LogHelper.Error(e,"发送日志：");
                MessageBox.Show("发送失败："+e.Message);
            }
          
           
        }

        private void ClearCookie()
        {

            // 清除所有cookies
            Cef.GetGlobalCookieManager().DeleteCookies("", "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
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
            Account popup = new Account(this);
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog(this);
        }

        public void InitAccount()
        { 
            var accounts= AccountHelp.GetAccounts();
            comboBox1.DisplayMember = "Login";
            comboBox1.ValueMember = "Login"; 
            // 将整个列表绑定到ComboBox的DataSource
            comboBox1.DataSource = accounts;


        }
        /// <summary>
        /// 直接输入账号会清空 暂时不处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //var account= (AccountHelp.Account)this.comboBox1.SelectedItem;
            //if (account != null)
            //{
            //    String execJs = "var account='" + account.Login + "';"; 
            //    execJs += "var evt=new InputEvent('input',{inputType:'insertText',data:account,dataTransfer:null,isComposing:false});";
            //    execJs += "document.getElementById('username').value=account;";
            //    execJs += "document.getElementById('username').dispatchEvent(evt);"; 
            //    try
            //    {
            //        this.chromiumWebBrowser1.ExecuteScriptAsync(execJs);
            //    }
            //    catch (Exception ex)
            //    {
            //        LogHelper.Error(ex, "自动输入账号");
            //    }
            //}
           
        }
    }
}
