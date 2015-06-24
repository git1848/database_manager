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
    public partial class SelectSqlServer : UserControl
    {
        #region 单例
        private static SelectSqlServer instance;
        public static SelectSqlServer Instance
        {
            get
            {
                lock (typeof(SelectSqlServer))
                {
                    if (instance == null)
                        instance = new SelectSqlServer();
                    return instance;
                }
            }
        }
        #endregion

        public SelectSqlServer()
        {
            InitializeComponent();
        }

        #region 属性
        public string ServerName
        {
            get
            {
                return this.txtServerName.Text.Trim();
            }
            set
            {
                this.txtServerName.Text = value;
            }
        }
        public string UserID
        {
            get
            {
                return this.txtUserID.Text.Trim();
            }
            set
            {
                this.txtUserID.Text = value;
            }
        }
        public string UserPass
        {
            get
            {
                return this.txtUserPass.Text.Trim();
            }
            set
            {
                this.txtUserPass.Text = value;
            }
        }
        #endregion
    }
}
