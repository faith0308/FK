using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IPropertyPredicate : IComparePredicate
    {
        string PropertyName2 { get; set; }
    }
}
