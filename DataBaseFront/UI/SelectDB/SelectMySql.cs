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
    public partial class SelectMySql : UserControl
    {
        #region 单例
        private static SelectMySql instance;
        public static SelectMySql Instance
        {
            get
            {
                lock (typeof(SelectMySql))
                {
                    if (instance == null)
                        instance = new SelectMySql();
                    return instance;
                }
            }
        }
        #endregion

        public SelectMySql()
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

        public int Port
        {
            get
            {
                return Convert.ToInt32(this.txtPort.Text.Trim());
            }
            set
            {
                this.txtPort.Text = value.ToString();
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
