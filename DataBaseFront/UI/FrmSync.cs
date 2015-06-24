using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseFront.DB.DbOperates;
using DataBaseFront.DB.DbParams;
using DataBaseFront.Entity;
using DataBaseFront.Properties;

namespace DataBaseFront.UI
{
    public partial class FrmSync : BaseForm
    {
        private Dictionary<int, SyncObject> SyncObjects;

        public FrmSync()
        {
            InitializeComponent();
        }

        private void FrmSync_Load(object sender, EventArgs e)
        {
            //设置源和目标的连接
            this.GetLink(this.cmbLinkFrom, this.cmbLinkTo);

            //设置表格
            this.DisignTable();
        }

        #region 源
        private void cmbLinkFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFrom = true;
            bool isSetNull = false;

            if (this.cmbLinkTo.SelectedItem != null && this.cmbLinkFrom.SelectedItem.ToString().Equals(this.cmbLinkTo.SelectedItem.ToString()))
            {
                MessageUtil.ShowWarning("“源”与“目标”的连接不能设置一致");

                if (SyncObjects != null && SyncObjects.Count > 0)
                {
                    foreach (var syncObject in SyncObjects)
                    {
                        syncObject.Value.From = null;
                    }
                }

                isSetNull = true;
            }

            this.GetDBTable(this.cmbLinkFrom, this.lblDbFrom, isFrom, isSetNull);
        }
        #endregion

        #region 目标
        private void cmbLinkTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFrom = false;
            bool isSetNull = false;

            if (this.cmbLinkFrom.SelectedItem != null && this.cmbLinkTo.SelectedItem.ToString().Equals(this.cmbLinkFrom.SelectedItem.ToString()))
            {
                MessageUtil.ShowWarning("“源”与“目标”的连接不能设置一致");

                if (SyncObjects != null && SyncObjects.Count > 0)
                {
                    foreach (var syncObject in SyncObjects)
                    {
                        syncObject.Value.To = null;
                    }
                }

                isSetNull = true;
            }

