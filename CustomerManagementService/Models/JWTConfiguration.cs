namespace CustomerManagementService.Models
{
    public class JWTConfiguration
    {
        public string? ValidIssuer { get; set; }

        public string? ValidAudience { get; set; }

        public string? SecretKey { get; set; }
    }
}
