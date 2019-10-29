using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExtensions
{
    /// <summary>
    /// 比较运算符
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// 等于
        /// Equal to  
        /// </summary>
        Eq,

        /// <summary>
        /// 大于
        /// Greater than  
        /// </summary>
        Gt,

        /// <summary>
        /// 大于或等于
        /// Greater than or equal to  
        /// </summary>
        Ge,

        /// <summary>
        /// 小于
        /// Less than
        /// </summary>
        Lt,

        /// <summary>
        /// 小于或等于
        /// Less than or equal to
        /// </summary>
        Le,

        /// <summary>
        /// 模糊查询 你可以在值中使用%来进行wilcard搜索
        /// Like (You can use % in the value to do wilcard searching)
        /// </summary>
        Like
    }

    /// <summary>
    /// Operator to use when joining predicates in a PredicateGroup.
    /// </summary>
    public enum GroupOperator
    {
        And,
        Or
    }
}
