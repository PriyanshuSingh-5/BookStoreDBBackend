using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer
{
    public interface IBookManagementBL
    {
        ResponseBookDB AddBook(RequestBook book);
        ICollection<ResponseBookDB> GetCustomerBooks(string CustomerID);
        bool DeleteBook(long bookID);
        ResponseBookDB UpdateBook(long BookID, RequestBook book);
    }
}
