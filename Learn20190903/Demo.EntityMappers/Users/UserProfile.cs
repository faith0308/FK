using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Demo.Domain.Users.AggregateRoot;
using Demo.IApplication.Users.DTO;

namespace Demo.Application.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, QueryUserOutput>();
            CreateMap<QueryUserInput, User>();
        }
    }
}
