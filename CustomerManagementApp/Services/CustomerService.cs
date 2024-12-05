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

        public async Task<ServiceResponse> GetAllAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/customer?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResponse>();
        }

        public async Task<ViewCustomerModel?> CreateAsync(CreateCustomerModel customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ViewCustomerModel>();
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCustomerModel customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{id}", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginAsync(LoginModel login)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", login);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
