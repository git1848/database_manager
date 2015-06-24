using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using DataBaseFront.DB.DbParams;

namespace DataBaseFront.DB.DbOperates
{
    public class AccessOperate : IDbOperate
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
            var tables = GetTables();
            return tables.Where(o => o.Name == table_name).Count() > 0;
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
            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    error = string.Empty;
                    return true;
                }
                catch (OleDbException ex)
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

            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = 45;
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (OleDbException ex)
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

            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        return obj;
                    }
                    catch (OleDbException ex)
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

            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(sql, connection);
                    command.Fill(dt);
                }
                catch (OleDbException ex)
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
            AccessParam accessParam = dbParam as AccessParam;

            if (!System.IO.Path.IsPathRooted(accessParam.DbPath))
                accessParam.DbPath = System.IO.Path.Combine(Environment.CurrentDirectory, accessParam.DbPath);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Provider={0}", "Microsoft.Jet.OLEDB.4.0");
            sb.AppendFormat(";Data Source={0}", accessParam.DbPath);
            if (accessParam.HasPassword)
                sb.AppendFormat(";Jet OLEDB:Database Password={0}", accessParam.DbPassword);

            DbName = accessParam.DbName;
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

                result = string.Format("SELECT {0} FROM 【{1}】;", sb.ToString(), table_name);
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

                result = string.Format("SELECT TOP {2} {0} FROM 【{1}】;", sb.ToString(), table_name, top);
            }

            this.FilterKeyWord(ref result);
            return result;
        }

        public string BuildCreateTableSql(string table_name)
        {
            throw new NullReferenceException("未实现BuildCreateTableSql");
        }

        public string BuildCreateViewSql(string view_name)
        {
            string result = string.Empty;

            DataTable dt = GetSchema("Views");
            //DataTable dt = GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new string[] { null, null, null, "VIEW" });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (view_name == dr["TABLE_NAME"].ToString())
                    {
                        result = dr["VIEW_DEFINITION"].ToString();
                        break;
                    }
                }
            }
            return result;
        }

        public string BuildCreateProcedureSql(string procedure_name)
        {
            throw new NotImplementedException("Access无存储过程");
        }

        public string BuildDropTableSql(string table_name)
        {
            throw new NullReferenceException("未实现BuildDropTableSql");
        }

        public string BuildDropViewSql(string view_name)
        {
            throw new NullReferenceException("未实现BuildDropViewSql");
        }

        public string BuildDropProcedureSql(string procdure_name)
        {
            throw new NotImplementedException("Access无存储过程");
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
            return string.Format("{0}:{1},{2},{3},{4},{5},{6}", "BuildInsertColumnSql", table_name, column_name, column_dbType, length, numericScale, IsPrimaryKey);
        }

        public string BuildUpdateColumnNameSql(string table_name, string column_name, string column_new_name)
        {
            return string.Format("{0}:{1},{2},{3}", "BuildInsertColumnSql", table_name, column_name, column_new_name);
        }

        public string BuildUpdateColumnDbTypeSql(string table_name, string column_name, string column_dbType, int length, int numericScale)
        {
            return string.Format("{0}:{1},{2},{3},{4},{5}", "BuildUpdateColumnDbTypeSql", table_name, column_name, column_dbType, length, numericScale);
        }

        public string BuildUpdateColumnPrimaryKeySql(string table_name, string column_name, bool IsPrimaryKey)
        {
            return string.Format("{0}:{1},{2},{3}", "BuildUpdateColumnPrimaryKeySql", table_name, column_name, IsPrimaryKey);
        }

        public string BuildDeleteColumnSql(string table_name, string column_name)
        {
            return string.Format("{0}:{1},{2}", "BuildDeleteColumnSql", table_name, column_name);
        }

        public IList<MGDataBase> GetDataBaseInfo()
        {
            AccessParam accessParam = DBParam as AccessParam;

            IList<MGDataBase> list = new List<MGDataBase>();
            list.Add(new MGDataBase()
            {
                Name = System.IO.Path.GetFileNameWithoutExtension(accessParam.DbPath)
            });
            return list;
        }

        public IList<MGTable> GetTables()
        {
            IList<MGTable> list = new List<MGTable>();
            //DataTable dt = GetSchema("Tables", new string[] { null, null, null, "TABLE" });
            DataTable dt = GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new string[] { null, null, null, "TABLE" });
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
            string[] restrictions = new string[] { null, null, table_name };
            DataTable dt = GetSchema("Columns", restrictions);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "ordinal_position";

                DataTable dt2 = dv.ToTable();

                foreach (DataRow dr in dt2.Rows)
                {
                    list.Add(Convert.ToString(dr["COLUMN_NAME"]));
                }
            }
            return list;
        }

        public IList<MGColumn> GetColumns(string table_name)
        {
            IList<MGColumn> list = new List<MGColumn>();
            //DataTable dt = GetSchema("Columns", new string[] { null, null, table_name });
            DataTable dt = GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new string[] { null, null, table_name });
            IList<string> primarykeyColumns = this.GetPrimaryKeyColumns(table_name);
            IList<string> autoIncrementColumns = this.GetAutoIncrementColumns(table_name);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "ORDINAL_POSITION";

                DataTable dt2 = dv.ToTable();
                MGColumn columnInfo;

                int dataTypeId, column_flags, columnSize, numericPrecision, numericScale;
                string dataType = string.Empty;
                foreach (DataRow dr in dt2.Rows)
                {
                    dataTypeId = Convert.ToInt32(dr["DATA_TYPE"]);
                    column_flags = Convert.ToInt32(dr["COLUMN_FLAGS"]);
                    dataType = this.GetDataType(dataTypeId, column_flags);
                    columnSize = FormUtil.IsNull(dr["CHARACTER_MAXIMUM_LENGTH"]) ? 0 : Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"]);
                    numericPrecision = FormUtil.IsNull(dr["NUMERIC_PRECISION"]) ? 0 : Convert.ToInt32(dr["NUMERIC_PRECISION"]);
                    numericScale = FormUtil.IsNull(dr["NUMERIC_SCALE"]) ? 0 : Convert.ToInt32(dr["NUMERIC_SCALE"]);

                    columnInfo = new MGColumn();
                    columnInfo.Index = Convert.ToInt32(dr["ORDINAL_POSITION"]);
                    columnInfo.Name = Convert.ToString(dr["COLUMN_NAME"]);
                    columnInfo.Remark = Convert.ToString(dr["DESCRIPTION"]);
                    columnInfo.AutoIncrement = autoIncrementColumns.Where(o => o == columnInfo.Name).Count() > 0;
                    columnInfo.IsPrimaryKey = primarykeyColumns.Where(o => o == columnInfo.Name).Count() > 0;
                    columnInfo.DbType = dataType.ToLower();
                    columnInfo.Length = columnSize;
                    columnInfo.NumericPrecision = numericPrecision;
                    columnInfo.NumericScale = numericScale;
                    columnInfo.AllowNull = Convert.ToBoolean(dr["IS_NULLABLE"].ToString());
                    if (Convert.ToBoolean(dr["COLUMN_HASDEFAULT"]))
                        columnInfo.DefaultValue = Convert.ToString(dr["COLUMN_DEFAULT"]);
                    else
                        columnInfo.DefaultValue = string.Empty;
                    list.Add(columnInfo);
                }
                return list;
            }
            return list.OrderBy(o => o.Index).ToList();
        }

        public IList<MGProcedure> GetProcedures()
        {
            IList<MGProcedure> list = new List<MGProcedure>();
            //DataTable dt = GetSchema("Procedures");
            DataTable dt = GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Procedures, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MGProcedure()
                    {
                        Name = dr["TABLE_NAME"].ToString()
                    });
                }
            }
            return list.OrderBy(o => o.Name).ToList();
        }

        public IList<MGView> GetViews()
        {
            IList<MGView> list = new List<MGView>();
            //DataTable dt = GetSchema("Views");
            DataTable dt = GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new string[] { null, null, null, "VIEW" });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MGView()
                    {
                        Name = dr["TABLE_NAME"].ToString()
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

        public DataTable GetSchema(string collection_name)
        {
            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    dt = connection.GetSchema(collection_name);
                }
                catch (OleDbException ex)
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

        public DataTable GetSchema(string collection_name, string[] restiction_values)
        {
            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    dt = connection.GetSchema(collection_name, restiction_values);
                }
                catch (OleDbException ex)
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

        public DataTable GetOleDbSchemaTable(Guid collection_name, string[] restiction_values)
        {
            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    dt = connection.GetOleDbSchemaTable(collection_name, restiction_values);
                }
                catch (OleDbException ex)
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

        private string GetDataType(int dataType, int column_flags)
        {
            string typeName = string.Empty;
            switch (dataType)
            {
                case 2:
                    typeName = "Short";
                    break;
                case 3:
                    {
                        if (column_flags == 90)
                            typeName = "Long";
                        else
                            typeName = "Long";
                    }
                    break;
                case 4:
                    typeName = "Single";
                    break;
                case 5:
                    typeName = "Double";
                    break;
                case 6:
                    typeName = "Currency";
                    break;
                case 7:
                    typeName = "DateTime";
                    break;
                case 11:
                    typeName = "Bit";
                    break;
                case 17:
                    typeName = "Byte";
                    break;
                case 72:
                    typeName = "GUID";
                    break;
                case 128:
                    typeName = "Binary";
                    break;
                case 130:
                    {
                        if (column_flags == 234)
                            typeName = "LongText";
                        else
                            typeName = "VarChar";
                    }
                    break;
                case 131:
                    typeName = "Decimal";
                    break;
                default:
                    typeName = dataType.ToString();
                    break;
            }
            return typeName.ToLower();
        }

        private IList<string> GetPrimaryKeyColumns(string table_name)
        {
            IList<string> list = new List<string>();
            DataTable dt = GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Primary_Keys, new string[] { null, null, table_name });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr["COLUMN_NAME"].ToString());
                }
            }
            return list;
        }

        private IList<string> GetAutoIncrementColumns(string table_name)
        {
            IList<string> list = new List<string>();
            ADODB.Recordset rs = new ADODB.Recordset();

            string sql = "SELECT * FROM 【{0}】";
            FilterKeyWord(ref sql);

            rs.Open(string.Format(sql, table_name)
                    , this.ConnectionString
                    , ADODB.CursorTypeEnum.adOpenDynamic
                    , ADODB.LockTypeEnum.adLockOptimistic
                    , -1);

            foreach (ADODB.Field filed in rs.Fields)
            {
                //BASECOLUMNNAME, BASETABLENAME, ISAUTOINCREMENT, ISCASESENSITIVE, COLLATINGSEQUENCE
                if (filed.Properties[2].Value == true)
                {
                    list.Add((string)filed.Properties[0].Value);
                }
            }
            rs.Close();
            rs = null;
            return list;
        }

        public IList<Entity.TypeEntity> GetDBTypes()
        {
            return DBTypeUtil.GetTypeEntity("OleDbType");
        }
    }
}
