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
    /// <summary>
    /// 京东会清除js 写入的账号信息 所以目前只做记录使用
    /// </summary>
    public partial class Account : Form
    {
        Form1 form { set; get; }
        public Account(Form1 form)
        {
            InitializeComponent();
            this.form = form; 
            this.textBox1.Text= AccountHelp.GetAccountsStr();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountHelp.SaveAccountsStr(this.textBox1.Text);
            this.form.InitAccount();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccountHelp.GetAccounts();
        }

        private void Account_Load(object sender, EventArgs e)
        {

        }
    }
}
