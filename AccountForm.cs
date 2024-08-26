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
    public partial class AccountForm : Form
    {
        Form1 form { set; get; }
        public AccountForm(Form1 form)
        {
            InitializeComponent();
            this.form = form; 
            this.textBox1.Text= AccountHelp.GetAccountsStr();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var account= (BindingList<AccountHelp.Account>)dataGridView1.DataSource;
            AccountHelp.SaveAccountsStr(System.Text.Json.JsonSerializer.Serialize(account));


            this.form.InitAccount();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccountHelp.GetAccounts();
        }

        private void Account_Load(object sender, EventArgs e)
        {

            InitData();

            inport.Visible = AccountHelp.GetAccounts().Count()==0;
        }

        private void InitData()
        {
            var data = AccountHelp.GetAccounts();

            BindingList<AccountHelp.Account> accounts = new BindingList<AccountHelp.Account>();

            data.ForEach(n =>
            {
                accounts.Add(n);
            });

            dataGridView1.DataSource = accounts;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.
                Show("从旧数据导入账号信息会导致现有账号丢失，请确认", "确认导入", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) {
                AccountHelp.FormatFromAccountsStr();
                InitData();
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = (BindingList<AccountHelp.Account>)dataGridView1.DataSource;
            data.Add(new AccountHelp.Account()
            {
                Memo = "1",
                Login = "2",
                Password = "3"
            });
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 检查是否有选中的行  
            if (dataGridView1.SelectedRows.Count > 0)
            { 
                DialogResult result = MessageBox.Show("您确定要删除选中的行吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 
                if (result == DialogResult.Yes)
                { 
                    for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                        }
                        catch
                        {

                        }
                       
                    }
                     
                }
            }
            else
            { 
                MessageBox.Show("没有选中任何行以供删除。", "无行选中", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
