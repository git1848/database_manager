using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseFront.Entity;
using DataBaseFront.Controls;

namespace DataBaseFront.UI.UIControls
{
    public partial class UCSelectColumns : UserControl
    {
        const string ConfigTrueValue = "True";
        const string ConfigFalseValue = "False";

        public UCSelectColumns()
        {
            InitializeComponent();
        }

        public void Init(Link link, string tableName)
        {
            this.DgvGrid.AutoGenerateColumns = false;

            DatagridViewCheckBoxHeaderCell chkCheckBoxHeader = new DatagridViewCheckBoxHeaderCell();
            chkCheckBoxHeader.OnCheckBoxClicked += chkCheckBoxHeader_OnCheckBoxClicked;

            this.DgvGrid.Columns.Clear();
            this.DgvGrid.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                ReadOnly = false,
                Name = "No",
                Width = 60,
                TrueValue = ConfigTrueValue,
                FalseValue = ConfigFalseValue,
                HeaderCell = chkCheckBoxHeader
            });

            this.DgvGrid.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Index", DataPropertyName = "Index", HeaderText = "序号", Width = 60 },
                new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "字段名", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "Remark", DataPropertyName = "Remark", HeaderText = "字段说明", Width = 100 },
                new DataGridViewCheckBoxColumn { Name = "AutoIncrement", DataPropertyName = "AutoIncrement", HeaderText = "自动增长", Width = 100 },
                new DataGridViewCheckBoxColumn { Name = "IsPrimaryKey", DataPropertyName = "IsPrimaryKey", HeaderText = "主键", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DbType", DataPropertyName = "DbType", HeaderText = "类型", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Length", DataPropertyName = "Length", HeaderText = "长度", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "NumericPrecision", DataPropertyName = "NumericPrecision", HeaderText = "精度", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "NumericScale", DataPropertyName = "NumericScale", HeaderText = "小数点", Width = 100 },
                new DataGridViewCheckBoxColumn { Name = "AllowNull", DataPropertyName = "AllowNull", HeaderText = "允许空", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DefaultValue", DataPropertyName = "DefaultValue", HeaderText = "默认值", Width = 100 }
                );

            //将表结构显示到DataGridView中
            IList<MGColumn> columnNames = link.DbOperate.GetColumns(tableName);
            this.DgvGrid.DataSource = columnNames;

            //默认全部选中
            foreach (DataGridViewRow row in this.DgvGrid.Rows)
                row.Cells[0].Value = ConfigTrueValue;
        }

        private void chkCheckBoxHeader_OnCheckBoxClicked(bool state)
        {
            //设置DataGridView的DataGridViewCheckBoxColumn的选中
            string checkValue = state ? ConfigTrueValue : ConfigFalseValue;
            foreach (DataGridViewRow row in this.DgvGrid.Rows)
                row.Cells[0].Value = checkValue;
        }

        public List<ModelEntity> SelectColumns
        {
            get
            {
                List<ModelEntity> selectItems = new List<ModelEntity>();
                ModelEntity selectItem;
                string columnName, columnType;
                foreach (DataGridViewRow row in this.DgvGrid.Rows)
                {
                    if (row.Cells[0].FormattedValue.ToString().Equals(ConfigTrueValue))
                    {
                        columnName = row.Cells["Name"].Value.ToString();
                        columnType = row.Cells["DbType"].Value.ToString();
                        selectItem = new ModelEntity()
                        {
                            ColumnName = columnName,
                            PrivateColumnName = string.Format("_{0}{1}", columnName.Substring(0, 1).ToLower(), columnName.Substring(1)),
                            ColumnType = DBTypeUtil.ConvertDbTypeToCShapeType(columnType),
                            ColumnDesc = row.Cells["Remark"].Value.ToString(),
                        };
                        selectItems.Add(selectItem);
                    }
                }
                return selectItems;
            }
        }
    }
}
