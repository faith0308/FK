using DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IPredicate
    {
        string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters);
    }
}
