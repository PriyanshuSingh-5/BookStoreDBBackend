using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerCartRL
    {
        public ICollection<CustomerCart> AddBookToCart(string CustomerID, long BookID);
        ICollection<CustomerCart> RemoveBookFromCart(string customerID, long bookID);
        ICollection<CustomerCart> GetCart(string customerID);
        ICollection<CustomerCart> UpdateBookInCart(string customerID, long bookID, long Quantity);
    }
}
