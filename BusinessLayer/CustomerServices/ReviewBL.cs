using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.CustomerServices
{
    public class ReviewBL : IReviewBL
    {
        IReviewRL reviewRL;
        public ReviewBL(IReviewRL reviewRL)
        {
            this.reviewRL = reviewRL;
        }

        public ReviewRequest AddReview(int bookId, int UserId, ReviewRequest review)
        {
            try
            {
                return this.reviewRL.AddReview(bookId, UserId, review);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ReviewListBookResponse> GetListOfReview(int UserId)
        {
            try
            {
                return this.reviewRL.GetListOfReview(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}



