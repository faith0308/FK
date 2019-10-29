using CPlatform.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.Domain.Users.AggregateRoot;
using System.Threading.Tasks;
using CPlatform.Msg.Req;
using CPlatform.Msg.Res;

namespace Demo.Domain.Users.Services
{
    /// <summary>
    /// 用户操作逻辑 定义
    /// </summary>
    public interface IUserService : IDependency
    {
    }
}
