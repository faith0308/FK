using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IExistsPredicate
    {
        IPredicate Predicate { get; set; }
        bool Not { get; set; }
    }
}
