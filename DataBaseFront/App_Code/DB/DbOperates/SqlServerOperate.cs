using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataBaseFront.DB.DbParams;

namespace DataBaseFront.DB.DbOperates
{
    public class SqlServerOperate : IDbOperate
    {
        public IDbParam DBParam { get; set; }
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public char KeyWordStart { get { return '['; } }
        public char KeyWordEnd { get { return ']'; } }
        public void FilterKeyWord(ref string sql)
        {
            sql = sql.Replace('【', KeyWordStart);
            sql = sql.Replace('】', KeyWordEnd);
        }

        public bool Exists(string sql)
        {
            object obj = GetSingle(sql);
            if (obj == null || obj == DBNull.Value)
                return false;
            else
                return true;
        }

        public bool IsExistColumn(string table_name, string column_name)
        {
            return GetColumnNames(table_name).Contains(column_name);
        }

        public bool IsExistTable(string table_name)
        {
            object obj = GetSingle(string.Format("SELECT COUNT(1) FROM SYSOBJECTS WHERE ID = OBJECT_ID(N'【{0}】') AND OBJECTPROPERTY(ID, N'ISUSERTABLE') = 1", table_name));
            if (obj == null || obj == DBNull.Value)
                return false;
            else
                return Convert.ToInt32(obj) > 0;
        }

        public int GetMaxID(string table_name, string field_name)
        {
            object obj = GetSingle(string.Format("SELECT MAX(【{0}】)+1 FROM 【{1}】", field_name, table_name));
            if (obj == null || obj == DBNull.Value)
                return 1;
            else
                return Convert.ToInt32(obj);
        }

