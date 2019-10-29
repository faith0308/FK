using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CPlatform.Enums;
using CPlatform.Msg.Req;
using CPlatform.Msg.Res;
using Demo.IApplication.Users;
using Demo.IApplication.Users.DTO;
using Demo.Domain.Users.Services;
using DapperCore;
using Demo.Domain.Users.Repositories;
using AutoMapper;
using Demo.Domain.Users.AggregateRoot;
using DapperExtensions;
using System.Linq;
using Demo.Domain.Shared.Paging;

namespace Demo.Application.Users
{
    public class UsersAppService : IUsersAppService
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public UsersAppService(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 添加用户数据
        /// </summary>
        /// <param name="addUserInput"></param>
        /// <returns></returns>
        public async Task<long> AddUser(AddUserInput addUserInput)
        {
            var entity = Mapper.Map<AddUserInput, User>(addUserInput);
            if (entity != null)
            {
                entity.CreateTime = DateTime.Now;
                entity.Status = (int)Status.normal;
            }
            return await _userRepository.Add(entity);
        }

        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public async Task<QueryUserOutput> GetUser(long Id)
        {
            var user = await _userRepository.Get(Id);

            return Mapper.Map<User, QueryUserOutput>(user);
        }

        /// <summary>
        /// 分页查询用户信息
        /// </summary>
        /// <param name="queryUserInput"></param>
        /// <returns></returns>
        public async Task<PageOutput<QueryUserOutput>> GetPageUsers(PageInput<QueryUserInput> queryUserInput)
        {
            long rows;
            IList<IPredicate> predlist = new List<IPredicate>();
            predlist.Add(Predicates.Field<User>(p => p.Status, Operator.Eq, (int)Status.normal));
            if (queryUserInput != null && queryUserInput.Query != null && !string.IsNullOrWhiteSpace(queryUserInput.Query.UserName))
            {
                predlist.Add(Predicates.Field<User>(p => p.UserName, Operator.Like, queryUserInput.Query.UserName + "%"));
            }
            IPredicateGroup group = Predicates.Group(GroupOperator.And, predlist.ToArray());

            IList<ISort> sorts = new List<ISort>();
            sorts.Add(new Sort { PropertyName = "Id", Ascending = false });
            
            var users = Mapper.Map<IEnumerable<User>, IEnumerable<QueryUserOutput>>(await _userRepository.GetPage(group, sorts, queryUserInput.PageIndex, queryUserInput.PageSize, out rows));


            return new PageOutput<QueryUserOutput> { Rows = rows, Result = users, PageIndex = queryUserInput.PageIndex };
        }

        /// <summary>
        /// 修改用户数据
        /// </summary>
        /// <param name="updateUserInput"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UpdateUserInput updateUserInput)
        {
            var result = false;
            var entity = Mapper.Map<UpdateUserInput, User>(updateUserInput);
            if (entity != null && entity.Id > 0)
            {
                result = await _userRepository.Update(entity);
            }
            return result;
        }
    }
}
