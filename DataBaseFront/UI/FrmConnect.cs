using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using DataBaseFront.DB.DbParams;
using DataBaseFront.DB.DbOperates;

namespace DataBaseFront.UI
{
    public partial class FrmConnect : BaseForm
    {
        private DbProvider? DBProvider;

        /// <summary>
        /// 连接别名
        /// </summary>
        public string P_AliasName { get; set; }

        /// <summary>
        /// 连接参数
        /// </summary>
        public IDbParam P_DbParam { get; set; }

        /// <summary>
        /// 连接数据库对象
        /// </summary>
        public IDbOperate P_DbOperate { get; set; }

        /// <summary>
        /// 设置历史中访问的数据库名称
        /// </summary>
        public string HistoryDataBaseName { get; set; }

        /// <summary>
        /// 设置当前是否未修改操作
        /// </summary>
        public bool IsModifyProperty = false;

        public FrmConnect()
        {
            InitializeComponent();
        }

        public FrmConnect(DbProvider dbProvider)
        {
            InitializeComponent();

            this.DBProvider = dbProvider;
        }

        #region 窗体事件
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //加载数据库驱动
            this.LoadDBProviders();
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.Cancel
                    && this.DialogResult != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 选择服务器类型
        /// <summary>
        /// 选择数据库类型
        /// </summary>
        private void cmbDbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            //隐藏数据库列表
            this.pnlBottom.Visible = false;
            this.cmbDBName.Items.Clear();

            //获取选中数据库驱动
            dynamic selectDbProvider = this.cmbDbProvider.SelectedItem;
            DbProvider dbProvider = (DbProvider)Enum.Parse(typeof(DbProvider), selectDbProvider.Value.ToString());

            //显示连接到哪个驱动
            this.gbMain.Text = "连接到: " + selectDbProvider.Text;

            //显示数据库驱动面板
            this.pnlSelectDB.Controls.Clear();
            switch (dbProvider)
            {
                case DbProvider.SQL2005:
                    this.pnlSelectDB.Controls.Add(SelectDB.SelectSqlServer.Instance);
                    this.pnlSelectDB.Height = SelectDB.SelectSqlServer.Instance.Height;
                    break;
                case DbProvider.MySQL:
                    this.pnlSelectDB.Controls.Add(SelectDB.SelectMySql.Instance);
                    this.pnlSelectDB.Height = SelectDB.SelectMySql.Instance.Height;
                    break;
                case DbProvider.Sqlite:
                    this.pnlSelectDB.Controls.Add(SelectDB.SelectSqlite.Instance);
                    this.pnlSelectDB.Height = SelectDB.SelectSqlite.Instance.Height;
                    break;
                case DbProvider.Access:
                    this.pnlSelectDB.Controls.Add(SelectDB.SelectAccess.Instance);
                    this.pnlSelectDB.Height = SelectDB.SelectAccess.Instance.Height;
                    break;
                case DbProvider.Oracle:
                    this.pnlSelectDB.Controls.Add(SelectDB.SelectOracle.Instance);
                    this.pnlSelectDB.Height = SelectDB.SelectOracle.Instance.Height;
                    break;
                default:
                    throw new NullReferenceException(dbProvider + "未实现");
            }
        }
        #endregion

