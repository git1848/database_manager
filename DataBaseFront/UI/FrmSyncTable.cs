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
    public partial class FrmSyncTable : Form
    {
        public FrmSyncTable()
        {
            InitializeComponent();
        }

        public Entity.SyncObject SetSyncObject { get; set; }
        public Dictionary<string, string> SetSyncTableColumns { get; set; }

        private void FrmSyncTable_Load(object sender, EventArgs e)
        {
            this.lblFromTable.Text = SetSyncObject.From.Table.Name;

            this.lblToTable.Text = SetSyncObject.To.Table.Name;

            this.DisplayColumns();
        }

        private void DisplayColumns()
        {
            #region 设置列名
            this.dgvColumns.Columns.Clear();

            DataGridViewTextBoxColumn dgvtc;
            DataGridViewComboBoxColumn dgvcbc;

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.ReadOnly = true;
            dgvtc.HeaderText = "序号";
            dgvtc.DisplayIndex = 0;
            dgvtc.Name = "ColumnIndex";
            dgvtc.DataPropertyName = "Index";
            dgvtc.Width = 60;
            dgvtc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvColumns.Columns.Add(dgvtc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.ReadOnly = true;
            dgvtc.HeaderText = "源>字段";
            dgvtc.DisplayIndex = 1;
            dgvtc.Name = "ColumnFromColumn";
            dgvtc.DataPropertyName = "FromColumn";
            dgvtc.Width = 150;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvColumns.Columns.Add(dgvtc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.ReadOnly = true;
            dgvtc.HeaderText = "源>类型";
            dgvtc.DisplayIndex = 2;
            dgvtc.Name = "ColumnFromType";
            dgvtc.DataPropertyName = "FromType";
            dgvtc.Width = 100;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvColumns.Columns.Add(dgvtc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.ReadOnly = true;
            dgvtc.HeaderText = "同步";
            dgvtc.DisplayIndex = 3;
            dgvtc.Name = "ColumnMove";
            dgvtc.DataPropertyName = "Move";
            dgvtc.Width = 40;
            dgvtc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvColumns.Columns.Add(dgvtc);

            dgvcbc = new DataGridViewComboBoxColumn();
            dgvcbc.HeaderText = "目标>字段";
            dgvcbc.DisplayIndex = 4;
            dgvcbc.Name = "ColumnToColumn";
            dgvcbc.DataPropertyName = "ToColumn";
            dgvcbc.Width = 150;
            foreach (var item in SetSyncObject.To.Columns)
            {
                dgvcbc.Items.Add(item.Name);
            }

            dgvcbc.Items.Insert(0, "");
            dgvcbc.ReadOnly = false;
            dgvcbc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvColumns.Columns.Add(dgvcbc);

            dgvtc = new DataGridViewTextBoxColumn();
            dgvtc.ReadOnly = true;
            dgvtc.HeaderText = "目标>类型";
            dgvtc.DisplayIndex = 5;
            dgvtc.Name = "ColumnToType";
            dgvtc.DataPropertyName = "ToType";
            dgvtc.Width = 100;
            dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvColumns.Columns.Add(dgvtc);
            #endregion

            #region 绑定值
            DataTable dtData = new DataTable();

            dtData.Columns.Add(new DataColumn("Index", typeof(int)));
            dtData.Columns.Add(new DataColumn("FromColumn", typeof(string)));
            dtData.Columns.Add(new DataColumn("FromType", typeof(string)));
            dtData.Columns.Add(new DataColumn("Move", typeof(string)));
            dtData.Columns.Add(new DataColumn("ToColumn", typeof(string)));
            dtData.Columns.Add(new DataColumn("ToType", typeof(string)));

            MGColumn fromColumn, toColumn;
            DataRow row;
            int fromCount = SetSyncObject.From.Columns.Count;
            int toCount = SetSyncObject.To.Columns.Count;
            int count = fromCount > toCount ? fromCount : toCount;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i < fromCount)
                        fromColumn = SetSyncObject.From.Columns[i];
                    else
                        fromColumn = null;

                    if (i < toCount)
                        toColumn = SetSyncObject.To.Columns[i];
                    else
                        toColumn = null;

                    row = dtData.NewRow();
                    row["Index"] = (i + 1);
                    row["FromColumn"] = fromColumn == null ? "" : fromColumn.Name;
                    row["FromType"] = fromColumn == null ? "" : DBTypeUtil.ConvertDbTypeToCShapeType(fromColumn.DbType);
                    row["Move"] = " => ";
                    row["ToColumn"] = "";
                    row["ToType"] = "";
                    dtData.Rows.Add(row);
                }
            }
            this.dgvColumns.AutoGenerateColumns = false;
            this.dgvColumns.DataSource = dtData;
            #endregion
        }

        private DataGridViewComboBoxEditingControl dataGridViewComboBox = null;
        private void dgvColumns_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl
                && this.dgvColumns.CurrentCell.ColumnIndex == 4
                && this.dgvColumns.CurrentCell.RowIndex != -1)
            {
                this.dataGridViewComboBox = (DataGridViewComboBoxEditingControl)e.Control;

                //增加委托处理
                dataGridViewComboBox.SelectionChangeCommitted += dataGridViewComboBox_SelectionChangeCommitted;
            }
        }

        private void dgvColumns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewComboBox != null)
            {
                dataGridViewComboBox.SelectionChangeCommitted -= new EventHandler(this.dataGridViewComboBox_SelectionChangeCommitted);
                this.dataGridViewComboBox = null;
            }
        }

        void dataGridViewComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string selectColumnName = cb.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selectColumnName))
            {
                this.dgvColumns.CurrentRow.Cells["ColumnToType"].Value = string.Empty;
            }
            else
            {
                var selectColumn = SetSyncObject.To.Columns.Where(o => o.Name == selectColumnName).FirstOrDefault();
                this.dgvColumns.CurrentRow.Cells["ColumnToType"].Value = DBTypeUtil.ConvertDbTypeToCShapeType(selectColumn.DbType);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            bool isShowTip = true;
            var selectColumns = this.GetSelectColumns(isShowTip, out isValid);
            if (!isValid) return;

            if (selectColumns.Count == 0)
            {
                MessageUtil.ShowTips("请设置列匹配");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("表：");
            sb.AppendFormat("  {0}=>{1}\n", this.lblFromTable.Text, this.lblToTable.Text);
            sb.AppendLine("字段：");
            foreach (var selectColumn in selectColumns)
            {
                sb.AppendFormat("  {0}=>{1}\n", selectColumn.Key, selectColumn.Value);
            }

            MessageUtil.ShowTips(sb.ToString());

            this.SetSyncTableColumns = selectColumns;

            this.DialogResult = DialogResult.OK;
        }

        private Dictionary<string, string> GetSelectColumns(bool isShowTip, out bool isValid)
        {
            isValid = true;
            string fromColumn = string.Empty;
            string fromColumnType = string.Empty;
            string toColumn = string.Empty;
            string toColumnType = string.Empty;

            Dictionary<string, string> columns = new Dictionary<string, string>();

            for (int i = 0; i < this.dgvColumns.Rows.Count; i++)
            {
                fromColumn = this.dgvColumns.Rows[i].Cells["ColumnFromColumn"].Value.ToString();
                fromColumnType = this.dgvColumns.Rows[i].Cells["ColumnFromType"].Value.ToString();
                toColumn = this.dgvColumns.Rows[i].Cells["ColumnToColumn"].Value.ToString();
                toColumnType = this.dgvColumns.Rows[i].Cells["ColumnToType"].Value.ToString();

                if (!string.IsNullOrEmpty(fromColumn) && !string.IsNullOrEmpty(toColumn))
                {
                    if (!fromColumnType.Equals(toColumnType, StringComparison.OrdinalIgnoreCase))
                    {
                        if (isShowTip)
                        {
                            isValid = false;

                            MessageUtil.ShowWarning(string.Format("字段类型不一致：\n{0}({1})=>{2}({3})\n"
                                                    , fromColumn, fromColumnType, toColumn, toColumnType));
                        }
                        break;
                    }

                    columns.Add(fromColumn, toColumn);
                }
            }
            return columns;
        }

        private void chkSelected_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.chkSelected.Checked;
            if (flag)
            {
                this.chkSelected.Text = "取消选择";

                for (int i = 0; i < this.dgvColumns.Rows.Count; i++)
                {
                    string fromColumnName = this.dgvColumns.Rows[i].Cells["ColumnFromColumn"].Value.ToString();
                    if (!string.IsNullOrEmpty(fromColumnName))
                    {
                        foreach (var toColumn in SetSyncObject.To.Columns)
                        {
                            if (fromColumnName.Equals(toColumn.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                this.dgvColumns.Rows[i].Cells["ColumnToColumn"].Value = toColumn.Name;
                                this.dgvColumns.Rows[i].Cells["ColumnToType"].Value = DBTypeUtil.ConvertDbTypeToCShapeType(toColumn.DbType);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                this.chkSelected.Text = "自动选择";

                for (int i = 0; i < this.dgvColumns.Rows.Count; i++)
                {
                    this.dgvColumns.Rows[i].Cells["ColumnToColumn"].Value = "";
                    this.dgvColumns.Rows[i].Cells["ColumnToType"].Value = "";
                }
            }
        }
    }
}

