namespace DataBaseFront.UI.SelectDB
{
    partial class SelectSqlServer
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
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtUserPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDBConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDBConfig
            // 
            this.pnlDBConfig.Controls.Add(this.txtServerName);
            this.pnlDBConfig.Controls.Add(this.txtUserPass);
            this.pnlDBConfig.Controls.Add(this.label3);
            this.pnlDBConfig.Controls.Add(this.label2);
            this.pnlDBConfig.Controls.Add(this.txtUserID);
            this.pnlDBConfig.Controls.Add(this.label1);
            this.pnlDBConfig.Location = new System.Drawing.Point(0, 0);
            this.pnlDBConfig.Name = "pnlDBConfig";
            this.pnlDBConfig.Size = new System.Drawing.Size(330, 80);
            this.pnlDBConfig.TabIndex = 10;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(109, 3);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(180, 21);
            this.txtServerName.TabIndex = 1;
            // 
            // txtUserPass
            // 
            this.txtUserPass.Location = new System.Drawing.Point(109, 56);
            this.txtUserPass.Name = "txtUserPass";
            this.txtUserPass.Size = new System.Drawing.Size(180, 21);
            this.txtUserPass.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "用户名：";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(109, 29);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(180, 21);
            this.txtUserID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "服务器名称：";
            // 
            // SelectSqlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDBConfig);
            this.Name = "SelectSqlServer";
            this.Size = new System.Drawing.Size(330, 80);
            this.pnlDBConfig.ResumeLayout(false);
            this.pnlDBConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDBConfig;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtUserPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label1;

    }
}