        #region 尝试登录
        /// <summary>
        /// 尝试数据库连接
        /// </summary>
        private void btnTryConnect_Click(object sender, EventArgs e)
        {
            //设置当前访问的数据库参数
            IDbParam dbParam;

            //设置当前访问的数据库对象
            IDbOperate dbOperate = this.GetDBOperate(out dbParam);

            //检测数据库对象是否为空
            if (dbOperate == null)
            {
                MessageUtil.ShowWarning("请输入必填项");
                return;
            }

            //尝试数据库连接
            string connectDBError = string.Empty;
            bool canConnectDB = dbOperate.TryConnect(out connectDBError);
            if (canConnectDB)
            {
                //显示登录按钮
                this.btnLogin.Enabled = true;

                //显示选择数据库
                this.pnlBottom.Visible = true;

                //显示所有数据库
                this.cmbDBName.Items.Clear();
                var databases = dbOperate.GetDataBaseInfo();
                if (databases != null && databases.Count > 0)
                {
                    foreach (var database in databases)
                        this.cmbDBName.Items.Add(database.Name);

                    if (string.IsNullOrEmpty(this.HistoryDataBaseName))
                        this.cmbDBName.SelectedIndex = 0;
                    else
                        this.cmbDBName.SelectedItem = this.HistoryDataBaseName;
                }
            }
            else
            {
                MessageUtil.ShowError(connectDBError);
            }
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录管理
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string aliasName = this.txtAliasName.Text.Trim();

            //判断是否填写连接名
            if (string.IsNullOrEmpty(aliasName))
            {
                MessageUtil.ShowWarning("请输入连接名");
                return;
            }

            if (!this.IsModifyProperty)
            {
                //判断连接名是否存在
                if (LinkUtil.Instance.IsExistsLink(aliasName))
                {
                    MessageUtil.ShowWarning("连接名已存在");
                    return;
                }
            }

            //判断是否选择数据库
            if (this.cmbDBName.SelectedItem == null)
            {
                MessageUtil.ShowWarning("请选择数据库");
                return;
            }

            //设置当前访问的数据库参数
            IDbParam dbParam;

            //设置当前访问的数据库对象
            IDbOperate dbOperate = this.GetDBOperate(out dbParam);

            //检测数据库对象是否为空
            if (dbOperate == null)
            {
                MessageUtil.ShowWarning("请输入必填项");
                return;
            }

            //保存连接别名
            this.P_AliasName = aliasName;

            //保存连接参数
            this.P_DbParam = dbParam;

            //保存连接数据库对象
            this.P_DbOperate = dbOperate;

            //保存登录记录
            if (!this.IsModifyProperty)
                LinkUtil.Instance.AddLink(aliasName, dbParam);
            else
                LinkUtil.Instance.ModifyLink(aliasName, dbParam);

            //显示主窗体
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        /// <summary>
        /// 加载数据库驱动
        /// </summary>
        private void LoadDBProviders()
        {
            //绑定数据库类型
            var dbProviders = typeof(DbProvider).GetItems();
            if (dbProviders != null && dbProviders.Count > 0)
            {
                this.cmbDbProvider.DataSource = dbProviders;
                this.cmbDbProvider.DisplayMember = "Text";
                this.cmbDbProvider.ValueMember = "Value";
                this.cmbDbProvider.SelectedIndex = 0;
            }

            //默认选中某个驱动
            if (this.DBProvider.HasValue)
            {
                this.cmbDbProvider.SelectedValue = (int)this.DBProvider;
            }
        }

        /// <summary>
        /// 设置当前访问数据库对象
        /// </summary>
        private IDbOperate GetDBOperate(out IDbParam dbParam)
        {
            //获取数据库类型
            dynamic selectDbProvider = this.cmbDbProvider.SelectedItem;
            DbProvider dbProvider = (DbProvider)Enum.Parse(typeof(DbProvider), selectDbProvider.Value.ToString());

            //获取数据库名称
            string dbName = string.Empty;
            if (this.cmbDBName.SelectedItem != null)
                dbName = this.cmbDBName.SelectedItem.ToString();

            //映射数据对象
            IDbOperate dbOperate;
            switch (dbProvider)
            {
                case DbProvider.SQL2005:
                    {
                        dbOperate = new SqlServerOperate();
                        dbParam = new SqlServerParam()
                        {
                            DbProvider = dbProvider,
                            ServerName = SelectDB.SelectSqlServer.Instance.ServerName,
                            DbName = dbName,
                            UserID = SelectDB.SelectSqlServer.Instance.UserID,
                            UserPass = SelectDB.SelectSqlServer.Instance.UserPass,
                        };
                        if (string.IsNullOrEmpty(SelectDB.SelectSqlServer.Instance.ServerName)) return null;
                        if (string.IsNullOrEmpty(SelectDB.SelectSqlServer.Instance.UserID)) return null;
                        if (string.IsNullOrEmpty(SelectDB.SelectSqlServer.Instance.UserPass)) return null;
                    }
                    break;
                case DbProvider.MySQL:
                    {
                        dbOperate = new MySqlOperate();
                        dbParam = new MySqlParam()
                        {
                            DbProvider = dbProvider,
                            ServerName = SelectDB.SelectMySql.Instance.ServerName,
                            Port = SelectDB.SelectMySql.Instance.Port,
                            DbName = dbName,
                            UserID = SelectDB.SelectMySql.Instance.UserID,
                            UserPass = SelectDB.SelectMySql.Instance.UserPass,
                        };
                        if (string.IsNullOrEmpty(SelectDB.SelectMySql.Instance.ServerName)) return null;
                        if (string.IsNullOrEmpty(SelectDB.SelectMySql.Instance.UserID)) return null;
                        if (string.IsNullOrEmpty(SelectDB.SelectMySql.Instance.UserPass)) return null;
                    }
                    break;
                case DbProvider.Sqlite:
                    {
                        dbOperate = new SqliteOperate();
                        dbParam = new SqliteParam()
                        {
                            DbProvider = dbProvider,
                            DbName = dbName,
                            DbPath = SelectDB.SelectSqlite.Instance.DbPath,
                        };
                        if (string.IsNullOrEmpty(SelectDB.SelectSqlite.Instance.DbPath)) return null;
                    }
                    break;
                case DbProvider.Access:
                    {
                        dbOperate = new AccessOperate();
                        dbParam = new AccessParam()
                        {
                            DbProvider = dbProvider,
                            DbName = dbName,
                            DbPath = SelectDB.SelectAccess.Instance.DbPath,
                            HasPassword = SelectDB.SelectAccess.Instance.HasPassword,
                            DbPassword = SelectDB.SelectAccess.Instance.DbPassword
                        };
                        if (string.IsNullOrEmpty(SelectDB.SelectAccess.Instance.DbPath)) return null;
                    }
                    break;
                case DbProvider.Oracle:
                    {
                        dbOperate = new OracleOperate();
                        dbParam = new OracleParam()
                        {
                            DbProvider = dbProvider,
                            ServerName = SelectDB.SelectOracle.Instance.ServerName,
                            DbName = dbName,
                            UserID = SelectDB.SelectOracle.Instance.UserID,
                            UserPass = SelectDB.SelectOracle.Instance.UserPass,
                        };
                        if (string.IsNullOrEmpty(SelectDB.SelectOracle.Instance.ServerName)) return null;
                        if (string.IsNullOrEmpty(SelectDB.SelectOracle.Instance.UserID)) return null;
                        if (string.IsNullOrEmpty(SelectDB.SelectOracle.Instance.UserPass)) return null;
                    }
                    break;
                default:
                    throw new NullReferenceException(dbProvider + "未实现");
            }

            //保存连接对象
            dbOperate.DBParam = dbParam;

            //生成数据库连接字符串
            dbOperate.BuildConnectionString(dbParam);

            return dbOperate;
        }

        /// <summary>
        /// 设置历史连接属性
        /// </summary>
        public void SetHistoryLink()
        {
            this.txtAliasName.Text = this.P_AliasName;

            this.cmbDbProvider.SelectedValue = (int)this.P_DbParam.DbProvider;

            switch (this.P_DbParam.DbProvider)
            {
                case DbProvider.SQL2005:
                    {
                        SqlServerParam dbParam = this.P_DbParam as SqlServerParam;

                        SelectDB.SelectSqlServer.Instance.ServerName = dbParam.ServerName;
                        SelectDB.SelectSqlServer.Instance.UserID = dbParam.UserID;
                        SelectDB.SelectSqlServer.Instance.UserPass = dbParam.UserPass;
                        this.HistoryDataBaseName = dbParam.DbName;
                    };
                    break;
                case DbProvider.MySQL:
                    {
                        MySqlParam dbParam = this.P_DbParam as MySqlParam;

                        SelectDB.SelectMySql.Instance.ServerName = dbParam.ServerName;
                        SelectDB.SelectMySql.Instance.Port = dbParam.Port;
                        SelectDB.SelectMySql.Instance.UserID = dbParam.UserID;
                        SelectDB.SelectMySql.Instance.UserPass = dbParam.UserPass;
                        this.HistoryDataBaseName = dbParam.DbName;
                    };
                    break;
                case DbProvider.Sqlite:
                    {
                        SqliteParam dbParam = this.P_DbParam as SqliteParam;

                        SelectDB.SelectSqlite.Instance.DbPath = dbParam.DbPath;
                        this.HistoryDataBaseName = dbParam.DbName;
                    };
                    break;
                case DbProvider.Access:
                    {
                        AccessParam dbParam = this.P_DbParam as AccessParam;

                        SelectDB.SelectAccess.Instance.DbPath = dbParam.DbPath;
                        SelectDB.SelectAccess.Instance.HasPassword = dbParam.HasPassword;
                        SelectDB.SelectAccess.Instance.DbPassword = dbParam.DbPassword;
                        this.HistoryDataBaseName = dbParam.DbName;
                    };
                    break;
                default:
                    throw new NullReferenceException(this.P_DbParam.DbProvider + "未实现");
            }
        }
    }
}
