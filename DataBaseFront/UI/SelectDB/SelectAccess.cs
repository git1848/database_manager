using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataBaseFront.UI.SelectDB
{
    [DesignTimeVisible(false)]
    public partial class SelectAccess : UserControl
    {
        #region 单例
        private static SelectAccess instance;
        public static SelectAccess Instance
        {
            get
            {
                lock (typeof(SelectAccess))
                {
                    if (instance == null)
                        instance = new SelectAccess();
                    return instance;
                }
            }
        }
        #endregion

        public SelectAccess()
        {
            InitializeComponent();
        }

        #region 属性

        public string DbPath
        {
            get
            {
                return this.txtDbPath.Text.Trim();
            }
            set
            {
                this.txtDbPath.Text = value;
            }
        }

        public bool HasPassword
        {
            get
            {
                return this.chkHasPassword.Checked;
            }
            set
            {
                this.chkHasPassword.Checked = value;
            }
        }

        public string DbPassword
        {
            get
            {
                return this.txtPassword.Text.Trim();
            }
            set
            {
                this.txtPassword.Text = value;
            }
        }
        #endregion

        private void chkHasPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkHasPassword.Checked)
            {
                this.lblForPassword.Visible = true;
                this.txtPassword.Visible = true;
            }
            else
            {
                this.lblForPassword.Visible = false;
                this.txtPassword.Visible = false;
            }
        }

        private void btnSelectDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Access 数据库|*.mdb;*.accdb|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                this.txtDbPath.Text = fileName;
            }
        }
    }
}
