using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerOrderRL
    {
        public CustomerOrder PlaceOrder(string CustomerID, long AddressID);
    }
}
