using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerAddressRL
    {
        CustomerAddressResponse AddCustomerAddress(CustomerAddress address);
        bool DeleteCustomerAddress(string customerID, long addressID);
        ICollection<CustomerAddressResponse> GetAllCustomerAddress(string customerID);
        CustomerAddressResponse UpdateCustomerAddress(CustomerAddress address);
    }
}
