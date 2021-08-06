using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers.CustomerControlers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        //Declare private object for logger object
       // private readonly ILoggerService _logger;
        private IReviewBL reviewBL;
        public ReviewController(IReviewBL reviewBL )
        {
            this.reviewBL = reviewBL;
            //this._logger = logger;
        }

        [HttpPost]
        public IActionResult AddReview(int BookId, ReviewRequest review)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(CustomerID => CustomerID.Type.Equals("CustomerID", StringComparison.InvariantCultureIgnoreCase));
                int CustomerID = Convert.ToInt32(idClaim.Value);
                var data = this.reviewBL.AddReview(CustomerID, BookId, review);
                if (data != null)
                {
                   // _logger.LogInfo($"Review Added To Book Successfully {UserId}"); // Logger Info
                    return this.Ok(new { status = "True", message = "Review Added To Book Successfully", data });
                }
                else
                {
                    //_logger.LogError($"Failed To Add Review {UserId}"); // Logger Error   
                    return this.BadRequest(new { status = "False", message = "Failed To Add Review" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult GetListOfReview()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(CustomerID => CustomerID.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int CustomerID = Convert.ToInt32(idClaim.Value);
                var data = reviewBL.GetListOfReview(CustomerID);
                if (data != null)
                {
                    //_logger.LogInfo($"List of Review Fetched Successfully {UserId}"); // Logger Info
                    return Ok(new { success = true, message = "List of Review Fetched Successfully", data });
                }
                else
                {
                    //_logger.LogError($"List of Review Fetched Failed {UserId}"); // Logger Error   
                    return NotFound(new { success = true, message = "List of Review Fetched Field" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}



