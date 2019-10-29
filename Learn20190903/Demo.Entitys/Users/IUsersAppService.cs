using System;
using System.Threading.Tasks;
using CPlatform.Ioc;
using CPlatform.Msg.Res;
using CPlatform.Msg.Req;
using Demo.IApplication.Users.DTO;
using System.Collections.Generic;
using Demo.Domain.Shared.Paging;

namespace Demo.IApplication.Users
{
    public interface IUsersAppService : IDependency
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="addUserInput"></param>
        /// <returns></returns>
        Task<long> AddUser(AddUserInput addUserInput);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="updateUserInput"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(UpdateUserInput updateUserInput);

        /// <summary>
        ///根据用户Id获取用户明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>返回用户明细</returns>
        Task<QueryUserOutput> GetUser(long Id);

        /// <summary>
        /// 根据分页参数 分页获取用户集合
        /// </summary>
        /// <param name="PageRequestPackage"></param>
        /// <returns>分页返回用户集合</returns>
        Task<PageOutput<QueryUserOutput>> GetPageUsers(PageInput<QueryUserInput> queryUserInput);
    }
}
