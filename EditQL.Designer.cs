namespace JD_Get
{
    partial class EditQL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.QL_URL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.QL_ClientID = new System.Windows.Forms.TextBox();
            this.QL_ClientSecret = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // QL_URL
            // 
            this.QL_URL.Location = new System.Drawing.Point(104, 38);
            this.QL_URL.Name = "QL_URL";
            this.QL_URL.Size = new System.Drawing.Size(211, 21);
            this.QL_URL.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "青龙地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ClientID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "ClientSecret";
            // 
            // QL_ClientID
            // 
            this.QL_ClientID.Location = new System.Drawing.Point(104, 65);
            this.QL_ClientID.Name = "QL_ClientID";
            this.QL_ClientID.Size = new System.Drawing.Size(211, 21);
            this.QL_ClientID.TabIndex = 4;
            // 
            // QL_ClientSecret
            // 
            this.QL_ClientSecret.Location = new System.Drawing.Point(104, 95);
            this.QL_ClientSecret.Name = "QL_ClientSecret";
            this.QL_ClientSecret.Size = new System.Drawing.Size(211, 21);
            this.QL_ClientSecret.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 108);
            this.label4.TabIndex = 6;
            this.label4.Text = "设置说明\r\n\r\n进入青龙面板\r\n\r\n选择系统设置-应用设置-创建应用\r\n\r\n名称随便填写，权限全选\r\n\r\n创建好后填写对应的参数";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 36);
            this.label5.TabIndex = 8;
            this.label5.Text = "填写说明\r\n地址格式为:http://{ip}:{端口}/\r\n例如http://192.168.9.1:5700/";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(21, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "配置后可以一键发送到青龙";
            // 
            // EditQL
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(355, 329);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.QL_ClientSecret);
            this.Controls.Add(this.QL_ClientID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QL_URL);
            this.Name = "EditQL";
            this.Text = "青龙配置";
            this.Load += new System.EventHandler(this.EditQL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox QL_URL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox QL_ClientID;
        private System.Windows.Forms.TextBox QL_ClientSecret;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}