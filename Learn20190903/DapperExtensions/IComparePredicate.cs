using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IComparePredicate : IBasePredicate
    {
        Operator Operator { get; set; }
        bool Not { get; set; }
    }
}
