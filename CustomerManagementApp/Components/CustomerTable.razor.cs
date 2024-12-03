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

        private IEnumerable<ViewCustomerModel> _items = [];

        private async Task<TableData<ViewCustomerModel>> GetCustomers(TableState state, CancellationToken cancellationToken)
        {
            var result = await FetchDataAsync(state.Page, state.PageSize);
            _items = result.Items;
            return new TableData<ViewCustomerModel>
            {
                Items = _items,
                TotalItems = 100 //TODO
            };
        }

        private async Task<(IEnumerable<ViewCustomerModel> Items, int TotalItems)> FetchDataAsync(int pageNumber, int pageSize)
        {
            var response = await CustomerService.GetAllAsync(pageNumber + 1, pageSize);
            return (response, response.Count());
        }
    }
}
