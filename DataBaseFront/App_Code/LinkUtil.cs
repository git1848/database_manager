using System;
using System.Collections.Generic;
using DataBaseFront.DB.DbParams;
using DataBaseFront.Entity;
using DataBaseFront.DB.DbOperates;

namespace DataBaseFront
{
    public class LinkUtil
    {
        private Dictionary<string, Link> Links = new Dictionary<string, Link>();

        #region 单例
        private static LinkUtil _instance;
        public static LinkUtil Instance
        {
            get
            {
                lock (typeof(LinkUtil))
                {
                    return _instance ?? (_instance = new LinkUtil());
                }
            }
        }
        #endregion

        #region 注册连接
        public void RegisterLink(Link link)
        {
            if (Links.ContainsKey(link.AliasName)) return;

            BuildDbOperate(link);

            Links.Add(link.AliasName, link);
        }

        public void UnRegisterLink(Link link)
        {
            if (!Links.ContainsKey(link.AliasName)) return;

            link.DbOperate = null;
            link.HasOpen = false;
            Links.Remove(link.AliasName);
        }

        public void ReRegisterLink(Link link)
        {
            UnRegisterLink(link);

            RegisterLink(link);
        }

        public void BuildDbOperate(Link link)
        {
            IDbOperate dbOperate;
            switch (link.DbParam.DbProvider)
            {
                case DbProvider.SQL2005:
                    dbOperate = new SqlServerOperate();
                    break;
                case DbProvider.MySQL:
                    dbOperate = new MySqlOperate();
                    break;
                case DbProvider.Sqlite:
                    dbOperate = new SqliteOperate();
                    break;
                case DbProvider.Access:
                    dbOperate = new AccessOperate();
                    break;
                case DbProvider.Oracle:
                    dbOperate = new OracleOperate();
                    break;
                default:
                    throw new NullReferenceException(link.DbParam.DbProvider + "未实现");
            }

            //保存连接对象
            dbOperate.DBParam = link.DbParam;

            //生成数据库连接字符串
            dbOperate.BuildConnectionString(link.DbParam);

            link.DbOperate = dbOperate;
            link.HasOpen = false;
        }
        #endregion

        #region 历史记录管理
        public void AddLink(string aliasName, IDbParam dbParam)
        {
            var links = this.GetLinks();

            if (!links.ContainsKey(aliasName))
            {
                links.Add(aliasName, dbParam);

                this.SaveLinks(links);
            }
        }

        public void ModifyLink(string aliasName, IDbParam dbParam)
        {
            var links = this.GetLinks();

            if (links.ContainsKey(aliasName))
            {
                links.Remove(aliasName);
                links.Add(aliasName, dbParam);
                this.SaveLinks(links);
            }
        }

        public void RemoveLink(string aliasName)
        {
            var links = this.GetLinks();

            if (links.ContainsKey(aliasName))
            {
                links.Remove(aliasName);

                this.SaveLinks(links);
            }
        }

        public bool IsExistsLink(string aliasName)
        {
            var links = this.GetLinks();

            return (links.ContainsKey(aliasName));
        }

        private void SaveLinks(Dictionary<string, IDbParam> dbParams)
        {
            SerializeHelper.BinarySerialize(AppInit.S_HistoryPath, dbParams);
        }

        public Dictionary<string, IDbParam> GetLinks()
        {
            object obj = SerializeHelper.BinaryDeserialize(AppInit.S_HistoryPath);
            return obj == null ? new Dictionary<string, IDbParam>() : (Dictionary<string, IDbParam>)obj;
        }
        #endregion
    }
}
