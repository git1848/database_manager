using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataBaseFront.UI
{
    public partial class FrmSplash : BaseForm
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

        public string StatusInfo
        {
            get
            {
                return this.lblStatus.Text;
            }
            set
            {
                this.lblStatus.InvokeIfNeeded((str) =>
                {
                    this.lblStatus.Text = str;
                }, value);
            }
        }
    }
}
