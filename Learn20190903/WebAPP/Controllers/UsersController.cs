using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.IApplication.Users;
using Demo.IApplication.Users.DTO;
using CPlatform.Msg.Req;
using CPlatform.Msg.Res;
using Demo.Domain.Shared.Paging;

namespace WebAPP.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUsersAppService _usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        public async Task<IActionResult> Index()
        {
            var queryWhere = new PageInput<QueryUserInput>
            {
                PageIndex = 1,
                PageSize = 10,
                Query = new QueryUserInput()
            };
            var users = await _usersAppService.GetPageUsers(queryWhere);
            return View(users);
            //return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            var queryWhere = new PageInput<QueryUserInput>
            {
                PageIndex = 1,
                PageSize = 10,
                Query = new QueryUserInput()
            };
            var users = await _usersAppService.GetPageUsers(queryWhere);
            return Json(users);
        }
    }
}