using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseFront.Entity;
using DataBaseFront.Util;

namespace DataBaseFront.UI
{
    public partial class FrmBuild : BaseForm
    {
        Link targetLink;
        string targetTableName;

        public FrmBuild(Link link, string tableName)
        {
            InitializeComponent();

            targetLink = link;
            targetTableName = tableName;

            this.ucSelectColumns1.Init(targetLink, targetTableName);
        }

        private void FrmBuild_Load(object sender, EventArgs e)
        {
            //取得Web格式报表的模板
            this.cmbModelTemplates.DataSource = FormUtil.GetTemplateNames(AppInit.S_TemplateModelFolder);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (this.ucSelectColumns1.SelectColumns.Count == 0)
            {
                MessageUtil.ShowWarning("请选择字段");
                return;
            }

            VelocityHelper v = new VelocityHelper();
            v.Init(AppInit.S_TemplateModelFolder);

            //设置数据库名称
            v.Put("NameSpace", "MG");

            //设置左侧导航
            v.Put("TableName", targetTableName);

            //设置内容详细
            v.Put("ColumnInfos", this.ucSelectColumns1.SelectColumns);

            //获取选中的模板
            string template = this.cmbModelTemplates.SelectedItem.ToString();

            //获取最终生成的文档内容
            string webDocument = v.Display(template);

            //保存文档
            v.Save(targetTableName + ".cs", webDocument);
            v = null;
        }
    }
}
