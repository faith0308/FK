using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    /// <summary>
    /// 排序实现
    /// </summary>
    public class Sort : ISort
    {
        public string PropertyName { get; set; }
        public bool Ascending { get; set; }
    }
}
