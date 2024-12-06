using CustomerManagementService.Models.Resources;

namespace CustomerManagementService.BusinessLayer
{
    public interface IUserManagementService
    {
        Task<string> LoginUser(LoginModel model);
    }
}
