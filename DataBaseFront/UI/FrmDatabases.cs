using DataBaseFront.DB.DbParams;
using DataBaseFront.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DataBaseFront.UI
{
    public partial class FrmDatabases : BaseForm
    {
        const string C_Database = "DataBaseFront.Entity.Link";
        const string C_Databases = "DATABASES";
        const string C_Tables = "TABLES";
        const string C_Views = "VIEWS";
        const string C_Procdures = "PROCDURES";
        const string C_Columns = "COLUMNS";

        Dictionary<string, bool> HasLoadTables;
        Dictionary<string, Dictionary<string, bool>> HasLoadTableColumns;
        Dictionary<string, bool> HasLoadViews;
        Dictionary<string, bool> HasLoadProcdures;

        ContextMenuStrip cmsMenuForDataBase,
                         cmsMenuForTable,
                         cmsMenuForColumn,
                         cmsMenuForView,
                         cmsMenuForProcedure;

        TreeNode theLastNode = null;//最后选择的节点（用于还原节点状态）

        public FrmDatabases()
        {
            InitializeComponent();

            HasLoadTables = new Dictionary<string, bool>();
            HasLoadTableColumns = new Dictionary<string, Dictionary<string, bool>>();
            HasLoadViews = new Dictionary<string, bool>();
            HasLoadProcdures = new Dictionary<string, bool>();

            cmsMenuForDataBase = new ContextMenuStrip();
            cmsMenuForTable = new ContextMenuStrip();
            cmsMenuForColumn = new ContextMenuStrip();
            cmsMenuForView = new ContextMenuStrip();
            cmsMenuForProcedure = new ContextMenuStrip();
        }

        private void FrmDatabases_Load(object sender, EventArgs e)
        {
            //加载图标
            this.LoadDBIcons();

            //加载数据库驱动
            this.LoadDBProviders();

            //初始化数据库列表
            this.LoadHistoryLinkRoot();

            //加载历史记录
            this.LoadHistoryLink();

            //加载右键菜单
            this.LoadHistoryContextMenu();
        }

        //新建连接
        private void tsbConnect_ButtonClick(object sender, EventArgs e)
        {
            //显示下拉列表
            this.tsbConnect.ShowDropDown();
        }

        //打开连接
        private void tsbConectDB_Click(object sender, EventArgs e)
        {
            //连接数据库操作
            var node = this.tvDB.SelectedNode;
            if (node != null && node.Tag is Link)
            {
                var currentLink = node.Tag as Link;

                //如果当前未打开
                if (!currentLink.HasOpen)
                {
                    //重新注册数据库
                    LinkUtil.Instance.RegisterLink(currentLink);

                    //尝试连接数据库
                    string connectError = string.Empty;
                    if (currentLink.DbOperate.TryConnect(out connectError))
                    {
                        //将当前数据库标识为打开
                        currentLink.HasOpen = true;

                        //将数据库的图标变亮
                        node.ImageKey = currentLink.DbParam.ConnectIcon;
                        node.SelectedImageKey = currentLink.DbParam.ConnectIcon;

                        //加载表/视图/存储过程的集合节点
                        this.LoadParentNodes(node);
                    }
                    else
                    {
                        //取消注册数据库
                        LinkUtil.Instance.UnRegisterLink(currentLink);

                        //将当前数据库标识为关闭
                        currentLink.HasOpen = false;

                        //将数据库的图标变灰
                        node.ImageKey = currentLink.DbParam.UnConnectIcon;
                        node.SelectedImageKey = currentLink.DbParam.UnConnectIcon;

                        MessageUtil.ShowWarning(connectError);
                        return;
                    }

                    //设置按钮状态
                    this.tsbConectDB.Enabled = false;
                    this.tsbDisConectDB.Enabled = true;
                    this.tsbRefresh.Enabled = true;
                    this.tsbProperty.Enabled = true;
                    this.tsbRemove.Enabled = true;
                }
            }
        }

        //关闭连接
        private void tsbDisConectDB_Click(object sender, EventArgs e)
        {
            //断开连接数据库操作
            var node = this.tvDB.SelectedNode;
            if (node != null && node.Tag is Link)
            {
                var currentLink = node.Tag as Link;
                if (currentLink.HasOpen)
                {
                    //取消注册数据库
                    LinkUtil.Instance.UnRegisterLink(currentLink);

                    //将数据库的图标变灰
                    node.ImageKey = currentLink.DbParam.UnConnectIcon;
                    node.SelectedImageKey = currentLink.DbParam.UnConnectIcon;

                    //移除数据库下面的全部节点
                    node.Nodes.Clear();

                    //清空加载缓存
                    this.HasLoadTables.Remove(DATABASE.AliasName);
                    this.HasLoadTableColumns.Remove(DATABASE.AliasName);
                    this.HasLoadViews.Remove(DATABASE.AliasName);
                    this.HasLoadProcdures.Remove(DATABASE.AliasName);

                    //设置按钮状态
                    this.tsbConectDB.Enabled = true;
                    this.tsbDisConectDB.Enabled = false;
                    this.tsbRefresh.Enabled = false;
                    this.tsbProperty.Enabled = true;
                    this.tsbRemove.Enabled = true;
                }
            }
        }

        //刷新
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            var node = this.tvDB.SelectedNode;
            if (node != null)
            {
                bool hasRemoveFlag = false;

                string selectTag = node.Tag.ToString();
                switch (selectTag)
                {
                    case C_Database:
                        {
                            //删除标识
                            hasRemoveFlag = true;
                            //清空加载缓存（表、列、视图、存储过程）
                            this.HasLoadTables.Remove(DATABASE.AliasName);
                            this.HasLoadTableColumns.Remove(DATABASE.AliasName);
                            this.HasLoadViews.Remove(DATABASE.AliasName);
                            this.HasLoadProcdures.Remove(DATABASE.AliasName);
                        }
                        break;
                    case C_Tables:
                        {
                            //删除标识
                            hasRemoveFlag = true;
                            //清空加载缓存（表、列）
                            this.HasLoadTables.Remove(DATABASE.AliasName);
                            this.HasLoadTableColumns.Remove(DATABASE.AliasName);
                        }
                        break;
                    case C_Columns:
                        {
                            //删除标识
                            hasRemoveFlag = true;
                            //清空加载缓存（列）
                            this.HasLoadTableColumns[DATABASE.AliasName].Remove(node.Parent.Text);
                        }
                        break;
                    case C_Views:
                        {
                            //删除标识
                            hasRemoveFlag = true;
                            //清空加载缓存（视图）
                            this.HasLoadViews.Remove(DATABASE.AliasName);
                        }
                        break;
                    case C_Procdures:
                        {
                            //删除标识
                            hasRemoveFlag = true;
                            //清空加载缓存（存储过程）
                            this.HasLoadProcdures.Remove(DATABASE.AliasName);
                        }
                        break;
                }

                //如果有删除，就收缩该节点，以便展开加载该节点的子集
                if (hasRemoveFlag)
                {
                    node.Collapse();
                }
            }
        }

        //属性
        private void tsbProperty_Click(object sender, EventArgs e)
        {
            var node = this.tvDB.SelectedNode;
            if (node != null && node.Tag is Link)
            {
                var currentLink = node.Tag as Link;

                //修改当前连接
                FrmConnect frmConnect = new FrmConnect(currentLink.DbParam.DbProvider);
                frmConnect.P_AliasName = currentLink.AliasName;
                frmConnect.P_DbParam = currentLink.DbParam;
                frmConnect.IsModifyProperty = true;
                frmConnect.SetHistoryLink();
                if (frmConnect.ShowDialog() == DialogResult.OK)
                {
                    var link = new Link()
                    {
                        AliasName = frmConnect.P_AliasName,
                        DbParam = frmConnect.P_DbParam,
                        DbOperate = frmConnect.P_DbOperate,
                        HasOpen = false
                    };

                    //重新注册数据库
                    LinkUtil.Instance.ReRegisterLink(link);

                    //添加到树形
                    var tvDatabase = new TreeNode()
                    {
                        Text = string.Format("{0}", frmConnect.P_AliasName),
                        ToolTipText = string.Format("数据库：{0}\n类  型：{1}", frmConnect.P_DbParam.DbName, frmConnect.P_DbParam.DbProvider),
                        Tag = link,
                        ImageKey = frmConnect.P_DbParam.UnConnectIcon,
                        SelectedImageKey = frmConnect.P_DbParam.UnConnectIcon
                    };

                    TreeNode tvRoot = this.tvDB.Nodes[0];
                    foreach (TreeNode item in tvRoot.Nodes)
                    {
                        if (item.Text == frmConnect.P_AliasName)
                        {
                            tvRoot.Nodes.Remove(item);
                            break;
                        }
                    }
                    tvRoot.Nodes.Add(tvDatabase);
                    tvRoot = null;

                    //清空加载缓存
                    this.HasLoadTables.Remove(link.AliasName);
                    this.HasLoadTableColumns.Remove(link.AliasName);
                    this.HasLoadViews.Remove(link.AliasName);
                    this.HasLoadProcdures.Remove(link.AliasName);
                }
            }
        }

        //删除连接
        private void tsbRemove_Click(object sender, EventArgs e)
        {
            var node = this.tvDB.SelectedNode;
            if (node != null && node.Tag is Link)
            {
                if (MessageUtil.ConfirmYesNo("确定删除当前这个连接？"))
                {
                    var currentLink = node.Tag as Link;

                    //从树形中移除该连接
                    TreeNode tvRoot = this.tvDB.Nodes[0];
                    foreach (TreeNode item in tvRoot.Nodes)
                    {
                        if (item.Text == currentLink.AliasName)
                        {
                            tvRoot.Nodes.Remove(item);
                            break;
                        }
                    }

                    //取消注册数据库
                    LinkUtil.Instance.UnRegisterLink(currentLink);

                    //从历史记录中移除连接
                    LinkUtil.Instance.RemoveLink(currentLink.AliasName);

                    //清空加载缓存
                    this.HasLoadTables.Remove(currentLink.AliasName);
                    this.HasLoadTableColumns.Remove(currentLink.AliasName);
                    this.HasLoadViews.Remove(currentLink.AliasName);
                    this.HasLoadProcdures.Remove(currentLink.AliasName);
                }
            }
        }

        //选中节点前
        private void tvDB_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ForeColor = Color.Blue;
            e.Node.NodeFont = new Font("宋体", 9, FontStyle.Underline);
            if (theLastNode != null)
            {
                theLastNode.ForeColor = SystemColors.WindowText;
                theLastNode.NodeFont = new Font("宋体", 9, FontStyle.Regular);
            }
        }

        //选中节点后
        private void tvDB_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvDB.SelectedNode != null)
            {
                theLastNode = tvDB.SelectedNode;
            }
        }

        //展开树形节点前，异步加载子节点
        private void tvDB_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
                return;

            //如果当前选中节点是以下节点，就打开线程加载子节点
            string selectTag = e.Node.Tag.ToString();
            switch (selectTag)
            {
                case C_Tables:
                case C_Views:
                case C_Procdures:
                case C_Columns:
                    {
                        this.Cursor = Cursors.WaitCursor;

                        Thread t = new Thread(new ParameterizedThreadStart(this.LoadChildNodes));
                        t.IsBackground = true;
                        t.Start(new { TAG = selectTag, NODE = e.Node });

                        this.Cursor = Cursors.Default;
                    }
                    break;
            }
        }

        //单击树形
        private void tvDB_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode node = this.tvDB.GetNodeAt(e.Location);

            //左键单击
            if (e.Button == MouseButtons.Left)
            {
                if (node != null && node.Tag != null)
                {
                    #region 设置工具栏属性
                    this.tsslLabel.Text = "选择:" + node.Text;

                    string selectTag = node.Tag.ToString();
                    switch (selectTag)
                    {
                        case C_Database:
                            {
                                var currentLink = node.Tag as Link;
                                if (currentLink.HasOpen)
                                {
                                    //设置按钮状态
                                    this.tsbConectDB.Enabled = false;
                                    this.tsbDisConectDB.Enabled = true;
                                    this.tsbRefresh.Enabled = true;
                                    this.tsbProperty.Enabled = true;
                                    this.tsbRemove.Enabled = true;
                                }
                                else
                                {
                                    //设置按钮状态
                                    this.tsbConectDB.Enabled = true;
                                    this.tsbDisConectDB.Enabled = false;
                                    this.tsbRefresh.Enabled = false;
                                    this.tsbProperty.Enabled = true;
                                    this.tsbRemove.Enabled = true;
                                }
                            }
                            break;
                        case C_Tables:
                            {
                                this.tsbConectDB.Enabled = false;
                                this.tsbDisConectDB.Enabled = false;
                                this.tsbRefresh.Enabled = true;
                                this.tsbProperty.Enabled = false;
                                this.tsbRemove.Enabled = false;
                            }
                            break;
                        case C_Columns:
                            {
                                this.tsbConectDB.Enabled = false;
                                this.tsbDisConectDB.Enabled = false;
                                this.tsbRefresh.Enabled = true;
                                this.tsbProperty.Enabled = false;
                                this.tsbRemove.Enabled = false;
                            }
                            break;
                        case C_Views:
                            {
                                this.tsbConectDB.Enabled = false;
                                this.tsbDisConectDB.Enabled = false;
                                this.tsbRefresh.Enabled = true;
                                this.tsbProperty.Enabled = false;
                                this.tsbRemove.Enabled = false;
                            }
                            break;
                        case C_Procdures:
                            {
                                this.tsbConectDB.Enabled = false;
                                this.tsbDisConectDB.Enabled = false;
                                this.tsbRefresh.Enabled = true;
                                this.tsbProperty.Enabled = false;
                                this.tsbRemove.Enabled = false;
                            }
                            break;
                        default:
                            {
                                this.tsbConectDB.Enabled = false;
                                this.tsbDisConectDB.Enabled = false;
                                this.tsbRefresh.Enabled = false;
                                this.tsbProperty.Enabled = false;
                                this.tsbRemove.Enabled = false;
                            }
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region 设置工具栏属性
                    this.tsbConectDB.Enabled = false;
                    this.tsbDisConectDB.Enabled = false;
                    this.tsbRefresh.Enabled = false;
                    this.tsbProperty.Enabled = false;
                    this.tsbRemove.Enabled = false;
                    #endregion
                }
            }

            //右键单击，显示菜单
            if (e.Button == MouseButtons.Right)
            {
                if (node != null && node.Tag != null)
                {
                    this.tvDB.SelectedNode = node;

                    //根据选中节点不同，显示不同的右键菜单
                    object selectTag = node.Tag;
                    if (selectTag is Link)
                        node.ContextMenuStrip = this.cmsMenuForDataBase;
                    else if (selectTag is MGTable)
                        node.ContextMenuStrip = this.cmsMenuForTable;
                    else if (selectTag is MGColumn)
                        node.ContextMenuStrip = this.cmsMenuForColumn;
                    else if (selectTag is MGView)
                        node.ContextMenuStrip = this.cmsMenuForView;
                    else if (selectTag is MGProcedure)
                        node.ContextMenuStrip = this.cmsMenuForProcedure;
                }
            }
        }

        //单击菜单事件
        private void cmsDBMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectMenu = sender as ToolStripMenuItem;
            switch (selectMenu.Name)
            {
                #region 数据库
                case "cmsForDataBase_sync":                 //同步数据
                    {
                        FrmMain.MAIN.ShowSync();
                    }
                    break;
                #endregion
                #region 表
                case "cmsForTable_open_table_all":          //打开表（显示所有）
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowData(DATABASE, tableName, 0);
                    }
                    break;
                case "cmsForTable_open_table_top_100":  //打开表（显示前100条）
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowData(DATABASE, tableName, 100);
                    }
                    break;
                case "cmsForTable_open_table_top_1000": //打开表（显示前1000条）
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowData(DATABASE, tableName, 1000);
                    }
                    break;
                case "cmsForTable_design_table":   //设计表
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowDesignTable(DATABASE, tableName);
                    }
                    break;
                case "cmsForTable_open_table_schema":   //打开表结构
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSchema(DATABASE, tableName);
                    }
                    break;
                case "cmsForTable_table_select_sql":          //SELECT
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildSelectTableSql(tableName));
                    }
                    break;
                case "cmsForTable_table_create_sql":          //CREATE
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildCreateTableSql(tableName));
                    }
                    break;
                case "cmsForTable_table_drop_sql":          //DROP
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildDropTableSql(tableName));
                    }
                    break;
                case "cmsForTable_table_drop_create_sql":          //DROP CREATE
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        string dropTableSql = DATABASE.DbOperate.BuildDropTableSql(tableName);
                        string createTableSql = DATABASE.DbOperate.BuildCreateTableSql(tableName);
                        FrmMain.MAIN.ShowSql(DATABASE, string.Format("{0}\nGO\n{1}", dropTableSql, createTableSql));
                    }
                    break;
                case "cmsForTable_table_insert_sql":          //INSERT
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildInsertSql(tableName));
                    }
                    break;
                case "cmsForTable_table_update_sql":          //UPDATE
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildUpdateSql(tableName));
                    }
                    break;
                case "cmsForTable_table_delete_sql":          //DELTE
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildDeleteSql(tableName));
                    }
                    break;
                case "cmsForTable_table_insert_sql_param":          //INSERT PARAM
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildInsertSqlWidthParam(tableName));
                    }
                    break;
                case "cmsForTable_table_update_sql_param":          //UPDATE PARAM
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildUpdateSqlWidthParam(tableName));
                    }
                    break;
                case "cmsForTable_table_delete_sql_param":          //DELETE PARAM
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildDeleteSqlWidthParam(tableName));
                    }
                    break;
                case "cmsForTable_copy_table_name":     //复制表名
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        string sql = "【" + tableName + "】";
                        DATABASE.DbOperate.FilterKeyWord(ref sql);
                        FormUtil.CopyData(sql);
                    }
                    break;
                case "cmsForTable_build_database_table_doc":     //生成数据库文档
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowBuildDoc(DATABASE, tableName);
                    }
                    break;
                case "cmsForTable_build_table_code":        //模板代码生成
                    {
                        string tableName = ((MGTable)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowBuild(DATABASE, tableName);
                    }
                    break;
                #endregion

                #region 字段
                case "cmsForColumn_copy_column_name":   //复制字段名名称
                    {
                        string columnName = ((MGColumn)this.tvDB.SelectedNode.Tag).Name;
                        string sql = "【" + columnName + "】";
                        DATABASE.DbOperate.FilterKeyWord(ref sql);
                        FormUtil.CopyData(sql);
                    }
                    break;
                #endregion

                #region 视图
                case "cmsForView_view_create_schema":   //CREATE
                    {
                        string viewName = ((MGView)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildCreateViewSql(viewName));
                    }
                    break;
                case "cmsForView_view_drop_schema":   //DROP
                    {
                        string viewName = ((MGView)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildDropViewSql(viewName));
                    }
                    break;
                case "cmsForView_view_select_schema":   //SELECT
                    {
                        string viewName = ((MGView)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, string.Format("SELECT * FROM [{0}]", viewName));
                    }
                    break;
                case "cmsForView_copy_view_name":   //复制视图名称
                    {
                        string viewName = ((MGView)this.tvDB.SelectedNode.Tag).Name;
                        string sql = "【" + viewName + "】";
                        DATABASE.DbOperate.FilterKeyWord(ref sql);
                        FormUtil.CopyData(sql);
                    }
                    break;
                #endregion

                #region 存储过程
                case "cmsForProcdure_procdure_create_schema":   //CREATE
                    {
                        string procdureName = ((MGProcedure)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildCreateProcedureSql(procdureName));
                    }
                    break;
                case "cmsForProcdure_procdure_drop_schema":   //DROP 
                    {
                        string procdureName = ((MGProcedure)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, DATABASE.DbOperate.BuildDropProcedureSql(procdureName));
                    }
                    break;
                case "cmsForProcdure_procdure_exec_schema":   //EXEC
                    {
                        string procdureName = ((MGProcedure)this.tvDB.SelectedNode.Tag).Name;
                        FrmMain.MAIN.ShowSql(DATABASE, string.Format("EXEC [{0}]", procdureName));
                    }
                    break;
                case "cmsForProcdure_copy_procdure_name":   //复制视存储过程名称
                    {
                        string procdureName = ((MGProcedure)this.tvDB.SelectedNode.Tag).Name;
                        string sql = "【" + procdureName + "】";
                        DATABASE.DbOperate.FilterKeyWord(ref sql);
                        FormUtil.CopyData(sql);
                    }
                    break;
                #endregion

                default:
                    MessageUtil.ShowWarning(selectMenu.Name + "未实现");
                    break;
            }
        }

        //单击数据库驱动事件
        private void cmsDBProviderItem_Click(object sender, EventArgs e)
        {
            //新建连接
            FrmConnect frmConnect;
            if (sender is ToolStripMenuItem)
            {
                DbProvider dbProvider = (DbProvider)(Convert.ToInt32(((ToolStripMenuItem)sender).Tag));
                frmConnect = new FrmConnect(dbProvider);
            }
            else
            {
                frmConnect = new FrmConnect();
            }

            if (frmConnect.ShowDialog() == DialogResult.OK)
            {
                var link = new Link()
                {
                    AliasName = frmConnect.P_AliasName,
                    DbParam = frmConnect.P_DbParam,
                    DbOperate = frmConnect.P_DbOperate,
                    HasOpen = false
                };

                var tvDatabase = new TreeNode()
                {
                    Text = string.Format("{0}", frmConnect.P_AliasName),
                    ToolTipText = string.Format("数据库：{0}\n类  型：{1}", frmConnect.P_DbParam.DbName, frmConnect.P_DbParam.DbProvider),
                    Tag = link,
                    ImageKey = frmConnect.P_DbParam.UnConnectIcon,
                    SelectedImageKey = frmConnect.P_DbParam.UnConnectIcon
                };

                TreeNode tvRoot = this.tvDB.Nodes[0];
                tvRoot.Nodes.Add(tvDatabase);
                tvRoot = null;
            }
            frmConnect = null;
        }

        /// <summary>
        /// 加载图标
        /// </summary>
        private void LoadDBIcons()
        {
            this.imgFortvDB.Images.Add("folder", Properties.Resources.folder);
            this.imgFortvDB.Images.Add("databases", Properties.Resources.databases);
            this.imgFortvDB.Images.Add("table", Properties.Resources.table);
            this.imgFortvDB.Images.Add("view", Properties.Resources.view);
            this.imgFortvDB.Images.Add("procedure", Properties.Resources.procedure);
            this.imgFortvDB.Images.Add("column", Properties.Resources.column);
            this.imgFortvDB.Images.Add("primarykey", Properties.Resources.primarykey);
            this.imgFortvDB.Images.Add("database", Properties.Resources.database);
            this.imgFortvDB.Images.Add("database_un", Properties.Resources.database_un);
            this.imgFortvDB.Images.Add("sqlserver2005", Properties.Resources.sqlserver2005);
            this.imgFortvDB.Images.Add("sqlserver2005_un", Properties.Resources.sqlserver2005_un);
            this.imgFortvDB.Images.Add("mysql", Properties.Resources.mysql);
            this.imgFortvDB.Images.Add("mysql_un", Properties.Resources.mysql_un);
            this.imgFortvDB.Images.Add("sqlite", Properties.Resources.sqlite);
            this.imgFortvDB.Images.Add("sqlite_un", Properties.Resources.sqlite_un);
            this.imgFortvDB.Images.Add("access2007", Properties.Resources.access2007);
            this.imgFortvDB.Images.Add("access2007_un", Properties.Resources.access2007_un);
        }

        /// <summary>
        /// 加载数据库驱动
        /// </summary>
        private void LoadDBProviders()
        {
            var dbProviders = typeof(DbProvider).GetItems();
            if (dbProviders != null && dbProviders.Count > 0)
            {
                ToolStripMenuItem toolStripItem;

                //动态加载不同数据库驱动
                foreach (dynamic item in dbProviders)
                {
                    toolStripItem = new ToolStripMenuItem();
                    toolStripItem.Text = item.Text;
                    toolStripItem.Name = "tsmi_db_provider_" + item.Value;
                    toolStripItem.Tag = item.Value;
                    toolStripItem.Click += cmsDBProviderItem_Click;
                    tsbConnect.DropDownItems.Add(toolStripItem);
                }
                toolStripItem = null;
            }
        }

        /// <summary>
        /// 初始化数据库列表
        /// </summary>
        private void LoadHistoryLinkRoot()
        {
            this.tvDB.Nodes.Clear();

            TreeNode tvRoot = new TreeNode()
            {
                Text = "数据库列表",
                Tag = C_Databases,
                ImageKey = "databases",
                SelectedImageKey = "databases",
            };

            this.tvDB.Nodes.Add(tvRoot);
        }

        /// <summary>
        /// 加载历史记录
        /// </summary>
        private void LoadHistoryLink()
        {
            TreeNode tvRoot = this.tvDB.Nodes[0];
            tvRoot.Nodes.Clear();

            var historyLinks = LinkUtil.Instance.GetLinks();
            if (historyLinks != null && historyLinks.Count > 0)
            {
                KeyValuePair<string, IDbParam> historyLink;
                Link link;
                TreeNode tvDatabase;
                for (int i = 0; i < historyLinks.Count; i++)
                {
                    historyLink = historyLinks.ElementAt(i);

                    link = new Link()
                    {
                        AliasName = historyLink.Key,
                        DbParam = historyLink.Value,
                        DbOperate = null,
                        HasOpen = false
                    };

                    tvDatabase = new TreeNode()
                    {
                        Text = string.Format("{0}({1})", link.AliasName, link.DbParam.DbName),
                        ToolTipText = string.Format("数据库：{0}\n类  型：{1}", link.DbParam.DbName, link.DbParam.DbProvider),
                        Tag = link,
                        ImageKey = link.DbParam.UnConnectIcon,
                        SelectedImageKey = link.DbParam.UnConnectIcon
                    };
                    tvRoot.Nodes.Add(tvDatabase);
                }
                tvDatabase = null;
                link = null;
                historyLinks = null;
            }

            tvRoot.Expand();

            tvRoot = null;
        }

        /// <summary>
        /// 加载右键菜单
        /// </summary>
        private void LoadHistoryContextMenu()
        {
            ToolStripMenuItem toolStripItem,       //定义菜单项
                               toolStripItem2;      //定义菜单项2
            ToolStripSeparator toolStripSeparator; //定义菜单分隔符

            #region 数据库
            //同步数据
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "同步数据";
            toolStripItem.Name = "cmsForDataBase_sync";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForDataBase.Items.Add(toolStripItem);

            //生成结构
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "生成结构";
            toolStripItem.Name = "cmsForDataBase_build_frame";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForDataBase.Items.Add(toolStripItem);
            #endregion

            #region 表
            //打开表
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "打开表";
            toolStripItem.Name = "cmsForTable_open_table";

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "显示前100条记录";
            toolStripItem2.Name = "cmsForTable_open_table_top_100";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "显示前1000条记录";
            toolStripItem2.Name = "cmsForTable_open_table_top_1000";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "显示所有记录";
            toolStripItem2.Name = "cmsForTable_open_table_all";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            this.cmsMenuForTable.Items.Add(toolStripItem);

            //设计表
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "设计表";
            toolStripItem.Name = "cmsForTable_design_table";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForTable.Items.Add(toolStripItem);

            //查看表结构
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "查看表结构";
            toolStripItem.Name = "cmsForTable_open_table_schema";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForTable.Items.Add(toolStripItem);

            //编辑表脚本为
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "编辑表脚本为";

            //SELECT
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "SELECT";
            toolStripItem2.Name = "cmsForTable_table_select_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            toolStripItem.DropDownItems.Add(toolStripSeparator);

            //CREATE
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "CREATE";
            toolStripItem2.Name = "cmsForTable_table_create_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //DROP
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "DROP";
            toolStripItem2.Name = "cmsForTable_table_drop_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //DROP 和 CREATE
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "DROP 和 CREATE";
            toolStripItem2.Name = "cmsForTable_table_drop_create_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            toolStripItem.DropDownItems.Add(toolStripSeparator);

            //INSERT
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "INSERT";
            toolStripItem2.Name = "cmsForTable_table_insert_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //UPDATE
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "UPDATE";
            toolStripItem2.Name = "cmsForTable_table_update_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //DELETE
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "DELETE";
            toolStripItem2.Name = "cmsForTable_table_delete_sql";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            toolStripItem.DropDownItems.Add(toolStripSeparator);

            //INSERT
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "INSERT(@)";
            toolStripItem2.Name = "cmsForTable_table_insert_sql_param";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //UPDATE
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "UPDATE(@)";
            toolStripItem2.Name = "cmsForTable_table_update_sql_param";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //DELETE
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "DELETE(@)";
            toolStripItem2.Name = "cmsForTable_table_delete_sql_param";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            this.cmsMenuForTable.Items.Add(toolStripItem);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            this.cmsMenuForTable.Items.Add(toolStripSeparator);

            //复制表名
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "复制表名";
            toolStripItem.Name = "cmsForTable_copy_table_name";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForTable.Items.Add(toolStripItem);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            this.cmsMenuForTable.Items.Add(toolStripSeparator);

            //生成数据库文档
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "生成数据库文档";
            toolStripItem.Name = "cmsForTable_build_database_table_doc";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForTable.Items.Add(toolStripItem);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            this.cmsMenuForTable.Items.Add(toolStripSeparator);

            //模板代码生成
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "模板代码生成";
            toolStripItem.Name = "cmsForTable_build_table_code";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForTable.Items.Add(toolStripItem);
            #endregion

            #region 字段
            //复制字段名称
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "复制字段名称";
            toolStripItem.Name = "cmsForColumn_copy_column_name";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForColumn.Items.Add(toolStripItem);
            #endregion

            #region 视图
            //生成SQL语句
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "生成SQL语句";

            //查看视图定义
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "CREATE";
            toolStripItem2.Name = "cmsForView_view_create_schema";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "DROP";
            toolStripItem2.Name = "cmsForView_view_drop_schema";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "SELECT";
            toolStripItem2.Name = "cmsForView_view_select_schema";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            toolStripItem.DropDownItems.Add(toolStripSeparator);

            this.cmsMenuForView.Items.Add(toolStripItem);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            this.cmsMenuForView.Items.Add(toolStripSeparator);

            //复制视图名称
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "复制视图名称";
            toolStripItem.Name = "cmsForView_copy_view_name";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForView.Items.Add(toolStripItem);
            #endregion

            #region 存储过程
            //生成SQL语句
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "生成SQL语句";

            //查看存储过程定义
            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "CREATE";
            toolStripItem2.Name = "cmsForProcdure_procdure_create_schema";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "DROP";
            toolStripItem2.Name = "cmsForProcdure_procdure_drop_schema";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            toolStripItem.DropDownItems.Add(toolStripSeparator);

            toolStripItem2 = new ToolStripMenuItem();
            toolStripItem2.Text = "EXEC";
            toolStripItem2.Name = "cmsForProcdure_procdure_exec_schema";
            toolStripItem2.Click += new EventHandler(cmsDBMenuItem_Click);
            toolStripItem.DropDownItems.Add(toolStripItem2);

            this.cmsMenuForProcedure.Items.Add(toolStripItem);

            //间隔符 -
            toolStripSeparator = new ToolStripSeparator();
            this.cmsMenuForProcedure.Items.Add(toolStripSeparator);

            //复制存储过程名称
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "复制存储过程名称";
            toolStripItem.Name = "cmsForProcdure_copy_procdure_name";
            toolStripItem.Click += new EventHandler(cmsDBMenuItem_Click);
            this.cmsMenuForProcedure.Items.Add(toolStripItem);
            #endregion
        }

        /// <summary>
        /// 加载表/视图/存储过程的父节点
        /// </summary>
        private void LoadParentNodes(TreeNode tvRoot)
        {
            tvRoot.Nodes.Clear();

            TreeNode tvTables, tvViews, tvProcedures;

            tvTables = new TreeNode() { Text = "表", Tag = C_Tables, ImageKey = "folder", SelectedImageKey = "folder" };
            tvTables.Nodes.Add(new TreeNode("正在加载..."));
            tvRoot.Nodes.Add(tvTables);

            tvViews = new TreeNode() { Text = "视图", Tag = C_Views, ImageKey = "folder", SelectedImageKey = "folder" };
            tvViews.Nodes.Add(new TreeNode("正在加载..."));
            tvRoot.Nodes.Add(tvViews);

            tvProcedures = new TreeNode() { Text = "存储过程", Tag = C_Procdures, ImageKey = "folder", SelectedImageKey = "folder" };
            tvProcedures.Nodes.Add(new TreeNode("正在加载..."));
            tvRoot.Nodes.Add(tvProcedures);

            tvRoot.Expand();

            tvTables = null;
            tvViews = null;
            tvProcedures = null;
            tvRoot = null;
        }

        /// <summary>
        /// 异步加载子节点
        /// </summary>
        private void LoadChildNodes(dynamic parentNodeInfo)
        {
            this.tvDB.InvokeIfNeeded((value) =>
            {
                string tag = parentNodeInfo.TAG;
                TreeNode node = parentNodeInfo.NODE;

                this.tvDB.BeginUpdate();
                switch (tag)
                {
                    case C_Tables:
                        this.LoadTables(node);
                        break;
                    case C_Views:
                        this.LoadViews(node);
                        break;
                    case C_Procdures:
                        this.LoadProcedures(node);
                        break;
                    case C_Columns:
                        this.LoadColumns(node);
                        break;
                }
                this.tvDB.EndUpdate();
            }, string.Empty);
        }

        /// <summary>
        /// 加载表
        /// </summary>
        private void LoadTables(TreeNode tvTables)
        {
            if (this.HasLoadTables.ContainsKey(DATABASE.AliasName)
                && this.HasLoadTables[DATABASE.AliasName])
                return;

            tvTables.Nodes.Clear();
            TreeNode tvTable;
            TreeNode tvColumns;
            var tables = DATABASE.DbOperate.GetTables();
            if (tables != null && tables.Count > 0)
            {
                foreach (MGTable table in tables)
                {
                    //表
                    tvTable = new TreeNode()
                    {
                        Text = table.Name,
                        ToolTipText = table.Name,
                        ImageKey = "table",
                        SelectedImageKey = "table",
                        Tag = table
                    };

                    //列
                    tvColumns = new TreeNode()
                    {
                        Text = "列",
                        Tag = C_Columns
                    };

                    tvColumns.Nodes.Add(new TreeNode("正在加载..."));

                    tvTable.Nodes.Add(tvColumns);

                    tvTables.Nodes.Add(tvTable);
                }
                tables = null;
            }

            this.HasLoadTables.Add(DATABASE.AliasName, true);
        }

        /// <summary>
        /// 加载列
        /// </summary>
        private void LoadColumns(TreeNode tvColumns)
        {
            string tableName = tvColumns.Parent.Text;

            if (this.HasLoadTableColumns.ContainsKey(DATABASE.AliasName)
                && this.HasLoadTableColumns[DATABASE.AliasName].ContainsKey(tableName)
                && this.HasLoadTableColumns[DATABASE.AliasName][tableName])
                return;

            tvColumns.Nodes.Clear();
            TreeNode tvColumn;
            var columns = DATABASE.DbOperate.GetColumns(tableName);
            if (columns != null && columns.Count > 0)
            {
                foreach (MGColumn column in columns)
                {
                    tvColumn = new TreeNode();

                    tvColumn.Text = string.Format("{0}({1})", column.Name, column.DbType);
                    tvColumn.ToolTipText = string.Format("描述：{0}\n类型：{1}\n长度：{2}", column.Remark, column.DbType, column.Length);
                    if (column.IsPrimaryKey)
                    {
                        tvColumn.ImageKey = "primarykey";
                        tvColumn.SelectedImageKey = "primarykey";
                    }
                    else
                    {
                        tvColumn.ImageKey = "column";
                        tvColumn.SelectedImageKey = "column";
                    }
                    tvColumn.Tag = column;

                    tvColumns.Nodes.Add(tvColumn);
                }
                columns = null;
            }

            var loadTableColumns = new Dictionary<string, bool>();
            if (this.HasLoadTableColumns.ContainsKey(DATABASE.AliasName))
            {
                loadTableColumns = this.HasLoadTableColumns[DATABASE.AliasName];
                loadTableColumns.Add(tableName, true);
                this.HasLoadTableColumns[DATABASE.AliasName] = loadTableColumns;
            }
            else
            {
                loadTableColumns.Add(tableName, true);
                this.HasLoadTableColumns.Add(DATABASE.AliasName, loadTableColumns);
            }
        }

        /// <summary>
        /// 加载视图
        /// </summary>
        private void LoadViews(TreeNode tvViews)
        {
            if (this.HasLoadViews.ContainsKey(DATABASE.AliasName)
                && this.HasLoadViews[DATABASE.AliasName])
                return;

            tvViews.Nodes.Clear();
            TreeNode tvView;
            var views = DATABASE.DbOperate.GetViews();
            if (views != null && views.Count > 0)
            {
                foreach (MGView view in views)
                {
                    tvView = new TreeNode()
                    {
                        Text = view.Name,
                        ToolTipText = view.Name,
                        ImageKey = "view",
                        SelectedImageKey = "view",
                        Tag = view
                    };

                    tvViews.Nodes.Add(tvView);
                }
                views = null;
            }

            this.HasLoadViews.Add(DATABASE.AliasName, true);
        }

        /// <summary>
        /// 加载存储过程
        /// </summary>
        private void LoadProcedures(TreeNode tvProcedures)
        {
            if (this.HasLoadProcdures.ContainsKey(DATABASE.AliasName)
                && this.HasLoadProcdures[DATABASE.AliasName])
                return;

            tvProcedures.Nodes.Clear();
            TreeNode tvProcdure;
            var procedures = DATABASE.DbOperate.GetProcedures();
            if (procedures != null && procedures.Count > 0)
            {
                foreach (MGProcedure procdure in procedures)
                {
                    tvProcdure = new TreeNode()
                    {
                        Text = procdure.Name,
                        ToolTipText = procdure.Name,
                        ImageKey = "procedure",
                        SelectedImageKey = "procedure",
                        Tag = procdure
                    };

                    tvProcedures.Nodes.Add(tvProcdure);
                }
                procedures = null;
            }

            this.HasLoadProcdures.Add(DATABASE.AliasName, true);
        }

        /// <summary>
        /// 单击树形任一节点，获取当前操作数据库
        /// </summary>
        private Link DATABASE
        {
            get
            {
                var node = this.tvDB.SelectedNode;
                if (node == null)
                    return null;

                bool isDataBaseNode = false;

                if (node.Tag is Link)
                    isDataBaseNode = true;
                else
                {
                    while (!isDataBaseNode)
                    {
                        node = node.Parent;
                        if (node == null)
                            break;

                        if (node.Tag != null && node.Tag is Link)
                            isDataBaseNode = true;
                    }
                }

                return isDataBaseNode ? (Link)node.Tag : null;
            }
        }
    }
}
