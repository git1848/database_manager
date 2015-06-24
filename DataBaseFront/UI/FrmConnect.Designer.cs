namespace DataBaseFront.UI
{
    partial class FrmConnect
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDbProvider = new System.Windows.Forms.ComboBox();
            this.lbDbProvider = new System.Windows.Forms.Label();
            this.btnTryConnect = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cmbDBName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pnlAliasName = new System.Windows.Forms.Panel();
            this.txtAliasName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlSelectDB = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.pnlLogin.SuspendLayout();
            this.pnlAliasName.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDbProvider
            // 
            this.cmbDbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbProvider.FormattingEnabled = true;
            this.cmbDbProvider.Location = new System.Drawing.Point(109, 4);
            this.cmbDbProvider.Name = "cmbDbProvider";
            this.cmbDbProvider.Size = new System.Drawing.Size(180, 20);
            this.cmbDbProvider.TabIndex = 0;
            this.cmbDbProvider.TabStop = false;
            this.cmbDbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbDbProvider_SelectedIndexChanged);
            // 
            // lbDbProvider
            // 
            this.lbDbProvider.AutoSize = true;
            this.lbDbProvider.Location = new System.Drawing.Point(26, 7);
            this.lbDbProvider.Name = "lbDbProvider";
            this.lbDbProvider.Size = new System.Drawing.Size(77, 12);
            this.lbDbProvider.TabIndex = 1;
            this.lbDbProvider.Text = "数据库类型：";
            // 
            // btnTryConnect
            // 
            this.btnTryConnect.Location = new System.Drawing.Point(6, 219);
            this.btnTryConnect.Name = "btnTryConnect";
            this.btnTryConnect.Size = new System.Drawing.Size(100, 23);
            this.btnTryConnect.TabIndex = 0;
            this.btnTryConnect.Text = "连接/测试";
            this.btnTryConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTryConnect.UseVisualStyleBackColor = true;
            this.btnTryConnect.Click += new System.EventHandler(this.btnTryConnect_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Enabled = false;
            this.btnLogin.Location = new System.Drawing.Point(263, 219);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "确定";
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cmbDBName
            // 
            this.cmbDBName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDBName.FormattingEnabled = true;
            this.cmbDBName.Location = new System.Drawing.Point(109, 3);
            this.cmbDBName.Name = "cmbDBName";
            this.cmbDBName.Size = new System.Drawing.Size(180, 20);
            this.cmbDBName.TabIndex = 4;
            this.cmbDBName.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "数据库名称：";
            // 
            // pnlLogin
            // 
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Controls.Add(this.pnlAliasName);
            this.pnlLogin.Controls.Add(this.btnTryConnect);
            this.pnlLogin.Controls.Add(this.pnlBottom);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.pnlSelectDB);
            this.pnlLogin.Controls.Add(this.pnlTop);
            this.pnlLogin.Location = new System.Drawing.Point(35, 30);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(347, 250);
            this.pnlLogin.TabIndex = 13;
            // 
            // pnlAliasName
            // 
            this.pnlAliasName.Controls.Add(this.txtAliasName);
            this.pnlAliasName.Controls.Add(this.label1);
            this.pnlAliasName.Location = new System.Drawing.Point(3, 3);
            this.pnlAliasName.Name = "pnlAliasName";
            this.pnlAliasName.Size = new System.Drawing.Size(338, 30);
            this.pnlAliasName.TabIndex = 0;
            // 
            // txtAliasName
            // 
            this.txtAliasName.Location = new System.Drawing.Point(110, 7);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new System.Drawing.Size(179, 21);
            this.txtAliasName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "连接名：";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.cmbDBName);
            this.pnlBottom.Controls.Add(this.label4);
            this.pnlBottom.Location = new System.Drawing.Point(3, 170);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(338, 30);
            this.pnlBottom.TabIndex = 3;
            this.pnlBottom.Visible = false;
            // 
            // pnlSelectDB
            // 
            this.pnlSelectDB.Location = new System.Drawing.Point(3, 67);
            this.pnlSelectDB.Name = "pnlSelectDB";
            this.pnlSelectDB.Size = new System.Drawing.Size(338, 100);
            this.pnlSelectDB.TabIndex = 2;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmbDbProvider);
            this.pnlTop.Controls.Add(this.lbDbProvider);
            this.pnlTop.Location = new System.Drawing.Point(3, 35);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(338, 30);
            this.pnlTop.TabIndex = 1;
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.pnlLogin);
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(427, 307);
            this.gbMain.TabIndex = 14;
            this.gbMain.TabStop = false;
            // 
            // FrmConnect
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 307);
            this.Controls.Add(this.gbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接到数据库";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogin_FormClosing);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlAliasName.ResumeLayout(false);
            this.pnlAliasName.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.gbMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbDbProvider;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnTryConnect;
        private System.Windows.Forms.ComboBox cmbDbProvider;
        private System.Windows.Forms.ComboBox cmbDBName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlSelectDB;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlAliasName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAliasName;
        private System.Windows.Forms.GroupBox gbMain;
    }
}

