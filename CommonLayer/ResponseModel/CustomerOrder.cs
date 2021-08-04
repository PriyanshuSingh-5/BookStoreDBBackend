using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;

namespace CommonLayer.ResponseModel
{
    public class CustomerOrder
    {
        public long OrderID { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerID { get; set; }
        public int TotalCost { get; set; }
        public string Name { get; set; }
        public long AddressID { get; set; }
        public int Pincode { get; set; }
        public long PhoneNumber { get; set; }
        public string Locality { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Landmark { get; set; }
        public string AddressType { get; set; }
    }
}
