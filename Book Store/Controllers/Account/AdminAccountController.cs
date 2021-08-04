using System;
using BusinessLayer.AdminInterfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Book_Store.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AdminAccountController : ControllerBase
    {
        IAdminAccountBL adminAccountBL;
        private readonly IConfiguration config;

        public AdminAccountController(IAdminAccountBL adminAccountBL, IConfiguration config)
        {
            this.adminAccountBL = adminAccountBL;
            this.config = config;
        }

        [HttpPost("Login")]
        public IActionResult LoginAdmin(LoginAdminAccount loginAdminAccount)
        {
            try
            {
                if (loginAdminAccount != null)
                { 
                    AdminAccount result = adminAccountBL.LoginAdmin(loginAdminAccount); ;
                    if (result != null)
                    {
                        return Ok(new { success = true, Message = "Admin login successfull", result });
                    }
                }
                return BadRequest(new { success = false, Message = "Admin login unsuccessfull" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

    }
}
