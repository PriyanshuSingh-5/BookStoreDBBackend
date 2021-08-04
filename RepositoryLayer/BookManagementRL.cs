using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AdminInterfaces;

namespace RepositoryLayer
{
    public class BookManagementRL : IBookManagementRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        ICollection<ResponseBookDB> Books;
        ResponseBookDB Book;
        readonly string InserBookRecordSQL = "InserBookRecord";
        readonly string GetBookRecordSQL = "GetCustomerBookRecord";
        readonly string DeleteBookRecordSQL = "DeleteBookRecord";
        readonly string UpdateBookRecordSQL = "UpdateBookRecord";

        public BookManagementRL(IConfiguration config)
        {
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString;
                //+ "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }
        public ResponseBookDB AddBook(RequestBookDB book)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InserBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("BookName", book.BookName);
                cmd.Parameters.AddWithValue("BookDiscription", book.BookDiscription);
                cmd.Parameters.AddWithValue("BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Book = new ResponseBookDB();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.Quantity = rd["BookQuantity"] == DBNull.Value ? default : rd.GetInt32("BookQuantity");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                    Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                    Book.InStock = rd["InStock"] != DBNull.Value && rd.GetBoolean("InStock");
                }
                return Book;
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

        public bool DeleteBook(long bookID)
        {
            try
            {
                ICollection<ResponseBook> Books = new List<ResponseBook>();
                connection.Open();
                SqlCommand cmd = new SqlCommand(DeleteBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("BookID", bookID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                var result = returnParameter.Value;
                if (result.Equals(2))
                {
                    throw new Exception("book don't exist");
                }
                else if (result.Equals(3))
                {
                    throw new Exception("book already deleted");
                }
                return true;
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

        public ICollection<ResponseBookDB> GetCustomerBooks(string CustomerID)
        {
            try
            {
                Books = new List<ResponseBookDB>();
                connection.Open();
                SqlCommand cmd = new SqlCommand(GetBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Book = new ResponseBookDB();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                    Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                    Book.InStock = rd["InStock"] == DBNull.Value ? default : rd.GetBoolean("InStock");
                    Book.InCart = rd["InCart"] == DBNull.Value ? default : rd.GetBoolean("InCart");
                    Books.Add(Book);
                }
                return Books;
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

        public ResponseBookDB UpdateBook(long BookID, RequestBook book)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(UpdateBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("BookID", BookID);
                cmd.Parameters.AddWithValue("BookName", book.BookName);
                cmd.Parameters.AddWithValue("BookDiscription", book.BookDiscription);
                cmd.Parameters.AddWithValue("BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                    throw new Exception("book don't exist");
                if (rd.Read())
                {
                    Book = new ResponseBookDB
                    {
                        BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID"),
                        BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription"),
                        BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice"),
                        BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName"),
                        AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName"),
                        BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage"),
                        InStock = rd["InStock"] != DBNull.Value && rd.GetBoolean("InStock")
                    };
                }
                return Book;
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
