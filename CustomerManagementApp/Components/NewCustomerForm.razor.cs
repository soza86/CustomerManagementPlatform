using CustomerManagementApp.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CustomerManagementApp.Components
{
    public partial class NewCustomerForm
    {
        [CascadingParameter] 
        MudDialogInstance? MudDialog { get; set; }

        [Parameter] 
        public CreateCustomerModel? Customer { get; set; }

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
