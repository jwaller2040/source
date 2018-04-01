using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
   // [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        // GET: api/User
        [HttpGet]
        public string Get()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name; ;
        }

        //public string Get(string id)
        //{
        //    return "value";
        //}



        [HttpGet("{id}", Name = "GetUser")]
        public async Task<string> GetUser(string id)
        {


            Task<string> userUpdate = Task.Run(() =>
            {
                try
                {
                    UserCommand uc = new UserCommand(id);
                    uc.ExecuteCommand();
                    return uc.UserProfile;
                }
                catch (Exception ex)
                {

                    return ex.ToString();

                }

            });

            var returnValue = await userUpdate;

            return returnValue;
        }


        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
