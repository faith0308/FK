using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    /// <summary>
    /// 排序
    /// </summary>
    public interface ISort
    {
        string PropertyName { get; set; }
        bool Ascending { get; set; }
    }
}
