using CustomerManagementService.BusinessLayer;
using CustomerManagementService.Controllers;
using CustomerManagementService.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.UnitTests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;

        public CustomerControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
        }

        [Fact]
        public async Task Given_IRequestForCustomers_When_GetCustomers_Then_ReturnsCollectionOfCustomers()
        {
            //Arrange
            var customersList = GenerateCustomersList();
            _customerServiceMock
                .Setup(service => service.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(customersList);

            var controller = new CustomerController(_customerServiceMock.Object);

            // Act
            var result = await controller.GetCustomers(1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ViewCustomerModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
            Assert.Equal("John Doe", returnValue.FirstOrDefault()?.ContactName);
            Assert.Equal("Region 1", returnValue.FirstOrDefault()?.Region);
        }

        [Fact]
        public async Task Given_IRequestForASingleCustomer_When_GetCustomer_Then_ReturnsCustomer()
        {
            //Arrange
            var customer = GenerateCustomer();
            _customerServiceMock
                .Setup(service => service.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            var controller = new CustomerController(_customerServiceMock.Object);

            // Act
            var result = await controller.GetCustomer(Guid.NewGuid());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ViewCustomerModel>(okResult.Value);
            Assert.Equal("John Doe", returnValue.ContactName);
            Assert.Equal("Greece", returnValue.Country);
        }

        [Fact]
        public async Task Given_IRequestForANonExistingCustomer_When_GetCustomer_Then_ReturnsNotFound()
        {
            //Arrange
            ViewCustomerModel customer = null;
            _customerServiceMock
                .Setup(service => service.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            var controller = new CustomerController(_customerServiceMock.Object);

            // Act
            var result = await controller.GetCustomer(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Given_IRequestToUpdateAnCustomerWithDifferentIdsInTheBodyOfTheRequest_When_PutCustomer_Then_ReturnsBadRequest()
        {
            //Arrange
            var updatedCustomerRequest = new UpdateCustomerModel
            {
                Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                Address = "Address 1",
                City = "City 1",
                Region = "Region 1",
                CompanyName = "Company 1",
                ContactName = "John Doe",
                Country = "Greece",
                Phone = "1234",
                PostalCode = "1345"
            };
            var controller = new CustomerController(_customerServiceMock.Object);

            // Act
            var result = await controller.PutCustomer(new Guid("11223344-5566-7748-99AA-BBCCDDEEFF00"), updatedCustomerRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteANonExistingCustomer_When_DeleteCustomer_Then_ReturnsNotFound()
        {
            //Arrange
            _customerServiceMock
                .Setup(service => service.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var controller = new CustomerController(_customerServiceMock.Object);

            // Act
            var result = await controller.DeleteCustomer(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private static IEnumerable<ViewCustomerModel> GenerateCustomersList()
        {
            IEnumerable<ViewCustomerModel> customersList = new List<ViewCustomerModel>
            {
                new() {
                     Id = Guid.NewGuid(),
                     Address = "Address 1",
                     City = "City 1",
                     Region= "Region 1",
                     CompanyName = "Company 1",
                     ContactName = "John Doe",
                     Country = "Greece",
                     Phone = "1234",
                     PostalCode = "1345"
                },
                new() {
                     Id = Guid.NewGuid(),
                     Address = "Address 2",
                     City = "City 2",
                     Region= "Region 2",
                     CompanyName = "Company 2",
                     ContactName = "Jane Doe",
                     Country = "Greece",
                     Phone = "1234",
                     PostalCode = "1345"
                }
            };
            return customersList;
        }

        private static ViewCustomerModel GenerateCustomer()
        {
            return new ViewCustomerModel
            {
                Id = Guid.NewGuid(),
                Address = "Address 1",
                City = "City 1",
                Region = "Region 1",
                CompanyName = "Company 1",
                ContactName = "John Doe",
                Country = "Greece",
                Phone = "1234",
                PostalCode = "1345"
            };
        }
    }
}
