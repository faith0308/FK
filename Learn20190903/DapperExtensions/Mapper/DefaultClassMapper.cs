using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DapperExtensions.Mapper
{
    public sealed class DefaultClassMapper<T> : ClassMapper<T> where T : class
    {
        public DefaultClassMapper()
        {
            var name = string.Empty;
            var type = typeof(T);
            if (!type.IsDefined(typeof(TableAttribute), false)) name = type.Name.ToString();
            else
            {
                var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false)[0] as TableAttribute;
                if (!string.IsNullOrWhiteSpace(tableAttribute.Name)) name = tableAttribute.Name;
            }
            Table(name);
            AutoMap();
        }
    }
}
