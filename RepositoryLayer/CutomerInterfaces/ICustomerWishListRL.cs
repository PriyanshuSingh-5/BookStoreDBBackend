using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerWishListRL
    {
        public ICollection<CustomerWishList> AddBookToWishList(string CustomerID, long BookID);
        ICollection<CustomerWishList> RemoveBookFromWishList(string customerID, long bookID);
        public ICollection<CustomerWishList> GetWishList(string CustomerID);
    }
}
