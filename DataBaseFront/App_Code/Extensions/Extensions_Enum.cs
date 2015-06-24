using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DataBaseFront
{
    public static class Extensions_Enum
    {
        /// <summary> 
        /// 获得枚举类型数据项（不包括空项）
        /// </summary> 
        /// <param name="enumType">枚举类型</param> 
        /// <returns></returns> 
        public static IList<object> GetItems(this Type enumType)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException();

            IList<object> list = new List<object>();

            // 获取Description特性 
            Type typeDescription = typeof(DescriptionAttribute);
            // 获取枚举字段
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                    continue;

                //设置显示文本
                string text = string.Empty;
                object[] array = field.GetCustomAttributes(typeDescription, false);
                if (array.Length > 0)
                    text = ((DescriptionAttribute)array[0]).Description;
                else
                    text = field.Name; //没有描述，直接取值

                //设置绑定值
                int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                //添加到列表
                list.Add(new { Text = text, Value = value });
            }
            return list;
        }
    }
}
