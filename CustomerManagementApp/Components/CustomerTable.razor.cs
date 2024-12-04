using CustomerManagementApp.Models;
using CustomerManagementApp.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CustomerManagementApp.Components
{
    public partial class CustomerTable
    {
        [Inject]
        public CustomerService? CustomerService { get; set; }

        [Inject]
        public IDialogService? DialogService { get; set; }

        [Inject]
        NavigationManager? NavigationManager { get; set; }

        private IEnumerable<ViewCustomerModel> _items = [];

        private async Task<TableData<ViewCustomerModel>> GetCustomers(TableState state, CancellationToken cancellationToken)
        {
            var result = await FetchDataAsync(state.Page, state.PageSize);
            _items = result.Items;
            return new TableData<ViewCustomerModel>
            {
                Items = _items,
                TotalItems = result.TotalItems
            };
        }

        private async Task<(IEnumerable<ViewCustomerModel> Items, int TotalItems)> FetchDataAsync(int pageNumber, int pageSize)
        {
            var response = await CustomerService.GetAllAsync(pageNumber + 1, pageSize);
            return (response.Customers, response.TotalItems);
        }

        private async Task OpenForm(ViewCustomerModel customer)
        {
            var options = new DialogOptions { CloseButton = true, Position = DialogPosition.Center, CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Customer", GetCustomer(customer) }
            };
            var dialog = DialogService.Show<EditCustomerForm>("Edit Customer", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var editedCustomer = result.Data as UpdateCustomerModel;
                var response = await CustomerService.UpdateAsync(editedCustomer.Id, editedCustomer);
                if (response)
                    NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
            else
            {
                dialog.Close();
            }
        }

        private async Task DeleteRecord(Guid customerId)
        {
            var result = await CustomerService.DeleteAsync(customerId);
            if (result)
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }

        private UpdateCustomerModel GetCustomer(ViewCustomerModel customer)
        {
            return new UpdateCustomerModel
            {
                Id = customer.Id,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName
            };
        }
    }
}
