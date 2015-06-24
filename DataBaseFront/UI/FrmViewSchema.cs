using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseFront.Entity;

namespace DataBaseFront.UI
{
    public partial class FrmViewSchema : BaseForm
    {
        Link targetLink;
        string targetTableName;

        public FrmViewSchema(Link link, string tableName)
        {
            InitializeComponent();

            targetLink = link;
            targetTableName = tableName;
        }

        private void FrmViewSchema_Load(object sender, EventArgs e)
        {
            //将表结构显示到DataGridView中
            IList<MGColumn> columnNames = targetLink.DbOperate.GetColumns(targetTableName);
            this.DgvGrid.DataSource = columnNames;
        }
    }
}
