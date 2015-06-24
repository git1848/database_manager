using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using DataBaseFront.Entity;

namespace DataBaseFront.UI
{
    public partial class FrmMain : BaseForm
    {
        public static FrmMain MAIN = null;

        private static FrmDatabases frmDatabases = new FrmDatabases();

        public FrmMain()
        {
            InitializeComponent();

            //保存窗体
            MAIN = this;

            //初始化程序配置信息
            this.InitAppStart();
        }

        /// <summary>
        /// 显示提示文字
        /// </summary>
        public string ShowTip
        {
            set
            {
                this.tsslTip.Text = value;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            frmDatabases.Show(this.dockPanel1, DockState.DockLeft);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            if (!this.TopMost)
            {
                this.btnPin.BackgroundImage = DataBaseFront.Properties.Resources.unpin;
                this.TopMost = true;
            }
            else
            {
                this.btnPin.BackgroundImage = DataBaseFront.Properties.Resources.pin;
                this.TopMost = false;
            }
        }

        private void tsbOpenDocument_Click(object sender, EventArgs e)
        {

        }

        private void tsbSaveDocument_Click(object sender, EventArgs e)
        {
            BaseForm baseForm = Extensions_BaseForm.GetActivatedForm(this.dockPanel1);
            if (baseForm == null || !baseForm.GetType().Equals(typeof(FrmExec)))
                return;

            FrmExec form = baseForm as FrmExec;
            FileUtil.Save(form.Sql);
        }

        private void tsbNewQuery_Click(object sender, EventArgs e)
        {

        }

        private void tsbDoExec_Click(object sender, EventArgs e)
        {
            this.DoExecSQL();
        }

        public void ShowBuild(Link link, string tableName)
        {
            string title = string.Format("数据库\"{0}\"模板代码生成", link.DbParam.DbName);
            FrmBuild form = new FrmBuild(link, tableName);
            form.OpenUniqueWindow(title, this.dockPanel1);
        }

        public void ShowData(Link link, string tableName, int top)
        {
            string title = string.Format("查看\"{0}\"表数据", tableName);
            FrmViewData form = new FrmViewData(link, tableName, top);
            form.OpenWindow(title, this.dockPanel1);
        }

        public void ShowSchema(Link link, string tableName)
        {
            string title = string.Format("查看\"{0}\"表结构", tableName);
            FrmViewSchema form = new FrmViewSchema(link, tableName);
            form.OpenWindow(title, this.dockPanel1);
        }

        public void ShowDesignTable(Link link, string tableName)
        {
            string title = string.Format("设计表\"{0}\"", tableName);
            FrmDesignTable form = new FrmDesignTable(link, tableName);
            form.OpenWindow(title, this.dockPanel1);
        }

        public void ShowSql(Link link, string sql)
        {
            string title = "表脚本";
            FrmExec form = new FrmExec(link, sql);
            form.OpenWindow(title, this.dockPanel1);
        }

        public void ShowBuildDoc(Link link, string tableName)
        {
            string title = string.Format("生成\"{0}\"数据库文档", link.DbParam.DbName);
            FrmBuildDoc form = new FrmBuildDoc(link, tableName);
            form.OpenWindow(title, this.dockPanel1);
        }

        public void ShowSync()
        {
            string title = "同步数据";
            FrmSync form = new FrmSync();
            form.OpenWindow(title, this.dockPanel1);
        }

        public void DoExecSQL()
        {
            BaseForm baseForm = Extensions_BaseForm.GetActivatedForm(this.dockPanel1);
            if (baseForm == null || !baseForm.GetType().Equals(typeof(FrmExec)))
                return;

            FrmExec form = baseForm as FrmExec;
            form.DoExec();
        }

        private void InitAppStart()
        {
            Splasher.Status = "Loading Folders...";
            System.Threading.Thread.Sleep(0);
            AppInit.InitProjectFolder();

            Splasher.Status = "Loading Files...";
            System.Threading.Thread.Sleep(1);

            Splasher.Status = "Loading Plug/Ins...";
            System.Threading.Thread.Sleep(2);

            Splasher.Status = "Loading Configs...";
            System.Threading.Thread.Sleep(3);

            //隐藏加载窗口
            Splasher.Close();
        }
    }
}
