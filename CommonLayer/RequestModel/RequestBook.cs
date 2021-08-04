using CommonLayer.BaseModel;
using Microsoft.AspNetCore.Http;

namespace CommonLayer.RequestModel
{
    public class RequestBook : BookModel
    {
        public IFormFile BookImage { get; set; }
    }
}
