using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerCartBL : ICustomerCartBL
    {
        ICustomerCartRL customerCartRL;

        public CustomerCartBL(ICustomerCartRL customerCartRL)
        {
            this.customerCartRL = customerCartRL;
        }

        public ICollection<CustomerCart> AddBookToCart(string CustomerID, long BookID) 
        {
            try
            {
                var result =  customerCartRL.AddBookToCart(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerCart> GetCart(string CustomerID)
        {
            try
            {
                var result = customerCartRL.GetCart(CustomerID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerCart> RemoveBookFromCart(string CustomerID, long BookID)
        {
            try
            {
                ICollection<CustomerCart> result = customerCartRL.RemoveBookFromCart(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerCart> UpdateBookInCart(string CustomerID, long BookID, long Quantity)
        {
            try
            {
                ICollection<CustomerCart> result;
                result = customerCartRL.UpdateBookInCart(CustomerID, BookID, Quantity);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
