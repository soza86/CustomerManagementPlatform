using CustomerManagementApp.Models;
using System.Net.Http.Json;

namespace CustomerManagementApp.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ViewCustomerModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/customer?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ViewCustomerModel>>() ?? new List<ViewCustomerModel>();
        }
    }
}
