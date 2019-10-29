using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DapperExtensions.Mapper
{
    /// <summary>
    /// 将实体属性映射到数据库中对应的列。
    /// </summary>
    public interface IPropertyMap
    {
        string Name { get; }
        string ColumnName { get; }
        bool Ignored { get; }
        bool IsReadOnly { get; }
        KeyType KeyType { get; }
        PropertyInfo PropertyInfo { get; }
    }
}
