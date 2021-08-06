using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class ReviewListBookResponse
    {
        public long BookID { get; set; }
        public long CustomerIdentity { get; set; }
        public string BookName { get; set; }
        public string BookDiscription { get; set; }
        public string BookImage { get; set; }
        public int BookPrice { get; set; }
        public int BookQuantity { get; set; }
       
        public int Review { get; set; }
        public string Feedback { get; set; }
    }
}
