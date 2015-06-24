namespace DataBaseFront.UI
{
    partial class FrmBuildDoc
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTemplates = new System.Windows.Forms.ComboBox();
            this.rbBuildFormatOfPDF = new System.Windows.Forms.RadioButton();
            this.rbBuildFormatOfWeb = new System.Windows.Forms.RadioButton();
            this.rbBuildFormatOfWord = new System.Windows.Forms.RadioButton();
            this.btnBuildDoc = new System.Windows.Forms.Button();
            this.pbBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.ucSelectTables1 = new DataBaseFront.UI.UIControls.UCSelectTables();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbTemplates);
            this.groupBox2.Controls.Add(this.rbBuildFormatOfPDF);
            this.groupBox2.Controls.Add(this.rbBuildFormatOfWeb);
            this.groupBox2.Controls.Add(this.rbBuildFormatOfWord);
            this.groupBox2.Location = new System.Drawing.Point(560, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 367);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生成格式";
            // 
            // cmbTemplates
            // 
            this.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplates.Enabled = false;
            this.cmbTemplates.FormattingEnabled = true;
            this.cmbTemplates.Location = new System.Drawing.Point(32, 84);
            this.cmbTemplates.Name = "cmbTemplates";
            this.cmbTemplates.Size = new System.Drawing.Size(121, 20);
            this.cmbTemplates.TabIndex = 2;
            // 
            // rbBuildFormatOfPDF
            // 
            this.rbBuildFormatOfPDF.AutoSize = true;
            this.rbBuildFormatOfPDF.Location = new System.Drawing.Point(16, 110);
            this.rbBuildFormatOfPDF.Name = "rbBuildFormatOfPDF";
            this.rbBuildFormatOfPDF.Size = new System.Drawing.Size(131, 16);
            this.rbBuildFormatOfPDF.TabIndex = 1;
            this.rbBuildFormatOfPDF.Text = "生成PDF(*.pdf)格式";
            this.rbBuildFormatOfPDF.UseVisualStyleBackColor = true;
            this.rbBuildFormatOfPDF.CheckedChanged += new System.EventHandler(this.rbBuildFormat_CheckedChanged);
            // 
            // rbBuildFormatOfWeb
            // 
            this.rbBuildFormatOfWeb.AutoSize = true;
            this.rbBuildFormatOfWeb.Location = new System.Drawing.Point(16, 62);
            this.rbBuildFormatOfWeb.Name = "rbBuildFormatOfWeb";
            this.rbBuildFormatOfWeb.Size = new System.Drawing.Size(137, 16);
            this.rbBuildFormatOfWeb.TabIndex = 1;
            this.rbBuildFormatOfWeb.Text = "生成Html(*.htm)格式";
            this.rbBuildFormatOfWeb.UseVisualStyleBackColor = true;
            this.rbBuildFormatOfWeb.CheckedChanged += new System.EventHandler(this.rbBuildFormat_CheckedChanged);
            // 
            // rbBuildFormatOfWord
            // 
            this.rbBuildFormatOfWord.AutoSize = true;
            this.rbBuildFormatOfWord.Checked = true;
            this.rbBuildFormatOfWord.Location = new System.Drawing.Point(16, 40);
            this.rbBuildFormatOfWord.Name = "rbBuildFormatOfWord";
            this.rbBuildFormatOfWord.Size = new System.Drawing.Size(137, 16);
            this.rbBuildFormatOfWord.TabIndex = 0;
            this.rbBuildFormatOfWord.TabStop = true;
            this.rbBuildFormatOfWord.Text = "生成Word(*.doc)格式";
            this.rbBuildFormatOfWord.UseVisualStyleBackColor = true;
            this.rbBuildFormatOfWord.CheckedChanged += new System.EventHandler(this.rbBuildFormat_CheckedChanged);
            // 
            // btnBuildDoc
            // 
            this.btnBuildDoc.Location = new System.Drawing.Point(306, 20);
            this.btnBuildDoc.Name = "btnBuildDoc";
            this.btnBuildDoc.Size = new System.Drawing.Size(133, 47);
            this.btnBuildDoc.TabIndex = 0;
            this.btnBuildDoc.Text = "生成文档";
            this.btnBuildDoc.UseVisualStyleBackColor = true;
            this.btnBuildDoc.Click += new System.EventHandler(this.btnBuildDoc_Click);
            // 
            // pbBar
            // 
            this.pbBar.ForeColor = System.Drawing.Color.Black;
            this.pbBar.Location = new System.Drawing.Point(6, 86);
            this.pbBar.Name = "pbBar";
            this.pbBar.Size = new System.Drawing.Size(762, 14);
            this.pbBar.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblProcess);
            this.groupBox3.Controls.Add(this.pbBar);
            this.groupBox3.Controls.Add(this.btnBuildDoc);
            this.groupBox3.Location = new System.Drawing.Point(12, 385);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(778, 106);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成";
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(6, 71);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(29, 12);
            this.lblProcess.TabIndex = 7;
            this.lblProcess.Text = "进度";
            // 
            // ucSelectTables1
            // 
            this.ucSelectTables1.Location = new System.Drawing.Point(12, 12);
            this.ucSelectTables1.Name = "ucSelectTables1";
            this.ucSelectTables1.Size = new System.Drawing.Size(542, 367);
            this.ucSelectTables1.TabIndex = 8;
            // 
            // FrmBuildDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(797, 502);
            this.Controls.Add(this.ucSelectTables1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmBuildDoc";
            this.Text = "生成数据库文档";
            this.Load += new System.EventHandler(this.FrmBuildDoc_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbBuildFormatOfWeb;
        private System.Windows.Forms.Button btnBuildDoc;
        private System.Windows.Forms.ProgressBar pbBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbBuildFormatOfWord;
        private System.Windows.Forms.RadioButton rbBuildFormatOfPDF;
        private System.Windows.Forms.ComboBox cmbTemplates;
        private UIControls.UCSelectTables ucSelectTables1;
        private System.Windows.Forms.Label lblProcess;


    }
}