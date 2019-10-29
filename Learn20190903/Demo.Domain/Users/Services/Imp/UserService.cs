using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Domain.Users.AggregateRoot;
using Demo.Domain.Users.Repositories;

namespace Demo.Domain.Users.Services.Imp
{
    /// <summary>
    /// 用户操作逻辑 实现
    /// </summary>
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
