using System.Net;

namespace insuranceLeadApi.Controllers.Model
{
    public class PolicyHolderRestModel<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T data { get; set; }
        public List<String> Messages { get; set; }

        public PolicyHolderRestModel() {
          StatusCode = HttpStatusCode.OK;
          data = default;
          Messages = [];
        }  
    }
}
