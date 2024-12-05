using CustomerManagementService.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.DataLayer
{
    public class CustomerContext(DbContextOptions<CustomerContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
