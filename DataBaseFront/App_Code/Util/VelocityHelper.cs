using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Commons.Collections;

using NVelocity;
using NVelocity.App;
using NVelocity.Context;
using NVelocity.Runtime;
using System.Windows.Forms;

namespace DataBaseFront.Util
{
    /// <summary>
    /// NVelocity模板工具类
    /// </summary>
    public class VelocityHelper
    {
        private VelocityEngine _velocity = null;
        private IContext _context = null;

        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public VelocityHelper() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatDir">模板文件夹路径</param>
        public VelocityHelper(string templatDir)
        {
            Init(templatDir);
        }

        /// <summary>
        /// 初始话NVelocity模块
        /// </summary>
        public void Init(string templatDir)
        {
            //创建VelocityEngine实例对象
            _velocity = new VelocityEngine();

            //使用设置初始化VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templatDir);
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");

            //模板的缓存设置
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_CACHE, true);              //是否缓存
            props.AddProperty("file.resource.loader.modificationCheckInterval", (Int64)30);    //缓存时间(秒)

            _velocity.Init(props);

            //为模板变量赋值
            _context = new VelocityContext();
        }

        /// <summary>
        /// 给模板变量赋值
        /// </summary>
        /// <param name="key">模板变量</param>
        /// <param name="value">模板变量值</param>
        public void Put(string key, object value)
        {
            if (_context == null)
                _context = new VelocityContext();
            _context.Put(key, value);
        }

        /// <summary>
        /// 显示模板
        /// </summary>
        /// <param name="templatFileName">模板文件名</param>
        public string Display(string templatFileName)
        {
            //从文件中读取模板
            Template template = _velocity.GetTemplate(templatFileName);
            //合并模板
            StringWriter writer = new StringWriter();
            template.Merge(_context, writer);
            //输出
            return writer.ToString();
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">内容</param>
        public void Save(string fileName, string content)
        {
            string path = Path.Combine(AppInit.S_TemplateBuildFolder, fileName);
            File.WriteAllText(path, content, Encoding.UTF8);
        }
    }
}
