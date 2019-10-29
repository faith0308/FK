using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IFieldPredicate : IComparePredicate
    {
        object Value { get; set; }
    }
}
