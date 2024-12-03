using AutoMapper;
using CustomerManagementService.Models.Entities;
using CustomerManagementService.Models.Resources;

namespace CustomerManagementService.BusinessLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, ViewCustomerModel>().ReverseMap();
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<UpdateCustomerModel, Customer>();
        }
    }
}