        public bool TryConnect(out string error)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    error = string.Empty;
                    return true;
                }
                catch (SqlException ex)
                {
                    error = ex.Message;
                    return false;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public int ExecuteSql(string sql)
        {
            this.FilterKeyWord(ref sql);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = 45;
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        public object GetSingle(string sql)
        {
            this.FilterKeyWord(ref sql);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        return obj;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        public DataTable GetDataTable(string sql)
        {
            this.FilterKeyWord(ref sql);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sql, connection);
                    command.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                return dt;
            }
        }

        public string BuildConnectionString(IDbParam dbParam)
        {
            SqlServerParam sqlServerParam = dbParam as SqlServerParam;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Data Source={0}", sqlServerParam.ServerName);
            if (!string.IsNullOrEmpty(sqlServerParam.DbName))
                sb.AppendFormat(";Initial Catalog={0}", sqlServerParam.DbName);
            sb.AppendFormat(";User ID={0}", sqlServerParam.UserID);
            sb.AppendFormat(";Password={0}", sqlServerParam.UserPass);

            DbName = sqlServerParam.DbName;
            ConnectionString = sb.ToString();
            sb = null;
            return ConnectionString;
        }

        public string BuildSelectTableSql(string table_name)
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            IList<string> columns = GetColumnNames(table_name);
            if (columns != null && columns.Count > 0)
            {
                foreach (string column in columns)
                {
                    sb.AppendFormat("【{0}】,", column);
                }
                sb = sb.Remove(sb.Length - 1, 1);

                result = string.Format("SELECT {0} FROM 【{1}】", sb.ToString(), table_name);
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildSelectTableSql(string table_name, int top)
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            IList<string> columns = GetColumnNames(table_name);
            if (columns != null && columns.Count > 0)
            {
                foreach (string column in columns)
                {
                    sb.AppendFormat("【{0}】,", column);
                }
                sb = sb.Remove(sb.Length - 1, 1);

                result = string.Format("SELECT TOP {0} {1} FROM 【{2}】", top, sb.ToString(), table_name);
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildCreateTableSql(string table_name)
        {
            SqlServerParam param = this.DBParam as SqlServerParam;
            SQLDMO.SQLServer oServer = new SQLDMO.SQLServer();
            oServer.Connect(param.ServerName, param.UserID, param.UserPass);
            SQLDMO._Database mydb = oServer.Databases.Item(param.DbName, "owner");
            SQLDMO._Table myTable = mydb.Tables.Item(table_name, "dbo");
            string sql = myTable.Script(SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default, null, null, SQLDMO.SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default);
            oServer.DisConnect();
            return sql;
        }

        public string BuildCreateViewSql(string view_name)
        {
            string result = string.Empty;
            IList<string> list = new List<string>();
            DataTable dt = GetDataTable(string.Format("EXEC sp_helptext '【{0}】';", view_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow _DataRowItem in dt.Rows)
                {
                    list.Add(_DataRowItem[0].ToString());
                }

                result = string.Join("\r\n", list.ToArray());
            }
            return result;
        }

        public string BuildCreateProcedureSql(string procedure_name)
        {
            string result = string.Empty;
            IList<string> list = new List<string>();
            DataTable dt = GetDataTable(string.Format("EXEC sp_helptext '【{0}】';", procedure_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow _DataRowItem in dt.Rows)
                {
                    list.Add(_DataRowItem[0].ToString());
                }

                result = string.Join("\r\n", list.ToArray());
            }
            return result;
        }

        public string BuildDropTableSql(string table_name)
        {
            string result = string.Empty;
            result = string.Format(@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'【dbo】.【{0}】') AND type in (N'U'))
DROP TABLE 【dbo】.【{0}】
GO", table_name);

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildDropViewSql(string view_name)
        {
            string result = string.Empty;
            result = string.Format(@"
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'【dbo】.【{0}】'))
DROP VIEW 【dbo】.【{0}】
GO", view_name);

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildDropProcedureSql(string procdure_name)
        {
            string result = string.Empty;
            result = string.Format(@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'【dbo】.【{0}】') AND type in (N'P', N'PC'))
DROP PROCEDURE 【dbo】.【{0}】
GO", procdure_name);

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildInsertSql(string table_name)
        {
            string result = string.Empty;
            IList<MGColumn> columnNames = GetColumns(table_name);

            if (columnNames != null && columnNames.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO 【{0}】", table_name);
                sb.Append("(");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.AppendFormat("【{0}】", columnNames[i].Name);
                }
                sb.Append(")");
                sb.Append(" VALUES ");
                sb.Append("(");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(DBTypeUtil.FormatColumnValue("", columnNames[i].DbType));
                }
                sb.Append(")");
                result = sb.ToString();
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildInsertSqlWidthParam(string table_name)
        {
            string result = string.Empty;
            IList<string> columnNames = GetColumnNames(table_name);

            if (columnNames != null && columnNames.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO 【{0}】", table_name);
                sb.Append("(");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.AppendFormat("【{0}】", columnNames[i]);
                }
                sb.Append(")");
                sb.Append(" VALUES ");
                sb.Append("(");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.AppendFormat("@{0}", columnNames[i]);
                }
                sb.Append(")");
                result = sb.ToString();
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildDeleteSql(string table_name)
        {
            string result = string.Empty;
            IList<MGColumn> columnNames = GetColumns(table_name);

            if (columnNames != null && columnNames.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("DELETE FROM 【{0}】 WHERE 【{1}】 = {2}", table_name, columnNames[0].Name, DBTypeUtil.FormatColumnValue("", columnNames[0].DbType));

                result = sb.ToString();
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildDeleteSqlWidthParam(string table_name)
        {
            string result = string.Empty;
            IList<string> columnNames = GetColumnNames(table_name);

            if (columnNames != null && columnNames.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("DELETE FROM 【{0}】 WHERE 【{1}】 = @{1}", table_name, columnNames[0]);

                result = sb.ToString();
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildUpdateSql(string table_name)
        {
            string result = string.Empty;
            IList<MGColumn> columnNames = GetColumns(table_name);

            if (columnNames != null && columnNames.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("UPDATE 【{0}】", table_name);
                sb.Append(" SET ");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.AppendFormat("【{0}】 = {1}", columnNames[i].Name, DBTypeUtil.FormatColumnValue("", columnNames[i].DbType));
                }
                sb.AppendFormat(" WHERE 【{0}】 = {1}", columnNames[0].Name, DBTypeUtil.FormatColumnValue("", columnNames[0].DbType));

                result = sb.ToString();
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildUpdateSqlWidthParam(string table_name)
        {
            string result = string.Empty;
            IList<string> columnNames = GetColumnNames(table_name);

            if (columnNames != null && columnNames.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("UPDATE 【{0}】", table_name);
                sb.Append(" SET ");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.AppendFormat("【{0}】 = @{0}", columnNames[i]);
                }
                sb.AppendFormat(" WHERE 【{0}】 = @{0}", columnNames[0]);

                result = sb.ToString();
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildInsertColumnSql(string table_name, string column_name, string column_dbType, int length, int numericScale, bool IsPrimaryKey)
        {
            //ALTER TABLE [dbo].[Test] ADD [Age] int NULL 
            return string.Format("{0}:{1},{2},{3},{4},{5},{6}", "BuildInsertColumnSql", table_name, column_name, column_dbType, length, numericScale, IsPrimaryKey);
        }

        public string BuildUpdateColumnNameSql(string table_name, string column_name, string column_new_name)
        {
            return string.Format("EXEC sp_rename N'[{0}].[{1}]', N'{2}', 'COLUMN';", table_name, column_name, column_new_name);
        }

        public string BuildUpdateColumnDbTypeSql(string table_name, string column_name, string column_dbType, int length, int numericScale)
        {
            //ALTER TABLE [dbo].[Test] ALTER COLUMN [Age2] tinyint 
            return string.Format("{0}:{1},{2},{3},{4},{5}", "BuildUpdateColumnDbTypeSql", table_name, column_name, column_dbType, length, numericScale);
        }

        public string BuildUpdateColumnPrimaryKeySql(string table_name, string column_name, bool IsPrimaryKey)
        {
            string sql = string.Empty;
            var primaryKeys = GetPrimarkKeyColumns(table_name);
            if (primaryKeys != null && primaryKeys.Count > 0)
            {
                sql += string.Format("ALTER TABLE [{0}] DROP CONSTRAINT [{1}]", table_name, primaryKeys[1].PrimaryKeyName);
            }
            if (IsPrimaryKey)
            {
                sql += "\n";
                sql += string.Format("ALTER TABLE [{0}] ADD CONSTRAINT [PK_{0}_{1}] PRIMARY KEY ([{1}])", table_name, column_name);
            }
            return sql;
        }

        public string BuildDeleteColumnSql(string table_name, string column_name)
        {
            return string.Format("ALTER TABLE [{0}] DROP COLUMN [{1}]", table_name, column_name);
        }

        public IList<MGDataBase> GetDataBaseInfo()
        {
            IList<MGDataBase> list = new List<MGDataBase>();
            DataTable dt = GetSchema("Databases");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MGDataBase()
                    {
                        Name = dr["database_name"].ToString()
                    });
                }
            }
            return list.OrderBy(o => o.Name).ToList();
        }

        public IList<MGTable> GetTables()
        {
            IList<MGTable> list = new List<MGTable>();
            string[] rs = new string[] { null, null, null, "BASE TABLE" };
            DataTable dt = GetSchema("tables", rs);
            if (dt != null & dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["table_name"].ToString().ToLower() != "sysdiagrams")
                    {
                        list.Add(new MGTable()
                        {
                            Name = dr["table_name"].ToString()
                        });
                    }
                }
            }
            return list.OrderBy(o => o.Name).ToList();
        }

        public IList<string> GetColumnNames(string table_name)
        {
            string[] restrictions = new string[] { null, null, table_name };
            DataTable dt = GetSchema("Columns", restrictions);
            IList<string> list = new List<string>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                DataView dataView = dt.DefaultView;
                dataView.Sort = "ORDINAL_POSITION";

                DataTable tmpTable = dataView.ToTable();

                foreach (DataRow dr in tmpTable.Rows)
                {
                    list.Add(dr["COLUMN_NAME"].ToString());
                }

                tmpTable = null;
            }
            dt = null;

            return list;
        }

        public IList<MGColumn> GetColumns(string table_name)
        {
            string sql = @"
SELECT 
    表名       = case when a.colorder=1 then d.name else '' end,
    表说明     = case when a.colorder=1 then isnull(f.value,'') else '' end,
    字段序号   = a.colorder,
    字段名     = a.name,
    标识       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
    主键       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                     SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then '√' else '' end,
    类型       = b.name,
    占用字节数 = a.length,
    长度       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    小数位数   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
    允许空     = case when a.isnullable=1 then '√'else '' end,
    默认值     = isnull(e.text,''),
    字段说明   = isnull(g.value,'')
FROM 
    syscolumns a
left join 
    systypes b on a.xusertype=b.xusertype
inner join 
    sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join 
    syscomments e on a.cdefault=e.id
left join 
	sys.extended_properties g on a.id=G.major_id and a.colid=g.minor_id  
left join 
	sys.extended_properties f on d.id=f.major_id and f.minor_id=0
where 
    d.name='{0}'    --如果只查询指定表,加上此条件
order by 
    a.id,a.colorder";

            sql = string.Format(sql, table_name);

            DataTable dt = GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                IList<MGColumn> list = new List<MGColumn>();
                MGColumn columnInfo;
                foreach (DataRow dr in dt.Rows)
                {
                    columnInfo = new MGColumn();

                    columnInfo.Index = Convert.ToInt32(dr["字段序号"]);
                    columnInfo.Name = Convert.ToString(dr["字段名"]);
                    columnInfo.Remark = Convert.ToString(dr["字段说明"]);
                    columnInfo.AutoIncrement = Convert.ToBoolean(dr["标识"].ToString() == "√");
                    columnInfo.IsPrimaryKey = Convert.ToBoolean(dr["主键"].ToString() == "√");
                    columnInfo.DbType = Convert.ToString(dr["类型"]).ToLower();
                    columnInfo.Length = Convert.ToInt32(dr["占用字节数"]);
                    columnInfo.NumericPrecision = Convert.ToInt32(dr["长度"]);
                    columnInfo.NumericScale = Convert.ToInt32(dr["小数位数"]);
                    columnInfo.AllowNull = Convert.ToBoolean(dr["允许空"].ToString() == "√");
                    columnInfo.DefaultValue = Convert.ToString(dr["默认值"]);

                    list.Add(columnInfo);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public IList<MGProcedure> GetProcedures()
        {
            IList<MGProcedure> list = new List<MGProcedure>();
            DataTable dt = GetSchema("Procedures");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["routine_type"].ToString().ToUpper() != "FUNCTION")
                    {
                        list.Add(new MGProcedure()
                        {
                            Name = dr["routine_name"].ToString()
                        });
                    }

                }
            }
            return list.OrderBy(o => o.Name).ToList();
        }

        public IList<MGView> GetViews()
        {
            IList<MGView> list = new List<MGView>();
            string[] rs = new string[] { null, null, null, "BASE TABLE" };
            DataTable dt = GetSchema("views");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MGView()
                    {
                        Name = dr["table_name"].ToString()
                    });
                }
            }
            return list.OrderBy(o => o.Name).ToList();
        }

        public IList<MGTableInfo> GetTableInfos()
        {
            return GetTableInfos(GetTables());
        }

        public IList<MGTableInfo> GetTableInfos(IList<MGTable> tables)
        {
            if (tables == null) return null;

            var source_tables = tables;
            var target_tables = new List<MGTableInfo>();
            foreach (var table in source_tables)
            {
                var columns = GetColumns(table.Name);
                target_tables.Add(new MGTableInfo() { Table = table, Columns = columns });
            }
            return target_tables;
        }

        private DataTable GetSchema(string collection_name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    dt = connection.GetSchema(collection_name);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                return dt;
            }
        }

        private DataTable GetSchema(string collection_name, string[] restiction_values)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    dt = connection.GetSchema(collection_name, restiction_values);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                return dt;
            }
        }

        public IList<Entity.TypeEntity> GetDBTypes()
        {
            return DBTypeUtil.GetTypeEntity("SqlDbType");
        }

        public IList<MGPrimaryKeyColumn> GetPrimarkKeyColumns(string table_name)
        {
            IList<MGPrimaryKeyColumn> list = new List<MGPrimaryKeyColumn>();
            DataTable dt = GetDataTable(string.Format("SP_PKEYS '{0}'", table_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MGPrimaryKeyColumn()
                    {
                        ColumnName = dr["COLUMN_NAME"].ToString(),
                        Seq = Convert.ToInt32(dr["KEY_SEQ"].ToString()),
                        PrimaryKeyName = dr["PK_NAME"].ToString()
                    });
                }
            }
            return list.OrderBy(o => o.Seq).ToList();
        }
    }
}
