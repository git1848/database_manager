using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataBaseFront
{
    public static class Extensions_DataTable
    {
        public static DataTable PagedTable(this DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;

            DataTable newdt = dt.Copy();
            newdt.Clear();

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;

            DataRow newdr;
            DataRow dr;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                newdr = newdt.NewRow();
                dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
    }
}
