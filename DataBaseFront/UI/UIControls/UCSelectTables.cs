using System;
using System.Windows.Forms;
using System.ComponentModel;
using DataBaseFront.DB.DbOperates;
using System.Collections.Generic;

namespace DataBaseFront.UI.UIControls
{
    public partial class UCSelectTables : UserControl
    {
        public UCSelectTables()
        {
            InitializeComponent();
        }

        public void Init(IDbOperate dbOperate, string targetTableName)
        {
            var tables = dbOperate.GetTables();

            //来源
            this.lbLeft.DisplayMember = "Name";
            this.lbLeft.ValueMember = "Name";
            this.lbLeft.Items.Clear();

            var targetTable = new MGTable();
            foreach (var table in tables)
            {
                if (string.IsNullOrEmpty(targetTableName))
                    this.lbLeft.Items.Add(table);
                else
                {
                    if (targetTableName.Equals(table.Name))
                    {
                        targetTable = table;
                    }
                    else
                    {
                        this.lbLeft.Items.Add(table);
                    }
                }
            }

            //目标
            this.lbRight.DisplayMember = "Name";
            this.lbRight.ValueMember = "Name";
            this.lbRight.Items.Clear();
            if (!string.IsNullOrEmpty(targetTableName))
            {
                this.lbRight.Items.Add(targetTable);
            }
        }

        private void lbLeft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lbLeft.SelectedItems.Count == 0) return;

            this.lbRight.Items.Add(this.lbLeft.SelectedItem);
            this.lbLeft.Items.Remove(this.lbLeft.SelectedItem);
        }

        private void lbRight_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lbRight.SelectedItems.Count == 0) return;

            this.lbLeft.Items.Add(this.lbRight.SelectedItem);
            this.lbRight.Items.Remove(this.lbRight.SelectedItem);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lbLeft.SelectedItems.Count == 0) return;

            this.lbRight.Items.Add(this.lbLeft.SelectedItem);
            this.lbLeft.Items.Remove(this.lbLeft.SelectedItem);
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            if (this.lbLeft.Items.Count == 0) return;

            foreach (var item in this.lbLeft.Items)
            {
                this.lbRight.Items.Add(item);
            }
            this.lbLeft.Items.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lbRight.SelectedItems.Count == 0) return;

            this.lbLeft.Items.Add(this.lbRight.SelectedItem);
            this.lbRight.Items.Remove(this.lbRight.SelectedItem);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (this.lbRight.Items.Count == 0) return;

            foreach (var item in this.lbRight.Items)
            {
                this.lbLeft.Items.Add(item);
            }
            this.lbRight.Items.Clear();
        }

        public List<MGTable> SelectTables
        {
            get
            {
                var targetTables = new List<MGTable>();
                foreach (var item in this.lbRight.Items)
                {
                    targetTables.Add((MGTable)item);
                }
                return targetTables;
            }
        }
    }
}
