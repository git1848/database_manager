using DataBaseFront.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DataBaseFront
{
    public class DBTypeUtil
    {
        static Dictionary<string, string> DbTypeToCSTypes = new Dictionary<string, string>();
        static Dictionary<string, bool> NeedMarks = new Dictionary<string, bool>();
        static Dictionary<int, string> AccessTypeNames = new Dictionary<int, string>();

        /// <summary>
        /// 根据字段类型，格式化值的输出，如果是字符类型，就前后加单引号（'） 
        /// </summary>
        /// <param name="columnValue">字段值</param>
        /// <param name="columnType">自动类型</param>
        /// <returns>格式化输出</returns>
        public static string FormatColumnValue(string columnValue, string columnType)
        {
            if (NeedMarks.Count == 0)
            {
                XmlDocument doc = LoadDataTypes();
                if (doc != null)
                {
                    XmlNodeList nodes = doc.SelectNodes("/Map/NeedMark/Item");
                    foreach (XmlNode item in nodes)
                    {
                        NeedMarks.Add(item.Attributes["key"].Value, bool.Parse(item.Attributes["value"].Value));
                    }

                    nodes = null;
                    doc = null;
                }
            }

            string formatValue = string.Empty;

            if (NeedMarks.ContainsKey(columnType))
                formatValue = "'" + columnValue + "'";
            else
                formatValue = columnValue;

            return formatValue;
        }

        /// <summary>
        /// 将Access数据库类型ID，转换成类型名称
        /// </summary>
        /// <param name="typeID">类型ID</param>
        /// <returns>类型名称</returns>
        public static string ConvertAccessTypeIDToTypeName(int typeID)
        {
            if (AccessTypeNames.Count == 0)
            {
                XmlDocument doc = LoadDataTypes();
                if (doc != null)
                {
                    XmlNodeList nodes = doc.SelectNodes("/Map/AccessTypeIDToTypeName/Item");
                    foreach (XmlNode item in nodes)
                    {
                        AccessTypeNames.Add(int.Parse(item.Attributes["key"].Value), item.Attributes["value"].Value);
                    }

                    nodes = null;
                    doc = null;
                }
            }

            string typeName = string.Empty;

            if (AccessTypeNames.ContainsKey(typeID))
                typeName = AccessTypeNames[typeID];

            return typeName;
        }

        /// <summary>
        /// 将数据库类型转换为CSharp类型
        /// </summary>
        /// <param name="columnType">数据库类型</param>
        /// <returns>CSharp类型</returns>
        public static string ConvertDbTypeToCShapeType(string columnType)
        {
            if (DbTypeToCSTypes.Count == 0)
            {
                XmlDocument doc = LoadDataTypes();
                if (doc != null)
                {
                    XmlNodeList nodes = doc.SelectNodes("/Map/DbToCS/Item");
                    foreach (XmlNode item in nodes)
                    {
                        DbTypeToCSTypes.Add(item.Attributes["key"].Value, item.Attributes["value"].Value);
                    }

                    nodes = null;
                    doc = null;
                }
            }

            string typeName = string.Empty;
            if (DbTypeToCSTypes.ContainsKey(columnType))
                typeName = DbTypeToCSTypes[columnType];

            return typeName;
        }

        public static IList<TypeEntity> GetTypeEntity(string dbTypeName)
        {
            IList<TypeEntity> dbTypes = new List<TypeEntity>();
            XmlDocument doc = LoadDataTypes();
            if (doc != null)
            {
                string nameSpace, cSharpTypePrefix;
                XmlNode node = doc.SelectSingleNode(string.Format("/Map/DbType/{0}", dbTypeName));
                nameSpace = node.Attributes["NameSpace"].Value;
                cSharpTypePrefix = node.Attributes["Prefix"].Value;

                XmlNodeList nodes = doc.SelectNodes(string.Format("/Map/DbType/{0}/Item", dbTypeName));
                TypeEntity typeEntity;
                foreach (XmlNode item in nodes)
                {
                    typeEntity = new TypeEntity()
                    {
                        DBType = item.Attributes["key"].Value,
                        CSharpTypePrefix = cSharpTypePrefix,
                        CSharpType = item.Attributes["value"].Value,
                        CSharpNameSpace = nameSpace
                    };
                    dbTypes.Add(typeEntity);
                }

                nodes = null;
                doc = null;
            }
            return dbTypes;
        }

        private static XmlDocument LoadDataTypes()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(AppInit.S_DataTypeMapPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return doc;
        }
    }
}
