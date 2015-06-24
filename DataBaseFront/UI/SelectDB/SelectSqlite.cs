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
    public partial class SelectSqlite : UserControl
    {
        #region 单例
        private static SelectSqlite instance;
        public static SelectSqlite Instance
        {
            get
            {
                lock (typeof(SelectSqlite))
                {
                    if (instance == null)
                        instance = new SelectSqlite();
                    return instance;
                }
            }
        }
        #endregion

        public SelectSqlite()
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
        #endregion

        private void btnSelectDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Sqlite3 Databases|*.db;*.db3|所有文件|*.*";
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
