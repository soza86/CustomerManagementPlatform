using CustomerManagementService;
using CustomerManagementService.DataLayer;
using CustomerManagementService.Models.Entities;
using CustomerManagementService.Models.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tests.SolutionHelpers;

namespace Tests.IntegrationTests
{
    public class CustomerAPITests : IClassFixture<CustomerManagementWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomerManagementWebApplicationFactory<Startup> _factory;
        private string _token = JwtTokenHelper.GenerateJwtToken();

        public CustomerAPITests(CustomerManagementWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async Task Given_IRequestForCustomers_When_GetCustomers_Then_ReturnsCollectionOfCustomers()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                context.Customers.Add(new Customer { Id = Guid.NewGuid(), ContactName = "John Doe" });
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/customer?pageNumber=1&pageSize=1");

            // Assert
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<ServiceResponse>();
            Assert.NotNull(customers);
            Assert.Equal("John Doe", customers.Customers.FirstOrDefault().ContactName);
        }

        [Fact]
        public async Task Given_IRequestForCustomer_When_GetCustomer_Then_ReturnsCustomer()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                context.Customers.Add(new Customer { Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), ContactName = "John Doe", City = "Athens" });
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/customer/11223344-5566-7788-99AA-BBCCDDEEFF01");

            // Assert
            response.EnsureSuccessStatusCode();
            var customer = await response.Content.ReadFromJsonAsync<ViewCustomerModel>();
            Assert.Equal("Athens", customer.City);
        }

        [Fact]
        public async Task Given_IRequestToCreateCustomer_When_PostCustomer_Then_ReturnsNewCustomer()
        {
            //Arrange
            var newCustomer = new CreateCustomerModel
            {
                 ContactName = "Jane Doe",
                 City = "Athens"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/customer", newCustomer);

            // Assert
            response.EnsureSuccessStatusCode();
            var customer = await response.Content.ReadFromJsonAsync<ViewCustomerModel>();
            Assert.Equal("Jane Doe", customer.ContactName);
            Assert.Equal("Athens", customer.City);
        }

        [Fact]
        public async Task Given_IRequestToUpdateExistingCustomer_When_PutCustomer_Then_ReturnsUpdatedCustomer()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                context.Customers.Add(new Customer { Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF02"), ContactName = "John Doe", City = "Athens" });
                context.SaveChanges();
            }
            var customer = new UpdateCustomerModel
            {
                Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF02"),
                ContactName = "John Doe",
                City = "Patras"
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/customer/11223344-5566-7788-99AA-BBCCDDEEFF02", customer);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Given_IRequestForCustomer_When_DeleteCustomer_Then_ReturnsSuccess()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                context.Customers.Add(new Customer { Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF03"), ContactName = "John Doe", City = "Athens" });
                context.SaveChanges();
            }

            // Act
            var response = await _client.DeleteAsync("/api/customer/11223344-5566-7788-99AA-BBCCDDEEFF03");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
