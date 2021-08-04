using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers.CustomerControlers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize(Roles = Role.Customer)]
    public class OrderController : ControllerBase
    {
        ICustomerOrderBL customerOrderBL;

        public OrderController(ICustomerOrderBL customerOrderBL)
        {
            this.customerOrderBL = customerOrderBL;
        }

        [HttpPost("{AddressID}")]
        public IActionResult PlaceOrder(long AddressID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    CustomerOrder Order = customerOrderBL.PlaceOrder(CustomerID, AddressID);
                    if (Order != null)
                    {
                        return Ok(new { success = true, Message = "order placed successfully", Order });
                    }
                }
                return BadRequest(new { success = false, Message = "order place Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
