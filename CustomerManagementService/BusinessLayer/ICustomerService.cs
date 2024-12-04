using CustomerManagementService.Models.Resources;

namespace CustomerManagementService.BusinessLayer
{
    public interface ICustomerService
    {
        Task<ServiceResponse> GetAll(int pageNumber, int pageSize);

        Task<ViewCustomerModel> GetById(Guid customerId);

        Task<ViewCustomerModel> Create(CreateCustomerModel customer);

        Task<ViewCustomerModel> Update(UpdateCustomerModel customer);

        Task<bool> Delete(Guid customerId);
    }
}
