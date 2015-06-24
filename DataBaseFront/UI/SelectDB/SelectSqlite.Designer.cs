namespace DataBaseFront.UI.SelectDB
{
    partial class SelectSqlite
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
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDBConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDBConfig
            // 
            this.pnlDBConfig.Controls.Add(this.btnSelectDB);
            this.pnlDBConfig.Controls.Add(this.txtDbPath);
            this.pnlDBConfig.Controls.Add(this.label1);
            this.pnlDBConfig.Location = new System.Drawing.Point(0, 0);
            this.pnlDBConfig.Name = "pnlDBConfig";
            this.pnlDBConfig.Size = new System.Drawing.Size(330, 29);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据库路径：";
            // 
            // SelectSqlite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDBConfig);
            this.Name = "SelectSqlite";
            this.Size = new System.Drawing.Size(330, 33);
            this.pnlDBConfig.ResumeLayout(false);
            this.pnlDBConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDBConfig;
        private System.Windows.Forms.TextBox txtDbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectDB;

    }
}
