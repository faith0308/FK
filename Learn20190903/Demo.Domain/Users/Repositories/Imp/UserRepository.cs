using System;
using System.Collections.Generic;
using System.Text;
using DapperCore;
using Demo.Domain.Users.AggregateRoot;

namespace Demo.Domain.Users.Repositories.Imp
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWorkDefaultContext unitOfWork) : base(unitOfWork)
        {

        }
    }
}
