namespace DataBaseFront.UI
{
    partial class FrmDatabases
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatabases));
            this.imgFortvDB = new System.Windows.Forms.ImageList(this.components);
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbConnect = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbConectDB = new System.Windows.Forms.ToolStripButton();
            this.tsbDisConectDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbProperty = new System.Windows.Forms.ToolStripButton();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tvDB = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgFortvDB
            // 
            this.imgFortvDB.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imgFortvDB.ImageSize = new System.Drawing.Size(16, 16);
            this.imgFortvDB.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(192, 548);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnect,
            this.toolStripSeparator1,
            this.tsbConectDB,
            this.tsbDisConectDB,
            this.toolStripSeparator2,
            this.tsbRefresh,
            this.tsbProperty,
            this.tsbRemove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(192, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbConnect
            // 
            this.tsbConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(45, 22);
            this.tsbConnect.Text = "连接";
            this.tsbConnect.ButtonClick += new System.EventHandler(this.tsbConnect_ButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbConectDB
            // 
            this.tsbConectDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbConectDB.Enabled = false;
            this.tsbConectDB.Image = global::DataBaseFront.Properties.Resources.btnConnect;
            this.tsbConectDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConectDB.Name = "tsbConectDB";
            this.tsbConectDB.Size = new System.Drawing.Size(23, 22);
            this.tsbConectDB.Text = "打开连接";
            this.tsbConectDB.Click += new System.EventHandler(this.tsbConectDB_Click);
            // 
            // tsbDisConectDB
            // 
            this.tsbDisConectDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDisConectDB.Enabled = false;
            this.tsbDisConectDB.Image = global::DataBaseFront.Properties.Resources.btnUnConnect;
            this.tsbDisConectDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDisConectDB.Name = "tsbDisConectDB";
            this.tsbDisConectDB.Size = new System.Drawing.Size(23, 22);
            this.tsbDisConectDB.Text = "关闭连接";
            this.tsbDisConectDB.Click += new System.EventHandler(this.tsbDisConectDB_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Enabled = false;
            this.tsbRefresh.Image = global::DataBaseFront.Properties.Resources.refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "刷新";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbProperty
            // 
            this.tsbProperty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProperty.Enabled = false;
            this.tsbProperty.Image = global::DataBaseFront.Properties.Resources.property;
            this.tsbProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProperty.Name = "tsbProperty";
            this.tsbProperty.Size = new System.Drawing.Size(23, 22);
            this.tsbProperty.Text = "属性";
            this.tsbProperty.Click += new System.EventHandler(this.tsbProperty_Click);
            // 
            // tsbRemove
            // 
            this.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemove.Enabled = false;
            this.tsbRemove.Image = global::DataBaseFront.Properties.Resources.remove;
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(23, 22);
            this.tsbRemove.Text = "删除连接";
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(192, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            // 
            // tsslLabel
            // 
            this.tsslLabel.Name = "tsslLabel";
            this.tsslLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // tvDB
            // 
            this.tvDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDB.ImageIndex = 0;
            this.tvDB.ImageList = this.imgFortvDB;
            this.tvDB.Location = new System.Drawing.Point(0, 25);
            this.tvDB.Name = "tvDB";
            this.tvDB.SelectedImageIndex = 0;
            this.tvDB.ShowNodeToolTips = true;
            this.tvDB.Size = new System.Drawing.Size(192, 526);
            this.tvDB.TabIndex = 4;
            this.tvDB.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDB_BeforeExpand);
            this.tvDB.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDB_BeforeSelect);
            this.tvDB.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDB_AfterSelect);
            this.tvDB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvDB_MouseClick);
            // 
            // FrmDatabases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 573);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tvDB);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.HideOnClose = true;
            this.Name = "FrmDatabases";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.Text = "连接管理";
            this.Load += new System.EventHandler(this.FrmDatabases_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgFortvDB;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbConectDB;
        private System.Windows.Forms.ToolStripButton tsbDisConectDB;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton tsbConnect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView tvDB;
        private System.Windows.Forms.ToolStripStatusLabel tsslLabel;
        private System.Windows.Forms.ToolStripButton tsbProperty;
        private System.Windows.Forms.ToolStripButton tsbRemove;
    }
}