using CustomerManagementApp.Components;
using CustomerManagementApp.Models;
using CustomerManagementApp.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CustomerManagementApp.Pages
{
    public partial class Customers
    {
        [Inject]
        public IDialogService? DialogService { get; set; }

        [Inject]
        public CustomerService? CustomerService { get; set; }

        [Inject]
        NavigationManager? NavigationManager { get; set; }

        private async Task OpenForm()
        {
            var options = new DialogOptions { CloseButton = true, Position = DialogPosition.Center, CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Customer", new CreateCustomerModel() }
            };
            var dialog = await DialogService.ShowAsync<NewCustomerForm>("Add Customer", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var newCustomer = result.Data as CreateCustomerModel;
                await CustomerService.CreateAsync(newCustomer);
                NavigationManager.NavigateTo("/customers");
            }
            else
            {
                dialog.Close();
            }
        }
    }
}
