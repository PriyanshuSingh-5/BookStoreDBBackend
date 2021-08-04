using System;
using System.Collections.Generic;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerWishListBL : ICustomerWishListBL
    {
        ICustomerWishListRL customerWishListRL;

        public CustomerWishListBL(ICustomerWishListRL customerWishListRL)
        {
            this.customerWishListRL = customerWishListRL;
        }

        public ICollection<CustomerWishList> GetWishList(string CustomerID)
        {
            try
            {
                var result = customerWishListRL.GetWishList(CustomerID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ICollection<CustomerWishList> AddBookToWishList(string CustomerID, long BookID)
        {
            try
            {
                var result = customerWishListRL.AddBookToWishList(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerWishList> RemoveBookFromWishList(string CustomerID, long BookID)
        {
            try
            {
                var result = customerWishListRL.RemoveBookFromWishList(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
