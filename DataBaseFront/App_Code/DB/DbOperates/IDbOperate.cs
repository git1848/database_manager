using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataBaseFront.DB.DbParams;
using DataBaseFront.Entity;

namespace DataBaseFront.DB.DbOperates
{
    public interface IDbOperate
    {
        DB.DbParams.IDbParam DBParam { get; set; }
        string DbName { get; set; }
        string ConnectionString { get; set; }
        char KeyWordStart { get; }
        char KeyWordEnd { get; }
        void FilterKeyWord(ref string sql);
        bool Exists(string sql);
        bool IsExistColumn(string table_name, string column_name);
        bool IsExistTable(string table_name);
        int GetMaxID(string table_name, string field_name);
        bool TryConnect(out string error);
        int ExecuteSql(string sql);
        object GetSingle(string sql);
        DataTable GetDataTable(string sql);
        string BuildConnectionString(IDbParam dbParam);
        string BuildSelectTableSql(string table_name);
        string BuildSelectTableSql(string table_name, int top);
        string BuildCreateTableSql(string table_name);
        string BuildCreateViewSql(string view_name);
        string BuildCreateProcedureSql(string procedure_name);
        string BuildDropTableSql(string table_name);
        string BuildDropViewSql(string view_name);
        string BuildDropProcedureSql(string procedure_name);
        string BuildInsertSql(string table_name);
        string BuildInsertSqlWidthParam(string table_name);
        string BuildDeleteSql(string table_name);
        string BuildDeleteSqlWidthParam(string table_name);
        string BuildUpdateSql(string table_name);
        string BuildUpdateSqlWidthParam(string table_name);
        string BuildInsertColumnSql(string table_name, string column_name, string column_dbType, int length, int numericScale, bool IsPrimaryKey);
        string BuildUpdateColumnNameSql(string table_name, string column_name, string column_new_name);
        string BuildUpdateColumnDbTypeSql(string table_name, string column_name, string column_dbType, int length, int numericScale);
        string BuildUpdateColumnPrimaryKeySql(string table_name, string column_name, bool IsPrimaryKey);
        string BuildDeleteColumnSql(string table_name, string column_name);
        IList<MGDataBase> GetDataBaseInfo();
        IList<MGTable> GetTables();
        IList<string> GetColumnNames(string table_name);
        IList<MGColumn> GetColumns(string table_name);
        IList<MGProcedure> GetProcedures();
        IList<MGView> GetViews();
        IList<MGTableInfo> GetTableInfos();
        IList<MGTableInfo> GetTableInfos(IList<MGTable> tables);
        IList<TypeEntity> GetDBTypes();
    }
}
