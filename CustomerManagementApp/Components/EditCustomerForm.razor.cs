using CustomerManagementApp.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CustomerManagementApp.Components
{
    public partial class EditCustomerForm
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public UpdateCustomerModel? Customer { get; set; }

        private void CloseDialog()
        {
            MudDialog.Close(DialogResult.Ok(Customer));
        }

        private void CancelDialog()
        {
            MudDialog.Cancel();
        }
    }
}
