using DataBaseFront.DB.DbParams;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbOperates
{
    public class OracleOperate : IDbOperate
    {
        public IDbParam DBParam { get; set; }
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public char KeyWordStart { get { return '"'; } }
        public char KeyWordEnd { get { return '"'; } }
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
            object obj = GetSingle(string.Format("select COUNT(1) from User_objects t where t.OBJECT_TYPE = 'TABLE' AND t.TEMPORARY = 'N' and t.OBJECT_NAME = '{0}'", table_name.ToUpper()));
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
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    error = string.Empty;
                    return true;
                }
                catch (OracleException ex)
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

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = new OracleCommand(sql, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = 45;
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (OracleException ex)
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

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = new OracleCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        return obj;
                    }
                    catch (OracleException ex)
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

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = new OracleCommand(sql, connection))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        connection.Open();
                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                    }
                    catch (OracleException ex)
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
        }

        public string BuildConnectionString(IDbParam dbParam)
        {
            OracleParam oracleParam = dbParam as OracleParam;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Data Source={0}", oracleParam.ServerName);
            sb.AppendFormat(";User Id={0}", oracleParam.UserID);
            sb.AppendFormat(";Password={0}", oracleParam.UserPass);

            DbName = oracleParam.DbName;
            ConnectionString = sb.ToString();
            ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.192)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=CN100)));User Id=sales_curd;Password=cn100";

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

                result = string.Format("SELECT {1} FROM 【{2}】 WHERE ROWNUM <= {0} ", top, sb.ToString(), table_name);
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildCreateTableSql(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildCreateViewSql(string view_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildCreateProcedureSql(string procedure_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildDropTableSql(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildDropViewSql(string view_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildDropProcedureSql(string procdure_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildInsertSql(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildInsertSqlWidthParam(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildDeleteSql(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildDeleteSqlWidthParam(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildUpdateSql(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildUpdateSqlWidthParam(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildInsertColumnSql(string table_name, string column_name, string column_dbType, int length, int numericScale, bool IsPrimaryKey)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildUpdateColumnNameSql(string table_name, string column_name, string column_new_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildUpdateColumnDbTypeSql(string table_name, string column_name, string column_dbType, int length, int numericScale)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildUpdateColumnPrimaryKeySql(string table_name, string column_name, bool IsPrimaryKey)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public string BuildDeleteColumnSql(string table_name, string column_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public IList<MGDataBase> GetDataBaseInfo()
        {
            DataTable dt = GetDataTable("select distinct owner from all_tables order by owner");

            IList<MGDataBase> list = new List<MGDataBase>();
            foreach (DataRow drRow in dt.Rows)
            {
                list.Add(new MGDataBase()
                {
                    Name = drRow[0].ToString()
                });
            }
            return list;
        }

        public IList<MGTable> GetTables()
        {
            IList<MGTable> list = new List<MGTable>();
            DataTable dt = GetDataTable(string.Format("select t.TABLE_NAME from all_tables t where owner = '{0}' order by t.TABLE_NAME", DbName));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MGTable()
                    {
                        Name = dr["TABLE_NAME"].ToString()
                    });
                }
            }
            return list.OrderBy(o => o.Name).ToList();
        }

        public IList<string> GetColumnNames(string table_name)
        {
            IList<string> list = new List<string>();
            DataTable dt = GetDataTable(string.Format("select t.COLUMN_NAME from cols t where t.TABLE_NAME = '{0}' order by t.COLUMN_ID", table_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr["COLUMN_NAME"].ToString());
                }
            }
            return list;
        }

        public IList<MGColumn> GetColumns(string table_name)
        {
            string sql =
@"SELECT t1.Table_Name   AS 表名称,
       t3.comments     AS 表说明,
       t1.Column_Name  AS 字段名称,
       t1.Data_Type    AS 数据类型,
       t1.Data_Length  AS 长度,
       t1.DATA_PRECISION AS 精度,
       t1.DATA_SCALE     AS 小数位数, 
       t1.NullAble     AS 是否为空,
       t2.Comments     AS 字段说明,
       t1.Data_Default AS 默认值
  FROM cols t1
  left join user_col_comments t2
    on t1.Table_name = t2.Table_name
   and t1.Column_Name = t2.Column_Name
  left join user_tab_comments t3
    on t1.Table_name = t3.Table_name
 WHERE NOT EXISTS (SELECT t4.Object_Name
          FROM User_objects t4
         WHERE t4.Object_Type = 'TABLE'
           AND t4.Temporary = 'Y'
           AND t4.Object_Name = t1.Table_Name)
   and t1.TABLE_NAME = '{0}'
 ORDER BY t1.Table_Name, t1.Column_ID";

            DataTable dt = GetDataTable(string.Format(sql, table_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                IList<MGColumn> list = new List<MGColumn>();
                MGColumn columnInfo;

                int index = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    columnInfo = new MGColumn
                    {
                        Index = index,
                        Name = Convert.ToString(dr["字段名称"]),
                        Remark = Convert.ToString(dr["字段说明"]),
                        AutoIncrement = false,
                        IsPrimaryKey = false,
                        DbType = dr["数据类型"].ToString(),
                        Length = Convert.ToInt32(dr["长度"].ToString()),
                        NumericPrecision = Convert.ToInt32(dr["精度"].ToString()),
                        NumericScale = Convert.ToInt32(dr["小数位数"].ToString()),
                        AllowNull = dr["是否为空"].ToString() == "Y",
                        DefaultValue = Convert.ToString(dr["默认值"])
                    };

                    list.Add(columnInfo);

                    index++;
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
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public IList<MGView> GetViews()
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public IList<MGTableInfo> GetTableInfos()
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public IList<MGTableInfo> GetTableInfos(IList<MGTable> tables)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }

        public IList<Entity.TypeEntity> GetDBTypes()
        {
            return DBTypeUtil.GetTypeEntity("OracleDbType");
        }

        public IList<MGPrimaryKeyColumn> GetPrimarkKeyColumns(string table_name)
        {
            //TODO 未实现
            throw new NullReferenceException("");
        }
    }
}
