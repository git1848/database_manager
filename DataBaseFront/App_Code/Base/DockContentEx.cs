using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace WeifenLuo.WinFormsUI.Docking
{
    public class DockContentEx : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ContextMenuStrip contextMenuStripMain;
        private ToolStripMenuItem tsmiClose;
        private ToolStripMenuItem tsmiCloseAll;
        private ToolStripMenuItem tsmiCloseAllButThis;
        private IContainer components;

        public DockContentEx()
        {
            InitializeComponent();

            this.TabPageContextMenuStrip = contextMenuStripMain;
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClose,
            this.tsmiCloseAllButThis,
            this.tsmiCloseAll});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.ShowImageMargin = false;
            this.contextMenuStripMain.Size = new System.Drawing.Size(142, 92);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(141, 22);
            this.tsmiClose.Text = "关闭";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // 除此之外全部关闭ToolStripMenuItem
            // 
            this.tsmiCloseAllButThis.Name = "tsmiCloseAllButThis";
            this.tsmiCloseAllButThis.Size = new System.Drawing.Size(141, 22);
            this.tsmiCloseAllButThis.Text = "除此之外全部关闭";
            this.tsmiCloseAllButThis.Click += new System.EventHandler(this.tsmiCloseAllButThis_Click);
            // 
            // 全部关闭ToolStripMenuItem
            // 
            this.tsmiCloseAll.Name = "tsmiCloseAll";
            this.tsmiCloseAll.Size = new System.Drawing.Size(141, 22);
            this.tsmiCloseAll.Text = "全部关闭";
            this.tsmiCloseAll.Click += new System.EventHandler(this.tsmiCloseAll_Click);
            // 
            // DockContentEx
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DockContentEx";
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiCloseAllButThis_Click(object sender, EventArgs e)
        {
            IDockContent[] documents = DockPanel.DocumentsToArray();

            foreach (IDockContent content in documents)
            {
                if (!content.Equals(this))
                {
                    content.DockHandler.Close();
                }
            }
        }

        private void tsmiCloseAll_Click(object sender, EventArgs e)
        {
            IDockContent[] documents = DockPanel.DocumentsToArray();

            foreach (IDockContent content in documents)
            {
                content.DockHandler.Close();
            }
        }
    }
}