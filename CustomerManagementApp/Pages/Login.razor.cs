using Blazored.LocalStorage;
using CustomerManagementApp.Models;
using CustomerManagementApp.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CustomerManagementApp.Pages
{
    public partial class Login
    {
        [Inject]
        public CustomerService? CustomerService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]

        public ILocalStorageService? LocalStorageService { get; set; }

        private MudForm _form;

        private bool _isFormValid;

        private LoginModel LoginModel { get; set; } = new LoginModel();

        private async Task Submit()
        {
            await _form.Validate();

            if (_isFormValid)
            {
                var result = await CustomerService.LoginAsync(LoginModel);
                if (!string.IsNullOrEmpty(result))
                {
                    await LocalStorageService.SetItemAsync("token", result);
                    NavigationManager.NavigateTo("/");
                }
            }
        }
    }
}
