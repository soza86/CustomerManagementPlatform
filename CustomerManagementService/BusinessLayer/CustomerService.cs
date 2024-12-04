using AutoMapper;
using CustomerManagementService.DataLayer;
using CustomerManagementService.Models.Entities;
using CustomerManagementService.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.BusinessLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> customerRepository,
                               IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> GetAll(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var records = await _customerRepository.GetAsIQueryable().Skip(skip).Take(take).ToListAsync();
            var totalCount = await _customerRepository.GetAsIQueryable().CountAsync();
            return new ServiceResponse
            {
                 Customers = _mapper.Map<IEnumerable<ViewCustomerModel>>(records),
                 TotalItems = totalCount
            };
        }

        public async Task<ViewCustomerModel> GetById(Guid customerId)
        {
            var record = await _customerRepository.GetByIdAsync(customerId);
            return _mapper.Map<ViewCustomerModel>(record);
        }

        public async Task<ViewCustomerModel> Create(CreateCustomerModel customer)
        {
            var record = _mapper.Map<Customer>(customer);
            var result = await _customerRepository.AddAsync(record);
            await _customerRepository.SaveAsync();
            return _mapper.Map<ViewCustomerModel>(result);
        }

        public async Task<ViewCustomerModel> Update(UpdateCustomerModel customer)
        {
            var record = _mapper.Map<Customer>(customer);
            var result = _customerRepository.Update(record);
            await _customerRepository.SaveAsync();
            return _mapper.Map<ViewCustomerModel>(result);
        }

        public async Task<bool> Delete(Guid customerId)
        {
            var result = await _customerRepository.DeleteAsync(customerId);
            await _customerRepository.SaveAsync();
            return result;
        }
    }
}
