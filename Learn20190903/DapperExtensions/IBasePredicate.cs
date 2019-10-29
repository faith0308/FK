using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IBasePredicate : IPredicate
    {
        string PropertyName { get; set; }
    }
}
