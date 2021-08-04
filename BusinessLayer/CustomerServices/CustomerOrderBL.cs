using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using BusinessLayer.MSMQ;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerOrderBL : ICustomerOrderBL
    {
        ICustomerOrderRL CustomerOrderRL;
        MSMQService MSMQ;

        public CustomerOrderBL(ICustomerOrderRL customerOrderRL, IConfiguration config)
        {
            CustomerOrderRL = customerOrderRL;
            MSMQ = new MSMQService(config);
        }
        public CustomerOrder PlaceOrder(string CustomerID, long AddressID)
        {
            try
            {
                var result =  CustomerOrderRL.PlaceOrder(CustomerID, AddressID);
                MSMQ.SendOrderEmail(result);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
