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
    public class CartController : ControllerBase
    {
        ICustomerCartBL customerCartBL;

        public CartController(ICustomerCartBL customerCartBL)
        {
            this.customerCartBL = customerCartBL;
        }
        [HttpGet]
        public IActionResult GetCart()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    ICollection<CustomerCart> cart = customerCartBL.GetCart(CustomerID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, cart });
                    }
                }
                return BadRequest(new { success = false, Message = "cart empty" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("{BookID}")]
        public IActionResult AddBookToCart(long BookID) 
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    ICollection<CustomerCart> cart = customerCartBL.AddBookToCart(CustomerID, BookID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book added to cart", cart });
                    }
                }
                return BadRequest(new { success = false, Message = "book add to cart Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpDelete("{BookID}")]
        public IActionResult RemoveBookFromCart(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    ICollection<CustomerCart> cart = customerCartBL.RemoveBookFromCart(CustomerID, BookID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book removed from cart", cart });
                    } 
                }
                return BadRequest(new { success = false, Message = "book removed from cart Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpPut("{BookID}/{Quantity}")]
        public IActionResult UpdateBookInCart(long BookID, long Quantity)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null && Quantity >= 0)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    ICollection<CustomerCart> cart = customerCartBL.UpdateBookInCart(CustomerID, BookID, Quantity);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book updated in cart", cart });
                    }
                }
                return BadRequest(new { success = false, Message = "book update Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
