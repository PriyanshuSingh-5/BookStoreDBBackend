using CommonLayer.BaseModel;

namespace CommonLayer.ResponseModel
{
    public class ResponseBookDB : BookModel
    {
        public long BookID { get; set; }
        public string BookImage { get; set; }
        public bool InStock { get; set; }
        public bool InCart { get; set; }
    }
}
