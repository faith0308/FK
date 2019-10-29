using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    public interface IMultipleResultReader
    {
        IEnumerable<T> Read<T>();
    }
}
