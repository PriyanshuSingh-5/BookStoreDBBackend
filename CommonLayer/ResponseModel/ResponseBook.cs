using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.BaseModel;
using Microsoft.AspNetCore.Http;

namespace CommonLayer.ResponseModel
{
    public class ResponseBook : BookModel
    {
        public long BookID { get; set; }
        public string BookImage { get; set; }
        public bool InStock { get; set; }
        public bool InCart { get; set; }
    }
}
