using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CustomerCart
    {
        public long CartID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string CustomerID { get; set; }
        public string BookImage { get; set; }
        public long BookID { get; set; }
        public int BookPrice { get; set; }
        public int TotalCost { get; set; }
        public int Count { get; set; }
    }
}
