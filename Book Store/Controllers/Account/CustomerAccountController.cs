using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Book_Store.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountBL CustomerAccountBL;

        public CustomerAccountController( ICustomerAccountBL customerAccountBL)
        {
          //  this.config = config;
            CustomerAccountBL = customerAccountBL;
        }

        [HttpPost("Register")]
        public IActionResult RegisterCustomer(RegisterCustomerAccount Customer)
        {
            if (Customer == null)
            {
                return BadRequest("Customer is null.");
            }
            try
            {
                CustomerAccount result = CustomerAccountBL.RegisterCustomer(Customer);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "Customer Registration Successful", Customer = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Customer Registration Unsuccessful" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult LoginUser(LoginCustomerAccount Customer)
        {
            if (Customer == null)
            {
                return BadRequest("Customer is null.");
            }
            try
            {
                CustomerAccount result = CustomerAccountBL.LoginCustomer(Customer);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "Customer login Successful", Customer = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Customer login Unsuccessful" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ResetForgottenPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                bool result = CustomerAccountBL.SendForgottenPasswordLink(forgetPasswordModel);

                if (result)
                {

                    return Ok(new { success = true, Message = "password reset link has been sent to your email id", email = forgetPasswordModel.Email });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "email id don't exist" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize(Roles = Role.Customer)]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                { 
                    IEnumerable<Claim> claims = identity.Claims;
                    var Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    resetPasswordModel.Email = Email;
                    bool result = CustomerAccountBL.ResetCustomerAccountPassword(resetPasswordModel);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "password changed successfully" });
                    }
                }
                return BadRequest(new { success = false, Message = "password change unsuccessfull" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

    }
}
