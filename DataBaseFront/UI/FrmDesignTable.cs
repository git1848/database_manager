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
    public partial class FrmDesignTable : BaseForm
    {
        const string ConfigTrueValue = "True";
        const string ConfigFalseValue = "False";

        Link targetLink;
        string targetTableName;

        DataTable TableColumns = new DataTable();
        int TableColumnsCount = 0;
        int AddColumnIndex = 0;

        List<DesignTable> OperateSqls = new List<DesignTable>();

        public FrmDesignTable(Link link, string tableName)
        {
            InitializeComponent();

            targetLink = link;
            targetTableName = tableName;
        }

        private void FrmDesignTable_Load(object sender, EventArgs e)
        {
            this.DisplayTables();
        }

        private void DisplayTables()
        {
            #region 设置列名
            this.DgvGrid.Columns.Clear();

            DataGridViewTextBoxColumn dgvtc;
            DataGridViewComboBoxColumn dgvcbc;
            DataGridViewCheckBoxColumn dgvchkbc;

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.HeaderText = "序号";
            dgvtc.DisplayIndex = 0;
            dgvtc.Name = "LineNo";
            dgvtc.DataPropertyName = "LineNo";
            dgvtc.Width = 50;
            dgvtc.ReadOnly = false;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvtc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvGrid.Columns.Add(dgvtc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.HeaderText = "名";
            dgvtc.DisplayIndex = 1;
            dgvtc.Name = "Name";
            dgvtc.DataPropertyName = "Name";
            dgvtc.Width = 250;
            dgvtc.ReadOnly = false;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvGrid.Columns.Add(dgvtc);

            dgvcbc = new DataGridViewComboBoxColumn();
            dgvcbc.HeaderText = "类型";
            dgvcbc.DisplayIndex = 2;
            dgvcbc.Name = "DbType";
            dgvcbc.DataPropertyName = "DbType";
            dgvcbc.Width = 150;
            foreach (var item in targetLink.DbOperate.GetDBTypes())
            {
                dgvcbc.Items.Add(item.DBType);
            }
            dgvcbc.Items.Insert(0, "");
            dgvcbc.ReadOnly = false;
            dgvcbc.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvcbc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.DgvGrid.Columns.Add(dgvcbc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.HeaderText = "长度";
            dgvtc.DisplayIndex = 3;
            dgvtc.Name = "Length";
            dgvtc.DataPropertyName = "Length";
            dgvtc.Width = 80;
            dgvtc.ReadOnly = false;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvGrid.Columns.Add(dgvtc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.HeaderText = "小数点";
            dgvtc.DisplayIndex = 4;
            dgvtc.Name = "NumericScale";
            dgvtc.DataPropertyName = "NumericScale";
            dgvtc.Width = 80;
            dgvtc.ReadOnly = false;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvGrid.Columns.Add(dgvtc);

            dgvchkbc = new DataGridViewCheckBoxColumn();
            dgvchkbc.ReadOnly = true;
            dgvchkbc.HeaderText = "主键";
            dgvchkbc.DisplayIndex = 5;
            dgvchkbc.Name = "IsPrimaryKey";
            dgvchkbc.DataPropertyName = "IsPrimaryKey";
            dgvchkbc.Width = 80;
            dgvchkbc.ReadOnly = false;
            dgvchkbc.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvchkbc.TrueValue = ConfigTrueValue;
            dgvchkbc.FalseValue = ConfigFalseValue;
            this.DgvGrid.Columns.Add(dgvchkbc);
            #endregion

            #region 绑定值
            this.TableColumns.Columns.Add(new DataColumn("LineNo", typeof(int)));

            this.TableColumns.Columns.Add(new DataColumn("OriginalName", typeof(string)));
            this.TableColumns.Columns.Add(new DataColumn("OriginalDbType", typeof(string)));
            this.TableColumns.Columns.Add(new DataColumn("OriginalLength", typeof(int)));
            this.TableColumns.Columns.Add(new DataColumn("OriginalNumericScale", typeof(int)));
            this.TableColumns.Columns.Add(new DataColumn("OriginalIsPrimaryKey", typeof(bool)));

            this.TableColumns.Columns.Add(new DataColumn("Name", typeof(string)));
            this.TableColumns.Columns.Add(new DataColumn("DbType", typeof(string)));
            this.TableColumns.Columns.Add(new DataColumn("Length", typeof(int)));
            this.TableColumns.Columns.Add(new DataColumn("NumericScale", typeof(int)));
            this.TableColumns.Columns.Add(new DataColumn("IsPrimaryKey", typeof(bool)));

            var columns = targetLink.DbOperate.GetColumns(targetTableName);
            if (columns != null && columns.Count > 0)
            {
                DataRow row;
                foreach (var column in columns)
                {
                    row = this.TableColumns.NewRow();
                    row["LineNo"] = ++AddColumnIndex;

                    row["OriginalName"] = column.Name;
                    row["OriginalDbType"] = column.DbType;
                    row["OriginalLength"] = column.Length;
                    row["OriginalNumericScale"] = column.NumericScale;
                    row["OriginalIsPrimaryKey"] = column.IsPrimaryKey;

                    row["Name"] = column.Name;
                    row["DbType"] = column.DbType;
                    row["Length"] = column.Length;
                    row["NumericScale"] = column.NumericScale;
                    row["IsPrimaryKey"] = column.IsPrimaryKey;

                    this.TableColumns.Rows.Add(row);
                }
            }

            this.DgvGrid.AutoGenerateColumns = false;
            this.DgvGrid.DataSource = this.TableColumns;

            this.TableColumnsCount = this.TableColumns.Rows.Count;
            #endregion
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            this.SaveOperateSql();
        }

        private void tsbAddColumn_Click(object sender, EventArgs e)
        {
            DataRow row = this.TableColumns.NewRow();
            row["LineNo"] = ++AddColumnIndex;

            row["OriginalName"] = string.Empty;
            row["OriginalDbType"] = string.Empty;
            row["OriginalLength"] = 0;
            row["OriginalNumericScale"] = 0;
            row["OriginalIsPrimaryKey"] = false;

            row["Name"] = string.Empty;
            row["DbType"] = string.Empty;
            row["Length"] = 0;
            row["NumericScale"] = 0;
            row["IsPrimaryKey"] = false;
            this.TableColumns.Rows.Add(row);
        }

        private void tsbRemoveColumn_Click(object sender, EventArgs e)
        {
            if (this.DgvGrid.CurrentCell != null && this.DgvGrid.CurrentCell.RowIndex != -1)
            {
                DataRow dtRow = this.TableColumns.Rows[this.DgvGrid.CurrentRow.Index];
                DataGridViewRow dgvRow = this.DgvGrid.CurrentRow;
                int selectLineNo = Utils.StrToInt(dtRow["LineNo"].ToString(), 0);

                string columnOriginalName = dtRow["OriginalName"].ToString();

                string columnNewName = dgvRow.Cells["Name"].Value.ToString();

                DesignTable designTable = new DesignTable()
                {
                    Action = DesignTableAction.DeleteColumn,
                    LineNo = selectLineNo,

                    ColumnOriginalName = columnOriginalName,

                    ColumnName = columnNewName
                };
                this.OperateSqls.RemoveAll(o => ((o.Action != DesignTableAction.DeleteColumn && o.Action != DesignTableAction.UpdateColumnName) && o.LineNo == designTable.LineNo && o.ColumnName == designTable.ColumnName));
                if (columnOriginalName.Equals(columnNewName))
                {
                    this.ModifyColumnNameFromOperateSqls(selectLineNo, columnNewName);
                }
                this.OperateSqls.Add(designTable);

                this.TableColumns.Rows.RemoveAt(this.DgvGrid.CurrentCell.RowIndex);
            }
        }

        private void DgvGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dtRow = this.TableColumns.Rows[e.RowIndex];
            DataGridViewRow dgvRow = this.DgvGrid.Rows[e.RowIndex];
            int selectLineNo = Utils.StrToInt(dtRow["LineNo"].ToString(), 0);

            string columnOriginalName = dtRow["OriginalName"].ToString();
            string columnOriginalDbType = dtRow["OriginalDbType"].ToString();
            int columnOriginalLength = Utils.StrToInt(dtRow["OriginalLength"].ToString(), 0);
            int columnOriginalNumericScale = Utils.StrToInt(dtRow["OriginalNumericScale"].ToString(), 0);
            bool columnOriginalIsPrimaryKey = Utils.StrToBool(dtRow["OriginalIsPrimaryKey"].ToString(), false);

            string columnNewName = dgvRow.Cells["Name"].Value.ToString();
            string columnNewType = dgvRow.Cells["DbType"].Value.ToString();
            int columnNewLength = Utils.StrToInt(dgvRow.Cells["Length"].Value.ToString(), 0);
            int columnNewNumericScale = Utils.StrToInt(dgvRow.Cells["NumericScale"].Value.ToString(), 0);
            bool columnNewIsPrimaryKey = dgvRow.Cells["IsPrimaryKey"].FormattedValue.ToString().Equals(ConfigTrueValue);

            DesignTable designTable = new DesignTable()
            {
                LineNo = selectLineNo,

                ColumnOriginalName = columnOriginalName,
                ColumnOriginalDbType = columnOriginalDbType,
                ColumnOriginalLength = columnOriginalLength,
                ColumnOriginalNumericScale = columnOriginalNumericScale,
                ColumnOriginalIsPrimaryKey = columnOriginalIsPrimaryKey,

                ColumnName = columnNewName,
                ColumnDbType = columnNewType,
                ColumnLength = columnNewLength,
                ColumnNumericScale = columnNewNumericScale,
                ColumnIsPrimaryKey = columnNewIsPrimaryKey,

                ActionTime = DateTime.Now,
            };

            if (selectLineNo > this.TableColumnsCount)//如果当前的序号大于该表的原列数，就为新增，否则为修改
            {
                //新增
                designTable.Action = DesignTableAction.InsertColumn;

                this.OperateSqls.RemoveAll(o => (o.Action == designTable.Action && o.LineNo == designTable.LineNo));

                this.OperateSqls.Add(designTable);
            }
            else
            {
                //修改

                //UpdateColumnName
                if (e.ColumnIndex == 1)
                {
                    designTable.Action = DesignTableAction.UpdateColumnName;
                    if (columnOriginalName.Equals(columnNewName))
                    {
                        this.OperateSqls.RemoveAll(o => (o.Action == designTable.Action && o.LineNo == designTable.LineNo));

                        this.ModifyColumnNameFromOperateSqls(selectLineNo, columnNewName);
                    }
                    else
                    {
                        this.OperateSqls.Add(designTable);
                    }
                }

                //UpdateColumnDbType
                if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {
                    designTable.Action = DesignTableAction.UpdateColumnDbType;
                    if (!columnOriginalDbType.Equals(columnNewType) || !columnOriginalLength.Equals(columnNewLength) || !columnOriginalNumericScale.Equals(columnNewNumericScale))
                        this.OperateSqls.RemoveAll(o => (o.Action == designTable.Action && o.LineNo == designTable.LineNo && o.ColumnName == designTable.ColumnName));

                    this.OperateSqls.Add(designTable);
                }

                //UpdateColumnPrimaryKey
                if (e.ColumnIndex == 5)
                {
                    designTable.Action = DesignTableAction.UpdateColumnPrimaryKey;
                    if (columnOriginalIsPrimaryKey.Equals(columnNewIsPrimaryKey))
                        this.OperateSqls.RemoveAll(o => (o.Action == designTable.Action && o.LineNo == designTable.LineNo && o.ColumnName == designTable.ColumnName));
                    else
                        this.OperateSqls.Add(designTable);
                }
            }
        }

        private void DgvGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.txtOperateLog.Text = "错误：" + e.Exception.Message;
        }

        private void ModifyColumnNameFromOperateSqls(int selectLineNo, string column_name)
        {
            foreach (var item in this.OperateSqls)
            {
                if (item.LineNo == selectLineNo)
                {
                    item.ColumnName = column_name;
                }
            }
        }

        private void SaveOperateSql()
        {
            if (this.OperateSqls.Count > 0)
            {
                this.txtOperateLog.Clear();

                var sortOperateSqls = (from o in OperateSqls
                                       orderby o.Action ascending, o.ActionTime ascending
                                       select o).ToList();

                foreach (var operateSql in sortOperateSqls)
                {
                    switch (operateSql.Action)
                    {
                        case DesignTableAction.InsertColumn:
                            operateSql.Sql = targetLink.DbOperate.BuildInsertColumnSql(targetTableName, operateSql.ColumnName, operateSql.ColumnDbType, operateSql.ColumnLength, operateSql.ColumnNumericScale, operateSql.ColumnIsPrimaryKey);
                            break;
                        case DesignTableAction.DeleteColumn:
                            operateSql.Sql = targetLink.DbOperate.BuildDeleteColumnSql(targetTableName, operateSql.ColumnName);
                            break;
                        case DesignTableAction.UpdateColumnName:
                            operateSql.Sql = targetLink.DbOperate.BuildUpdateColumnNameSql(targetTableName, operateSql.ColumnOriginalName, operateSql.ColumnName);
                            break;
                        case DesignTableAction.UpdateColumnDbType:
                            operateSql.Sql = targetLink.DbOperate.BuildUpdateColumnDbTypeSql(targetTableName, operateSql.ColumnName, operateSql.ColumnDbType, operateSql.ColumnLength, operateSql.ColumnNumericScale);
                            break;
                        case DesignTableAction.UpdateColumnPrimaryKey:
                            operateSql.Sql = targetLink.DbOperate.BuildUpdateColumnPrimaryKeySql(targetTableName, operateSql.ColumnName, operateSql.ColumnIsPrimaryKey);
                            break;
                    }

                    this.txtOperateLog.AppendText(string.Format("{0}=>{1}\r\n{2}\r\n\r\n", operateSql.ColumnOriginalName, operateSql.Action, operateSql.Sql));
                }
            }
            else
            {
                this.txtOperateLog.Text = "暂无操作";
            }
        }
    }
}
