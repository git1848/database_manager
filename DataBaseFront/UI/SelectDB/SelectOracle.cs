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
    public partial class SelectOracle : UserControl
    {
        #region 单例
        private static SelectOracle instance;
        public static SelectOracle Instance
        {
            get
            {
                lock (typeof(SelectOracle))
                {
                    if (instance == null)
                        instance = new SelectOracle();
                    return instance;
                }
            }
        }
        #endregion

        public SelectOracle()
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
