using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace DataBaseFront.UI
{
    public class BaseForm : DockContentEx
    {
        protected override void OnLoad(EventArgs e)
        {
            this.Activate();

            base.OnLoad(e);
        }
    }
}
