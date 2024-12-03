using CustomerManagementService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.DataLayer
{
    public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
