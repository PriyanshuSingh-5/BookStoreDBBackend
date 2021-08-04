using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerAddressBL : ICustomerAddressBL
    {
        ICustomerAddressRL customerAddressRL;

        public CustomerAddressBL(ICustomerAddressRL customerAddressRL)
        {
            this.customerAddressRL = customerAddressRL;
        }

        public CustomerAddressResponse AddCustomerAddress(CustomerAddress address)
        {
            try
            {
                return customerAddressRL.AddCustomerAddress(address); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomerAddress(string customerID, long addressID)
        {
            try
            {
                return customerAddressRL.DeleteCustomerAddress(customerID, addressID); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerAddressResponse> GetAllCustomerAddress(string customerID)
        {
            try
            {
                return customerAddressRL.GetAllCustomerAddress(customerID); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomerAddressResponse UpdateCustomerAddress(CustomerAddress address)
        {
            try
            {
                return customerAddressRL.UpdateCustomerAddress(address); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
