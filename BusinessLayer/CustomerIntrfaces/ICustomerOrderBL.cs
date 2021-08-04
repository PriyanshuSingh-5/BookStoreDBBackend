using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerOrderBL
    {
        public CustomerOrder PlaceOrder(string CustomerID, long AddressID);
    }
}
