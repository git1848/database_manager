using System;
using System.Data;
using System.Windows.Forms;
using DataBaseFront.Entity;

namespace DataBaseFront.UI
{
    public partial class FrmViewData : BaseForm
    {
        Link targetLink;
        string targetTableName;

        int topCount = 0;
        DataTable dtSource;

        public FrmViewData(Link link, string tableName, int _topCount)
        {
            InitializeComponent();

            targetLink = link;
            targetTableName = tableName;
            topCount = _topCount;
        }

        private void FrmViewData_Load(object sender, EventArgs e)
        {
            //组装SQL语句
            string sql = string.Empty;
            if (topCount == 0)
                sql = targetLink.DbOperate.BuildSelectTableSql(targetTableName);
            else
                sql = targetLink.DbOperate.BuildSelectTableSql(targetTableName, topCount);

            //查询数据
            dtSource = targetLink.DbOperate.GetDataTable(sql);

            //设置分页控件
            this.pager1.PageIndex = 1;
            this.pager1.PageSize = 100;
            this.pager1.Bind();
        }

        private int DataBind()
        {
            //分页显示数据
            this.DgvGrid.DataSource = dtSource.PagedTable(pager1.PageIndex, pager1.PageSize);

            //返回总行数
            return dtSource.Rows.Count;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //单击显示DataGridView单元格的文本
            string value = this.DgvGrid.CurrentCell.Value.ToString();
            this.txtSelectCell.Text = value;
        }

        private int pager1_EventPaging(Controls.EventPagingArg e)
        {
            return this.DataBind();
        }

    }
}