            this.GetDBTable(this.cmbLinkTo, this.lblDbTo, isFrom, isSetNull);
        }
        #endregion

        private void GetLink(ComboBox _cmbFrom, ComboBox _cmbTo)
        {
            using (new AutoWaitCursor(this))
            {
                var links = LinkUtil.Instance.GetLinks();
                if (links != null && links.Count > 0)
                {
                    _cmbFrom.Items.Clear();
                    _cmbFrom.DisplayMember = "Text";
                    _cmbFrom.ValueMember = "Value";

                    _cmbTo.Items.Clear();
                    _cmbTo.DisplayMember = "Text";
                    _cmbTo.ValueMember = "Value";

                    ListItem item;
                    foreach (var link in links)
                    {
                        item = new ListItem(
                                        string.Format("{0} - {1} - {2}", link.Key, link.Value.DbName, link.Value.DbProvider.ToString())
                                        , link.Key
                                        , link);

                        _cmbFrom.Items.Add(item);
                        _cmbTo.Items.Add(item);
                    }
                    item = null;

                    links = null;
                }
            }
        }

        private void GetDBTable(ComboBox cmbLink, Label lblDB, bool isFrom, bool isSetNull)
        {
            using (new AutoWaitCursor(this))
            {
                //选择
                ListItem selectItem = cmbLink.SelectedItem as ListItem;
                if (selectItem == null) return;

                //选择的连接
                KeyValuePair<string, IDbParam> selectLink = (KeyValuePair<string, IDbParam>)selectItem.Tag;

                //组装连接
                Link link = new Link()
                {
                    AliasName = selectLink.Key,
                    DbParam = selectLink.Value,
                    DbOperate = null,
                    HasOpen = false
                };

                //生成数据库访问对象
                LinkUtil.Instance.BuildDbOperate(link);

                //获取数据库名称
                lblDB.Text = link.DbParam.DbName;

                //重置源和目标
                if (SyncObjects != null && SyncObjects.Count > 0)
                {
                    foreach (var syncObject in SyncObjects)
                    {
                        if (isFrom)
                            syncObject.Value.From = null;
                        else
                            syncObject.Value.To = null;
                    }
                }

                //获取表
                var tables = link.DbOperate.GetTableInfos();
                if (tables != null && tables.Count > 0)
                {
                    if (SyncObjects == null)
                        SyncObjects = new Dictionary<int, SyncObject>();

                    SyncObject syncObject;
                    for (int i = 0; i < tables.Count; i++)
                    {
                        if (!SyncObjects.ContainsKey(i))
                        {
                            SyncObjects.Add(i, null);
                        }

                        syncObject = SyncObjects[i];

                        if (syncObject == null)
                            syncObject = new SyncObject();

                        if (isFrom)
                        {
                            if (!isSetNull)
                                syncObject.From = tables[i];
                            else
                                syncObject.From = null;
                        }
                        else
                        {
                            if (!isSetNull)
                                syncObject.To = tables[i];
                            else
                                syncObject.To = null;
                        }

                        SyncObjects[i] = syncObject;
                    }
                    syncObject = null;
                }

                //显示数据
                this.DisplayTable();
            }
        }

        private void DisignTable()
        {
            using (new AutoWaitCursor(this))
            {
                #region 设置列名
                this.dgvTable.Columns.Clear();

                DataGridViewTextBoxColumn dgvtc;
                DataGridViewComboBoxColumn dgvcbc;
                DataGridViewButtonColumn dgvbtn;
                DataGridViewImageColumn dgvibtn;

                dgvtc = new DataGridViewTextBoxColumn();
                dgvtc.ReadOnly = true;
                dgvtc.HeaderText = "序号";
                dgvtc.DisplayIndex = 0;
                dgvtc.Name = "ColumnIndex";
                dgvtc.DataPropertyName = "Index";
                dgvtc.Width = 60;
                dgvtc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvTable.Columns.Add(dgvtc);

                dgvtc = new DataGridViewTextBoxColumn();
                dgvtc.ReadOnly = true;
                dgvtc.HeaderText = "源";
                dgvtc.DisplayIndex = 1;
                dgvtc.Name = "ColumnFrom";
                dgvtc.DataPropertyName = "From";
                dgvtc.Width = 200;
                dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvTable.Columns.Add(dgvtc);

                dgvtc = new DataGridViewTextBoxColumn();
                dgvtc.ReadOnly = true;
                dgvtc.HeaderText = "同步";
                dgvtc.DisplayIndex = 2;
                dgvtc.Name = "ColumnMove";
                dgvtc.DataPropertyName = "Move";
                dgvtc.Width = 40;
                dgvtc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvtc.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvTable.Columns.Add(dgvtc);

                dgvcbc = new DataGridViewComboBoxColumn();
                dgvcbc.HeaderText = "目标";
                dgvcbc.DisplayIndex = 3;
                dgvcbc.Name = "ColumnTo";
                dgvcbc.DataPropertyName = "To";
                dgvcbc.Width = 200;
                dgvcbc.ReadOnly = false;
                dgvcbc.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvTable.Columns.Add(dgvcbc);

                dgvbtn = new DataGridViewButtonColumn();
                dgvbtn.ReadOnly = true;
                dgvbtn.HeaderText = "设置列";
                dgvbtn.DisplayIndex = 4;
                dgvbtn.Name = "SetColumn";
                dgvbtn.Width = 80;
                dgvbtn.DefaultCellStyle.NullValue = "设置";
                dgvbtn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvbtn.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvTable.Columns.Add(dgvbtn);

                dgvibtn = new DataGridViewImageColumn();
                dgvibtn.ReadOnly = true;
                dgvibtn.HeaderText = "设置列";
                dgvibtn.DisplayIndex = 5;
                dgvibtn.Name = "SelectColumn";
                dgvibtn.Width = 80;
                dgvibtn.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvTable.Columns.Add(dgvibtn);
                #endregion
            }
        }

        private void DisplayTable()
        {
            using (new AutoWaitCursor(this))
            {
                #region 绑定值
                //
                DataTable dtData = new DataTable();

                dtData.Columns.Add(new DataColumn("Index", typeof(int)));
                dtData.Columns.Add(new DataColumn("From", typeof(string)));
                dtData.Columns.Add(new DataColumn("Move", typeof(string)));
                dtData.Columns.Add(new DataColumn("To", typeof(string)));

                //
                MGTableInfo fromTable, toTable;
                DataRow row;
                for (int i = 0; i < SyncObjects.Count; i++)
                {
                    fromTable = SyncObjects[i].From;
                    toTable = SyncObjects[i].To;

                    row = dtData.NewRow();
                    row["Index"] = (i + 1);
                    row["From"] = fromTable == null ? "" : fromTable.Table.Name;
                    row["Move"] = " => ";
                    row["To"] = "";
                    dtData.Rows.Add(row);
                }
                this.dgvTable.AutoGenerateColumns = false;
                this.dgvTable.DataSource = dtData;

                //
                DataGridViewComboBoxColumn cmbColumnTo = this.dgvTable.Columns["ColumnTo"] as DataGridViewComboBoxColumn;
                cmbColumnTo.Items.Clear();
                foreach (var item in SyncObjects.Values)
                {
                    if (item.To != null)
                    {
                        cmbColumnTo.Items.Add(item.To.Table.Name);
                    }
                }

                DataGridViewImageColumn btnSelectColumn = this.dgvTable.Columns["SelectColumn"] as DataGridViewImageColumn;
                foreach (var item in SyncObjects.Values)
                {
                    btnSelectColumn.Image = Resources.btnUnChecked;
                }
                #endregion
            }
        }

        private void dgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 4)
                {
                    string fromTableName = this.dgvTable.Rows[e.RowIndex].Cells["ColumnFrom"].Value.ToString();
                    if (fromTableName.IsNullOrEmpty())
                    {
                        MessageUtil.ShowWarning("请选择“源”表");
                        return;
                    }

                    string toTableName = this.dgvTable.Rows[e.RowIndex].Cells["ColumnTo"].Value.ToString();
                    if (toTableName.IsNullOrEmpty())
                    {
                        MessageUtil.ShowWarning("请选择“目标”表");
                        return;
                    }

                    var searchToTable = SyncObjects.Where(o => o.Value.To.Table.Name == toTableName).FirstOrDefault();

                    SyncObject syncObject = new SyncObject();
                    syncObject.From = SyncObjects[e.RowIndex].From;
                    syncObject.To = searchToTable.Value.To;

                    FrmSyncTable form = new FrmSyncTable();
                    form.SetSyncObject = syncObject;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        DataGridViewButtonCell cmbColumnTo = (DataGridViewButtonCell)this.dgvTable.Rows[e.RowIndex].Cells["SetColumn"];
                        cmbColumnTo.Value = "已设置";
                        cmbColumnTo.Tag = form.SetSyncTableColumns;

                        DataGridViewImageCell btnSelectColumn = (DataGridViewImageCell)this.dgvTable.Rows[e.RowIndex].Cells["SelectColumn"];
                        btnSelectColumn.Value = Resources.btnChecked;
                    }
                }

                if (e.ColumnIndex == 5)
                {
                    DataGridViewButtonCell cmbColumnTo = (DataGridViewButtonCell)this.dgvTable.Rows[e.RowIndex].Cells["SetColumn"];
                    cmbColumnTo.Value = "设置";
                    cmbColumnTo.Tag = null;

                    DataGridViewImageCell btnSelectColumn = (DataGridViewImageCell)this.dgvTable.Rows[e.RowIndex].Cells["SelectColumn"];
                    btnSelectColumn.Value = Resources.btnUnChecked;
                }
            }
        }

        private void dgvTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            throw e.Exception;
        }

        private void chkSelected_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.chkSelected.Checked;
            if (flag)
            {
                this.chkSelected.Text = "取消选择";

                for (int i = 0; i < this.dgvTable.Rows.Count; i++)
                {
                    string fromTableName = this.dgvTable.Rows[i].Cells["ColumnFrom"].Value.ToString();
                    if (!string.IsNullOrEmpty(fromTableName))
                    {
                        foreach (var syncObject in SyncObjects.Values)
                        {
                            if (syncObject.To != null)
                            {
                                if (fromTableName.Equals(syncObject.To.Table.Name, StringComparison.OrdinalIgnoreCase))
                                {
                                    this.dgvTable.Rows[i].Cells["ColumnTo"].Value = syncObject.To.Table.Name;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.chkSelected.Text = "自动选择";

                for (int i = 0; i < this.dgvTable.Rows.Count; i++)
                {
                    this.dgvTable.Rows[i].Cells["ColumnTo"].Value = "";
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            List<SyncInfo> syncInfo = new List<SyncInfo>();
            if (this.dgvTable.Rows.Count > 0)
            {
                string fromTableName;
                string toTableName;
                Dictionary<string, string> setColumns;
                foreach (DataGridViewRow row in this.dgvTable.Rows)
                {
                    if (row.Cells["SetColumn"].Tag != null)
                    {
                        fromTableName = row.Cells["ColumnFrom"].Value.ToString();
                        toTableName = row.Cells["ColumnTo"].Value.ToString();
                        setColumns = row.Cells["SetColumn"].Tag as Dictionary<string, string>;

                        syncInfo.Add(new SyncInfo()
                        {
                            FromTableName = fromTableName,
                            ToTableName = toTableName,
                            SetColumns = setColumns
                        });
                    }
                }
            }



        }
    }
}
