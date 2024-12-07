using Blazored.LocalStorage;
using CustomerManagementApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CustomerManagementApp.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public CustomerService(HttpClient httpClient,
                               AuthenticationStateProvider authenticationStateProvider,
                               ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<ServiceResponse> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var canAccess = await CanAccessToken();
                if (!canAccess)
                    return new ServiceResponse
                    {
                        IsSuccess = false
                    };

                var response = await _httpClient.GetAsync($"api/customer?pageNumber={pageNumber}&pageSize={pageSize}");
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse
                    {
                        IsSuccess = false,
                        StatusCode = response.StatusCode
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<ServiceResponse>();
                result.StatusCode = response.StatusCode;
                result.IsSuccess = response.IsSuccessStatusCode;
                return result;
            }
            catch (Exception)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }           
        }

        public async Task<ViewCustomerModel?> CreateAsync(CreateCustomerModel customer)
        {
            var result = await CanAccessToken();
            if (!result)
                return new ViewCustomerModel();

            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ViewCustomerModel>();
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCustomerModel customer)
        {
            var result = await CanAccessToken();
            if (!result)
                return result;

            var response = await _httpClient.PutAsJsonAsync($"api/customer/{id}", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await CanAccessToken();
            if (!result)
                return result;

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

        private async Task<bool> CanAccessToken()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity?.IsAuthenticated == false)
                return false;

            var token = await _localStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
    }
}
