namespace CustomerManagementApp.Models
{
    public class ServiceResponse
    {
        public IEnumerable<ViewCustomerModel>? Customers { get; set; }

        public int TotalItems { get; set; }
    }
}
