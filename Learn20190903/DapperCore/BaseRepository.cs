using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCore
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 事务
        /// </summary>
        private IDbTransaction Tran;
        /// <summary>
        /// 数据库连接
        /// </summary>
        private IDbConnection Conn;
        /// <summary>
        /// 工作单元
        /// </summary>
        private IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            this.Tran = unitOfWork.Tran;
            this.Conn = unitOfWork.Conn;
            _unitOfWork = unitOfWork;
        }

        #region ORM 形式

        /// <summary>
        /// 通过主键查找对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<T> Get(long Id)
        {
            return Task.FromResult(Conn.Get<T>(Id, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T> Get(IPredicate predicate)
        {
            return Task.FromResult(Conn.GetList<T>(predicate, transaction: _unitOfWork.Tran).FirstOrDefault());
        }

        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetList(IPredicate predicate, IList<ISort> sort = null)
        {
            return Task.FromResult(Conn.GetList<T>(predicate, sort, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 查询计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<long> Count(IPredicate predicate)
        {
            return Task.FromResult(Convert.ToInt64(Conn.Count<T>(predicate)));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort">传空值默认将按ID倒序</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetPage(IPredicate predicate, IList<ISort> sort = null, int page = 0, int pageSize = 15)
        {
            //if (sort == null || !sort.Any())
            //{
            //    sort = new List<ISort> { Predicates.Sort<T>(s => s.Id, false) };
            //}
            return Task.FromResult(Conn.GetPage<T>(predicate, sort, page - 1, pageSize, _unitOfWork.Tran));
        }

        /// <summary>
        /// 分页查询,默认将按ID倒序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// 
        /// <returns></returns>
        public Task<IEnumerable<T>> GetPage(IPredicate predicate, int page, int pageSize, out long totalCount)
        {
            totalCount = Conn.Count<T>(predicate);

            return this.GetPage(predicate, null, page - 1, pageSize, out totalCount);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort">传空值默认将按ID倒序</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// 
        /// <returns></returns>
        public Task<IEnumerable<T>> GetPage(IPredicate predicate, IList<ISort> sort, int page, int pageSize, out long totalCount)
        {
            //if (sort == null || !sort.Any())
            //{
            //    sort = new List<ISort> { Predicates.Sort<T>(s => s.Id, false) };
            //}
            totalCount = Conn.Count<T>(predicate);

            return Task.FromResult(Conn.GetPage<T>(predicate, sort, page - 1, pageSize, _unitOfWork.Tran));
        }

        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <returns>返回实体主键(单主键为object,联合主键返回为字典对象)</returns>
        /// <param name="entity"></param>
        public Task<long> Add(T entity)
        {
            return Task.FromResult(Convert.ToInt64(Conn.Insert(entity, transaction: _unitOfWork.Tran)));
        }

        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <returns>返回实体主键(单主键为object,联合主键返回为字典对象)</returns>
        /// <param name="entity"></param>
        public Task<long> Add(T entity, IDbTransaction tran)
        {
            return Task.FromResult(Convert.ToInt64(Conn.Insert(entity, transaction: tran)));
        }

        /// <summary>
        /// 添加实体列表
        /// </summary>
        /// <param name="entities"></param>
        public Task Add(IEnumerable<T> entities)
        {
            Conn.Insert(entities, transaction: _unitOfWork.Tran);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public Task<bool> Update(T entity)
        {
            return Task.FromResult(Conn.Update(entity, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public Task<bool> Delete(T entity)
        {
            return Task.FromResult(Conn.Delete<T>(entity, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public Task<bool> Delete(long ID)
        {
            return Task.FromResult(Conn.Delete<T>(new { ID = ID }, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public Task<bool> Delete(IPredicate predicate)
        {
            return Task.FromResult(Conn.Delete<T>(predicate, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 删除实体列表(返回操作结果列表)
        /// </summary>
        /// <param name="entities"></param>
        public Task<IEnumerable<bool>> Delete(IEnumerable<T> entities)
        {
            List<bool> deletes = new List<bool>();
            foreach (var item in entities)
            {
                deletes.Add(Conn.Delete(item, transaction: _unitOfWork.Tran));
            }

            return Task.FromResult(deletes.AsEnumerable());
        }

        #endregion

        #region ado.net 形式

        /// <summary>
        /// 执行sql查询脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<T> Get(string sql, object param = null)
        {
            return Task.FromResult(Conn.QueryFirstOrDefault<T>(sql, param, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 执行sql查询脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<K> Get<K>(string sql, object param = null)
        {
            return Task.FromResult(Conn.QueryFirstOrDefault<K>(sql, param, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 查询计数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<long> Count(string sql, object param = null)
        {
            return Task.FromResult(Convert.ToInt64(Conn.ExecuteScalar(sql, param, transaction: _unitOfWork.Tran)));
        }

        /// <summary>
        /// 执行sql查询脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetList(string sql, object param = null)
        {
            return Task.FromResult(Conn.Query<T>(sql, param, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 执行sql查询脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<IEnumerable<K>> GetList<K>(string sql, object param = null)
        {
            return Task.FromResult(Conn.Query<K>(sql, param, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 执行sql查询脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> GetDynamicList(string sql, object param = null)
        {
            return Task.FromResult(Conn.Query(sql, param, transaction: _unitOfWork.Tran));
        }

        /// <summary>
        /// 执行sql脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<bool> Execute(string sql, object param = null)
        {
            return Task.FromResult(Conn.Execute(sql, param, transaction: _unitOfWork.Tran) > 0);
        }

        /// <summary>
        /// 执行sql脚本返回查询结果
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<K> ExecuteScalar<K>(string sql, object param = null)
        {
            return Task.FromResult(Conn.ExecuteScalar<K>(sql, param, transaction: _unitOfWork.Tran));
        }

        #endregion
    }
}
