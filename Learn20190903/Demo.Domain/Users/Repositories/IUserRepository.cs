using DapperCore;
using Demo.Domain.Users.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CPlatform.Msg.Req;
using CPlatform.Msg.Res;

namespace Demo.Domain.Users.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        
    }
}
