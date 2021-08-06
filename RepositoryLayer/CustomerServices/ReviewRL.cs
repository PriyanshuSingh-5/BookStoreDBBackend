using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.CustomerServices
{
    public class ReviewRL : IReviewRL
    {
        
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public ReviewRL(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }
        public ReviewRequest AddReview(int bookId, int UserId, ReviewRequest review)
        {
            try
            {
                ReviewRequest reviewRequest = new ReviewRequest();
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_ReviewBackToBook", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@Review", review.Review);
                    cmd.Parameters.AddWithValue("@Feedback", review.Feedback);
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                };
                return reviewRequest;
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
                List<ReviewListBookResponse> reviewList = null;
                SQLConnection();
                reviewList = new List<ReviewListBookResponse>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOfReview", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    reviewList = ListBookResponseModel(dataReader);
                };
                return reviewList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<ReviewListBookResponse> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<ReviewListBookResponse> bookList = new List<ReviewListBookResponse>();
                ReviewListBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new ReviewListBookResponse
                    {
                        BookID = Convert.ToInt64(dataReader["BookId"]),
                        CustomerIdentity = Convert.ToInt64(dataReader["UserId"]),
                        BookName = dataReader["BookName"].ToString(),
                        BookDiscription = dataReader["BookDiscription"].ToString(),
                        BookImage = dataReader["BookImage"].ToString(),
                        BookPrice = Convert.ToInt32(dataReader["BookPrice"]),
                        BookQuantity = Convert.ToInt32(dataReader["BookQuantity"]),
                        
                        Review = Convert.ToInt32(dataReader["Review"]),
                        Feedback = dataReader["Feedback"].ToString()
                    };
                    bookList.Add(responseData);
                }
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}



