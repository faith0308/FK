using System;
using System.Data;
using System.Data.Common;
using dbPF = System.Data.Common;

namespace DapperCore
{
    public class UnitOfWorkDefaultContext : IUnitOfWorkDefaultContext
    {
        private string id = Guid.NewGuid().ToString("N");
        public string Id { get { return id; } }

        public IDbConnection Conn { get; private set; }

        private bool _committed = false;
        public bool Committed
        {
            get { return _committed; }
            private set { _committed = value; }
        }

        private IDbTransaction _dbTrans = null;
        public IDbTransaction Tran
        {
            get { return _dbTrans; }
            private set { _dbTrans = value; }
        }
        
        /// <summary>
        /// 类构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public UnitOfWorkDefaultContext(ConnectionStringSettings connectionString)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(connectionString.ProviderName);

            this.Conn = dbfactory.CreateConnection();
            if (Conn != null)
            {
                this.Conn.ConnectionString = connectionString.ConnectionString;
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            if (this._dbTrans == null)
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    this.Conn.Open();
                }
                this._dbTrans = Conn.BeginTransaction();
                this._committed = false;
            }
        }

        /// <summary>
        /// 锁
        /// </summary>
        static object lockObj = new object();

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            if (this._committed) return;

            lock (lockObj)
            {
                if (this._dbTrans != null)
                {
                    this._dbTrans.Commit();
                    this._committed = true;
                    this._dbTrans = null;
                }
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            if (this._committed) return;
            lock (lockObj)
            {
                if (this._dbTrans != null)
                {
                    this._dbTrans.Rollback();
                    this._committed = true;
                    this._dbTrans = null;
                }
            }
        }

        bool disposed = false;

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;

                if (!this._committed)
                {
                    if (this._dbTrans != null)
                    {
                        try
                        {
                            this._dbTrans.Commit();
                            this._committed = true;
                        }
                        catch
                        {
                            this._dbTrans.Rollback();
                        }
                    }
                }
                this._dbTrans = null;
                this.Conn.Close();
                this.Conn.Dispose();
            }
        }
    }
}
