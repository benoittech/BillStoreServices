using BillStoreService.Model;
using DataService.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.BusinessServices;
using System.Threading.Tasks;

namespace Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[Users]")]
    public class UserController
    {
        private readonly UserService userService;

        public UserController(IConfiguration configuration)
        {
            userService = new UserService(configuration);
        }

        // GET api/values/5
        [HttpGet("{countryCode}/{phoneNumber}")]
        public async Task<UserInfo> Get(string countryCode, string phoneNumber)
        {
            return await userService.GetUser(phoneNumber, countryCode);
        }

        // POST api/values
        [HttpPost]
        public async Task<UserInfo> Post([FromBody]UserInfo user)
        {
            return await userService.Adduser(user);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<UserInfo> Put([FromBody]UserInfo user)
        {
            return await userService.UpdateUser(user);
        }

        // DELETE api/values/5
        [HttpDelete("{countryCode}/{phoneNumber}")]
        public async Task Delete(string countryCode, string phoneNumber)
        {
           await userService.DeleteUser(countryCode, phoneNumber);
        }

    }
}
