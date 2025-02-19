using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JD_Get
{
    public partial class EditQL : Form
    {
        Form1 form { set; get; }
        public EditQL(Form1 form)
        {
            InitializeComponent();
            this.form=form;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ConfigHelp.IsValidUri(QL_URL.Text)) {
                MessageBox.Show("青龙地址非网址");
                return;
            } 
            ConfigHelp.SetSetting("QL_URL", QL_URL.Text);
            ConfigHelp.SetSetting("QL_ClientID", QL_ClientID.Text);
            ConfigHelp.SetSetting("QL_ClientSecret", QL_ClientSecret.Text);
           
            form.GetQLConfig();
          

            this.Close();
        }
       
         
        private void EditQL_Load(object sender, EventArgs e)
        {
            var ql= new QLHelp();
            this.QL_URL.Text = ql.Url;
            this.QL_ClientID.Text = ql.ClientID;
            this.QL_ClientSecret.Text = ql.ClientSecret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var testQL = new QLHelp()
            {
                ClientID= QL_ClientID.Text,
                ClientSecret= QL_ClientSecret.Text,
                Url = QL_URL.Text,

            };
            try
            {
                string token = testQL.Login();
                MessageBox.Show("测试通过");
            }
            catch(Exception ex)
            {
                MessageBox.Show("参数存在错误"+ex.Message);
            }
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var testQL = new QLHelp()
            {
                ClientID = QL_ClientID.Text,
                ClientSecret = QL_ClientSecret.Text,
                Url = QL_URL.Text,

            };
            try
            {
                testQL.Login();
                testQL.AddEnvs("来自工具的测试", "https://github.com/yclown/", "ToolTest");
                MessageBox.Show("测试通过");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var testQL = new QLHelp()
            {
                ClientID = QL_ClientID.Text,
                ClientSecret = QL_ClientSecret.Text,
                Url = QL_URL.Text,

            };
            try
            {
                testQL.Login();
                string id= testQL.GetEnvs("ToolTest");
                testQL.UpdateEnvs(id, "https://github.com/yclown/", "更新成功");
                MessageBox.Show("测试通过");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
