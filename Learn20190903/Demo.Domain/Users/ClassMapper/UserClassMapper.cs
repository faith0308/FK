using System;
using System.Collections.Generic;
using System.Text;
using DapperExtensions.Mapper;
using Demo.Domain.Users.AggregateRoot;

namespace Demo.Domain.Users.ClassMapper
{
    public class UserClassMapper : ClassMapper<User>
    {
        public UserClassMapper()
        {
            Table("users");
            AutoMap();
        }
    }
}
