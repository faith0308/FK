using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IBetweenPredicate : IPredicate
    {
        string PropertyName { get; set; }
        BetweenValues Value { get; set; }
        bool Not { get; set; }
    }
}
