using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using CPlatform;
using CPlatform.Ioc;

namespace DapperCore
{
    public interface IRepository<T> : IDependency where T : class, new()
    {
        #region OR
        /// <summary>
        /// 通过主键查找对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<T> Get(long Id);
        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> Get(IPredicate predicate);
        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList(IPredicate predicate, IList<ISort> sort = null);
        /// <summary>
        /// 执行sql查询脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        //IEnumerable<K> GetList<K>(string sql, object param);
        /// <summary>
        /// 查询计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> Count(IPredicate predicate);
        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <param name="sort"></param>
        ///// <param name="page"></param>
        ///// <param name="pageSize"></param>
        ///// <returns></returns>
        //[Obsolete("请使用新的分页方法,IEnumerable<T> GetPage(IPredicate predicate, IList<ISort> sort, int page, int pageSize, out long totalCount);")]
        //Task<IEnumerable<T>> GetPage(IPredicate predicate, IList<ISort> sort = null, int page = 0, int pageSize = 15);
        /// <summary>
        /// 分页查询,默认按ID倒序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPage(IPredicate predicate, int pageIndex, int pageSize, out long totalCount);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort">sort为空时默认按ID倒序</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPage(IPredicate predicate, IList<ISort> sort, int pageIndex, int pageSize, out long totalCount);
        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <returns>返回实体主键(单主键为object,联合主键返回为字典对象)</returns>
        /// <param name="entity"></param>
        Task<long> Add(T entity);
        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <returns>返回实体主键(单主键为object,联合主键返回为字典对象)</returns>
        /// <param name="entity"></param>
        Task<long> Add(T entity, IDbTransaction tran);
        /// <summary>
        /// 添加实体列表
        /// </summary>
        /// <param name="entities"></param>
        Task Add(IEnumerable<T> entities);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        Task<bool> Update(T entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        Task<bool> Delete(T entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        Task<bool> Delete(long ID);
        /// <summary>
        /// 按条件删除简陋
        /// </summary>
        /// <returns></returns>
        Task<bool> Delete(IPredicate predicate);
        /// <summary>
        /// 删除实体列表(返回操作结果列表)
        /// </summary>
        /// <param name="entities"></param>
        Task<IEnumerable<bool>> Delete(IEnumerable<T> entities);

        #endregion

    }
}
