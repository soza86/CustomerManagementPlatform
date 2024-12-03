namespace CustomerManagementApp.Models
{
    public class BingAutosuggestResponse
    {
        public ResourceSet[]? ResourceSets { get; set; }
    }

    public class ResourceSet
    {
        public Resource[]? Resources { get; set; }
    }

    public class Resource
    {
        public string[]? Suggestions { get; set; }
    }
}
