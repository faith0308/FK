using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions.Mapper
{
    /// <summary>
    /// 使用键的反射和命名约定的组合自动将实体映射到表。
    ///  Automatically maps an entity to a table using a combination of reflection and naming conventions for keys.
    /// </summary>
    public class AutoClassMapper<T> : ClassMapper<T> where T : class
    {
        public AutoClassMapper()
        {
            Type type = typeof(T);
            Table(type.Name);
            AutoMap();
        }
    }
}
