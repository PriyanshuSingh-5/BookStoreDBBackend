using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerWishListRL : ICustomerWishListRL
    {
        private readonly IConfiguration config;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string InsertBookToWishListSQL = "InsertBookToWishList";
        readonly string RemoveBookFromWishListSQL = "RemoveBookFromWishList";
        readonly string GetWishListSQL = "GetWishList";

        public CustomerWishListRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = this.config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString;
                //+ "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public ICollection<CustomerWishList> GetWishList(string CustomerID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(GetWishListSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                SqlDataReader rd = cmd.ExecuteReader();
                ICollection<CustomerWishList> WishList = new List<CustomerWishList>();
                CustomerWishList Book;
                while (rd.Read())
                {
                    Book = new CustomerWishList();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    WishList.Add(Book);
                }
                return WishList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        public ICollection<CustomerWishList> AddBookToWishList(string CustomerID, long BookID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InsertBookToWishListSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("BookID", BookID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(3))
                {
                    throw new Exception("Book already in wish list");
                }
                else if (result != null && result.Equals(2))
                {
                    throw new Exception("Book don't exist");
                }
                ICollection<CustomerWishList> WishList = new List<CustomerWishList>();
                CustomerWishList Book;
                while (rd.Read())
                {
                    Book = new CustomerWishList();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    WishList.Add(Book);
                }
                return WishList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public ICollection<CustomerWishList> RemoveBookFromWishList(string CustomerID, long BookID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(RemoveBookFromWishListSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("BookID", BookID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(3))
                {
                    throw new Exception("Book is not in wish list");
                }
                else if (result != null && result.Equals(2))
                {
                    throw new Exception("Book don't exist");
                }
                ICollection<CustomerWishList> WishList = new List<CustomerWishList>();
                CustomerWishList Book;
                while (rd.Read())
                {
                    Book = new CustomerWishList();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    WishList.Add(Book);
                }
                return WishList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
