using CustomerManagementService.DataLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.SolutionHelpers
{
    public class CustomerManagementWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing ApplicationDbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CustomerContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add In-Memory Database for testing
                services.AddDbContext<CustomerContext>(options =>
                    options.UseInMemoryDatabase("CustomerInMemoryDb"));

                // Ensure database is initialized
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
