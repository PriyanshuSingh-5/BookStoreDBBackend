using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerWishListBL
    {
        public ICollection<CustomerWishList> AddBookToWishList(string CustomerID, long BookID);
        ICollection<CustomerWishList> RemoveBookFromWishList(string customerID, long bookID);
        public ICollection<CustomerWishList> GetWishList(string CustomerID);
    }
}
