using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IPredicateGroup : IPredicate
    {
        GroupOperator Operator { get; set; }
        IList<IPredicate> Predicates { get; set; }
    }
}
