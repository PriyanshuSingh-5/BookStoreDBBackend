using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BusinessLayer;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers.AdminController
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManagementBL bookManagementBL;
        public BookController(IBookManagementBL bookManagementBL)
        {
            this.bookManagementBL = bookManagementBL;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult AddBook([FromForm]RequestBook Book)
        {
            if (Book == null)
            {
                return BadRequest("Book is null.");
            }
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    ResponseBookDB book = bookManagementBL.AddBook(Book);
                    if (book != null)
                    {
                        return Ok(new { success = true, Message = "book added", book });
                    }
                }
                return BadRequest(new { success = false, Message = "book adding Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize(Roles = Role.Customer)]
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;

                    ICollection<ResponseBookDB> books = bookManagementBL.GetCustomerBooks(CustomerID);
                    if (books != null)
                    {
                        return Ok(new { success = true, Message = "books fetched", books });
                    }
                }
                return BadRequest(new { success = false, Message = "books fetch Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{BookID}")]
        public IActionResult DeleteBook(long BookID) 
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    bool result = bookManagementBL.DeleteBook(BookID);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "book deleted" });
                    }
                }
                return BadRequest(new { success = false, Message = "book delete Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{BookID}")]
        public IActionResult UpdateBook(long BookID, RequestBook Book)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    ResponseBookDB book = bookManagementBL.UpdateBook(BookID, Book);
                    if (book != null)
                    {
                        return Ok(new { success = true, Message = "book updated", book });
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
