using System.Net;

namespace CustomerManagementApp.Models
{
    public class ServiceResponse
    {
        public IEnumerable<ViewCustomerModel>? Customers { get; set; }

        public int TotalItems { get; set; }

        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
