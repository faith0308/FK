using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Domain.Shared.Paging;
using Demo.IApplication.Users;
using Demo.IApplication.Users.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExemple.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1", Deprecated = false)]
    [Route("api/v{api-version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IUsersAppService _usersAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usersAppService"></param>
        public ValuesController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        /// <summary>
        /// 分页获取用户数据
        /// </summary>
        /// GET api/values
        /// <param name="pageInput">输入参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<QueryUserOutput>> Get(PageInput<QueryUserInput> pageInput)
        {
            var queryWhere = new PageInput<QueryUserInput>
            {
                PageIndex = 1,
                PageSize = 10,
                Query = new QueryUserInput()
            };
            var users = await _usersAppService.GetPageUsers(queryWhere);
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
