using System;

namespace DataBaseFront.Entity
{
    public class DesignTable
    {
        public DesignTableAction Action;
        public int LineNo;

        public string ColumnOriginalName;
        public string ColumnOriginalDbType;
        public int ColumnOriginalLength;
        public int ColumnOriginalNumericScale;
        public bool ColumnOriginalIsPrimaryKey;

        public string ColumnName;
        public string ColumnDbType;
        public int ColumnLength;
        public int ColumnNumericScale;
        public bool ColumnIsPrimaryKey;

        public DateTime ActionTime;
        public string Sql;
    }

    public enum DesignTableAction
    {
        InsertColumn = 0,
        UpdateColumnName = 1,
        DeleteColumn = 2,
        UpdateColumnDbType = 3,
        UpdateColumnPrimaryKey = 4,
    }
}
