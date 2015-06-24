using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using DataBaseFront.Entity;
using DataBaseFront.Util;

namespace DataBaseFront.UI
{
    public partial class FrmBuildDoc : BaseForm
    {
        Link targetLink;
        string targetTableName = string.Empty;

        public FrmBuildDoc(Link link, string tableName)
        {
            InitializeComponent();

            targetLink = link;
            targetTableName = tableName;
        }

        private void FrmBuildDoc_Load(object sender, EventArgs e)
        {
            //初始化选择表
            this.ucSelectTables1.Init(targetLink.DbOperate, targetTableName);

            //取得Web格式报表的模板
            this.cmbTemplates.DataSource = FormUtil.GetTemplateNames(AppInit.S_TemplateDocFolder);
        }

        private void btnBuildDoc_Click(object sender, EventArgs e)
        {
            var targetTables = this.ucSelectTables1.SelectTables;
            if (targetTables == null || targetTables.Count == 0)
            {
                MessageUtil.ShowWarning("请选择目标表");
                return;
            }

            SetBuildDocButtonEnable(false);

            new Thread(new ThreadStart(() =>
            {
                if (rbBuildFormatOfWord.Checked)
                {
                    BuildFormatWord(targetTables);
                }
                else if (rbBuildFormatOfWeb.Checked)
                {
                    BuildFormatWeb(targetTables);
                }
                else if (rbBuildFormatOfPDF.Checked)
                {
                    BuildFormatPdf(targetTables);
                }
            })).Start();
        }

        /// <summary>
        /// 生成Word文档格式
        /// </summary>
        private void BuildFormatWord(List<MGTable> targetTables)
        {
            try
            {
                var tableInfos = targetLink.DbOperate.GetTableInfos(targetTables);
                int tablesCount = tableInfos.Count;
                this.SetProgressValue(0, tablesCount);

                WordUtil word = new WordUtil();
                if (word.CreateWord() == false)
                {
                    MessageUtil.ShowError("文件创造失败");
                    return;
                }

                string[] tableHeader = new string[] { "序号", "字段名", "字段说明", "自动增长", "主键", "类型", "长度", "精度", "小数位", "允许空", "默认值" };

                DataTable storedt = new DataTable();
                foreach (string header in tableHeader)
                {
                    storedt.Columns.Add(header);
                }

                storedt.Rows.Add(tableHeader);

                word.OpenNewWordFileToEdit();

                word.SetPage(WordUtil.Orientation.横板, 18.4, 26, 3, 2.4, 1.87, 2.1);

                word.InsertText(targetLink.DbOperate.DbName, new Font("微软雅黑", 14, FontStyle.Bold), WordUtil.Alignment.居中, true);

                int progressStep = 0;
                foreach (var tableInfo in tableInfos)
                {
                    storedt.Rows.Clear();
                    storedt.Rows.Add(tableHeader);

                    //表名
                    word.InsertText(tableInfo.Table.Name, new Font("宋体", 12, FontStyle.Regular), WordUtil.Alignment.左对齐, false);

                    //字段列表
                    foreach (var columnInfo in tableInfo.Columns)
                    {
                        storedt.Rows.Add(columnInfo.Index
                                        , columnInfo.Name
                                        , columnInfo.Remark
                                        , columnInfo.AutoIncrement ? "是" : "否"
                                        , columnInfo.IsPrimaryKey ? "是" : "否"
                                        , columnInfo.DbType
                                        , columnInfo.Length
                                        , columnInfo.NumericPrecision
                                        , columnInfo.NumericScale
                                        , columnInfo.AllowNull ? "是" : "否"
                                        , columnInfo.DefaultValue);
                    }

                    word.InsertTable(storedt, true);
                    word.InsertText("", new Font("宋体", 12, FontStyle.Regular), WordUtil.Alignment.左对齐, false);

                    this.SetProgressValue(progressStep, tablesCount);

                    progressStep++;
                }

                this.SetProgressValue(tablesCount, tablesCount);

                word.InsertText(string.Format("生成时间：{0:yyyy年MM月dd日}", DateTime.Now), new Font("宋体", 12, FontStyle.Regular), WordUtil.Alignment.右对齐, false);
                word.InsertPageFooterNumber(new Font("宋体", 9, FontStyle.Bold), WordUtil.Alignment.右对齐);

                word.Save(Path.Combine(AppInit.S_TemplateBuildFolder, targetLink.DbOperate.DbName + ".doc"));
                word.CloseDocument();
                word.Quit();
                word = null;
                MessageUtil.ShowTips("数据库结构（Word格式）生成成功");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SetBuildDocButtonEnable(true);
            }
        }

        /// <summary>
        /// 生成Web文档格式
        /// </summary>
        private void BuildFormatWeb(IList<MGTable> targetTables)
        {
            try
            {
                this.SetProgressValue(0, 100);   //设置最大进度

                VelocityHelper v = new VelocityHelper();
                v.Init(AppInit.S_TemplateDocFolder);

                this.SetProgressValue(20, 100);   //设置进度

                //设置数据库名称
                v.Put("DataBaseName", targetLink.DbOperate.DbName);

                this.SetProgressValue(40, 100);   //设置进度

                //设置左侧导航
                v.Put("Tables", targetTables);

                this.SetProgressValue(60, 100);   //设置进度

                //设置内容详细
                v.Put("TableInfos", targetLink.DbOperate.GetTableInfos(targetTables));

                this.SetProgressValue(80, 100);   //设置进度

                //获取选中的模板
                string template = string.Empty;
                this.cmbTemplates.InvokeIfNeeded((value) =>
                {
                    template = this.cmbTemplates.SelectedItem.ToString();
                }, string.Empty);

                //获取最终生成的文档内容
                string webDocument = v.Display(template);

                //保存文档
                v.Save(targetLink.DbOperate.DbName + ".htm", webDocument);
                v = null;

                this.SetProgressValue(100, 100);   //设置进度
                MessageUtil.ShowTips("数据库结构（网页格式）生成成功");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SetBuildDocButtonEnable(true);
            }
        }

        /// <summary>
        /// 生成Pdf文档格式
        /// </summary>
        private void BuildFormatPdf(List<MGTable> targetTables)
        {
            try
            {
                MessageUtil.ShowWarning("未实现");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SetBuildDocButtonEnable(true);
            }
        }

        /// <summary>
        /// 设置进度条进度
        /// </summary>
        /// <param name="num">值</param>
        /// <param name="max">最大值</param>
        private void SetProgressValue(int num, int max)
        {
            this.pbBar.InvokeIfNeeded((value) =>
            {
                this.pbBar.Value = num;
                this.pbBar.Maximum = max;
            }, 0);

            this.lblProcess.InvokeIfNeeded((value) =>
            {
                this.lblProcess.Text = num + "/" + max;
            }, 0);
        }

        /// <summary>
        /// 设置按钮控件是否启用
        /// </summary>
        private void SetBuildDocButtonEnable(bool enable)
        {
            this.btnBuildDoc.InvokeIfNeeded((value) =>
            {
                this.btnBuildDoc.Enabled = value;
            }, enable);
        }

        /// <summary>
        /// 选择生成文档格式
        /// </summary>
        private void rbBuildFormat_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbTemplates.Enabled = this.rbBuildFormatOfWeb.Checked;
        }
    }
}
