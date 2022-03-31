using DevTest.Helpers.AUTH;
using DevTest.Services.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagement _userManagement;
        public UserManagementController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }


        // [AppAuthorize(true)]
        [HttpGet]
        [Route("getPersons/{orgId}")]
        public ActionResult GetPersons(int orgId)
        {
            try
            {
                var result = _userManagement.GetPeople(orgId);


                return Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }



        }





    }
}
