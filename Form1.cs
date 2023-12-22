using CefSharp;
using CefSharp.DevTools.Network;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            ql = new QLHelp(
                ConfigHelp.GetConfig("QL_URL"),
                ConfigHelp.GetConfig("QL_ClientID") ,
                ConfigHelp.GetConfig("QL_ClientSecret")
            );

            if(!string.IsNullOrEmpty( ql.Url)&&
                !string.IsNullOrEmpty(ql.ClientID)&&
                !string.IsNullOrEmpty(ql.ClientSecret)
                )
            {
                this.button2.Show();
            }
            else
            {
                this.button2.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.chromiumWebBrowser1.Load("https://bean.m.jd.com/bean/signIndex.action");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var cookieManager = this.chromiumWebBrowser1.GetCookieManager();
            var visitor = new CookieVisitor();
            visitor.SendCookie += visitor_SendCookie;
            cookieManager.VisitAllCookies(visitor);
          
        }

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
            if (string.IsNullOrEmpty(pt_pin))
            {
                MessageBox.Show("未获取到cookie 请先登录");
                return;
            }
            Send(pt_pin, this.textBox1.Text);

        }
        private void Send(string pin, string key)
        {
            try
            {
                ql.Login();
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
                MessageBox.Show("发送失败："+e.Message);
            }
          
           
        }
    }
}
