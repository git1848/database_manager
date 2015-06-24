namespace DataBaseFront.UI.SelectDB
{
    partial class SelectAccess
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlDBConfig = new System.Windows.Forms.Panel();
            this.btnSelectDB = new System.Windows.Forms.Button();
            this.txtDbPath = new System.Windows.Forms.TextBox();
            this.lblForDbPath = new System.Windows.Forms.Label();
            this.lblForPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkHasPassword = new System.Windows.Forms.CheckBox();
            this.pnlDBConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDBConfig
            // 
            this.pnlDBConfig.Controls.Add(this.chkHasPassword);
            this.pnlDBConfig.Controls.Add(this.txtPassword);
            this.pnlDBConfig.Controls.Add(this.btnSelectDB);
            this.pnlDBConfig.Controls.Add(this.txtDbPath);
            this.pnlDBConfig.Controls.Add(this.lblForPassword);
            this.pnlDBConfig.Controls.Add(this.lblForDbPath);
            this.pnlDBConfig.Location = new System.Drawing.Point(0, 0);
            this.pnlDBConfig.Name = "pnlDBConfig";
            this.pnlDBConfig.Size = new System.Drawing.Size(330, 87);
            this.pnlDBConfig.TabIndex = 10;
            // 
            // btnSelectDB
            // 
            this.btnSelectDB.Location = new System.Drawing.Point(293, 2);
            this.btnSelectDB.Name = "btnSelectDB";
            this.btnSelectDB.Size = new System.Drawing.Size(31, 23);
            this.btnSelectDB.TabIndex = 1;
            this.btnSelectDB.Text = "...";
            this.btnSelectDB.UseVisualStyleBackColor = true;
            this.btnSelectDB.Click += new System.EventHandler(this.btnSelectDB_Click);
            // 
            // txtDbPath
            // 
            this.txtDbPath.Location = new System.Drawing.Point(109, 3);
            this.txtDbPath.Name = "txtDbPath";
            this.txtDbPath.Size = new System.Drawing.Size(180, 21);
            this.txtDbPath.TabIndex = 0;
            // 
            // lblForDbPath
            // 
            this.lblForDbPath.AutoSize = true;
            this.lblForDbPath.Location = new System.Drawing.Point(27, 7);
            this.lblForDbPath.Name = "lblForDbPath";
            this.lblForDbPath.Size = new System.Drawing.Size(77, 12);
            this.lblForDbPath.TabIndex = 6;
            this.lblForDbPath.Text = "数据库路径：";
            // 
            // lblForPassword
            // 
            this.lblForPassword.AutoSize = true;
            this.lblForPassword.Location = new System.Drawing.Point(27, 63);
            this.lblForPassword.Name = "lblForPassword";
            this.lblForPassword.Size = new System.Drawing.Size(77, 12);
            this.lblForPassword.TabIndex = 6;
            this.lblForPassword.Text = "数据库路径：";
            this.lblForPassword.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(109, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(180, 21);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Visible = false;
            // 
            // chkHasPassword
            // 
            this.chkHasPassword.AutoSize = true;
            this.chkHasPassword.Location = new System.Drawing.Point(109, 37);
            this.chkHasPassword.Name = "chkHasPassword";
            this.chkHasPassword.Size = new System.Drawing.Size(72, 16);
            this.chkHasPassword.TabIndex = 8;
            this.chkHasPassword.Text = "输入密码";
            this.chkHasPassword.UseVisualStyleBackColor = true;
            this.chkHasPassword.CheckedChanged += new System.EventHandler(this.chkHasPassword_CheckedChanged);
            // 
            // SelectAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDBConfig);
            this.Name = "SelectAccess";
            this.Size = new System.Drawing.Size(330, 90);
            this.pnlDBConfig.ResumeLayout(false);
            this.pnlDBConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDBConfig;
        private System.Windows.Forms.TextBox txtDbPath;
        private System.Windows.Forms.Label lblForDbPath;
        private System.Windows.Forms.Button btnSelectDB;
        private System.Windows.Forms.Label lblForPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkHasPassword;

    }
}
