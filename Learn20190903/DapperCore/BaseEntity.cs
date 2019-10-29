using System;
using System.Collections.Generic;
using System.Text;

namespace DapperCore
{
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// 各表的自增主键
        /// </summary>
        public virtual long Id { get; set; }
    }
}
