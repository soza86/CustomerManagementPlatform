using CustomerManagementApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CustomerManagementApp.Components
{
    public partial class AutosuggestComponent
    {
        [Inject]
        public AutosuggestService? AutosuggestService { get; set; }

        private string? _textValue;

        public async Task HandleClick(MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(_textValue))
            {
                var result = await AutosuggestService.GetSuggestion(_textValue);
            }
        }
    }
}
