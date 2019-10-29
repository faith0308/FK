using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperCore
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 事务处理
        /// </summary>
        IDbTransaction Tran { get; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        IDbConnection Conn { get; }

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 是否提交
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}
