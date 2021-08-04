using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.AdminInterfaces;
using BusinessLayer.CloudinaryImageupload;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer;
using RepositoryLayer.AdminInterfaces;

namespace BusinessLayer
{
    public class BookManagementBL : IBookManagementBL
    {
        IBookManagementRL bookManagementRL;
        CloudinaryBL cloudinary;
        RequestBookDB requestBookDB;
        ResponseBook responseBook;

        public BookManagementBL(IBookManagementRL bookManagementRL, IConfiguration config)
        {
            cloudinary = new CloudinaryBL(config);
            this.bookManagementRL = bookManagementRL;
        }

        public ResponseBookDB AddBook(RequestBook book)
        {
            try
            {
                string ImageUrl = "";
                if (book.BookImage != null)
                {
                    ImageUrl = cloudinary.UploadImage(book.BookName, book.BookImage);
                }
                
                requestBookDB = new RequestBookDB
                {
                    BookName = book.BookName,
                    BookDiscription = book.BookDiscription == null ? "" : book.BookDiscription,
                    BookPrice = book.BookPrice,
                    BookImage = "",
                   // BookImage = book.ImageUrl,
                    AuthorName = book.AuthorName,
                    Quantity = book.Quantity
                };
                
                var result = bookManagementRL.AddBook(requestBookDB);
                responseBook = new ResponseBook
                {
                    BookID = result.BookID,
                    BookDiscription = result.BookDiscription,
                    BookImage = result.BookImage,
                    BookName = result.BookName,
                    BookPrice = result.BookPrice,
                    AuthorName = result.AuthorName,
                    InCart = result.InCart,
                    InStock = result.InStock,
                    Quantity = result.Quantity
                };
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(long bookID)
        {
            try
            {
                return bookManagementRL.DeleteBook(bookID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<ResponseBookDB> GetCustomerBooks(string CustomerID)
        {
            try
            {
                return bookManagementRL.GetCustomerBooks(CustomerID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseBookDB UpdateBook(long BookID, RequestBook book)
        {
            try
            {
                return bookManagementRL.UpdateBook(BookID, book);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
