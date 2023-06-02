using AutoMapper;
using Vehicle.InsurancePolicies.API.Mappings.Converters;
using Vehicle.InsurancePolicies.Contracts.DTO.Coverage;
using Vehicle.InsurancePolicies.Contracts.DTO.Customer;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm;
using Vehicle.InsurancePolicies.Contracts.DTO.Vehicle;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.API.Mappings
{
  class VehicleInsurancePoliciesProfile : Profile
  {
    public VehicleInsurancePoliciesProfile()
    {
      CreateMap<CustomerEntity, CustomerResponse>();
      CreateMap<VehicleEntity, VehicleResponse>();
      CreateMap<CoverageEntity, CoverageResponse>();
      CreateMap<PolicyTermEntity, PolicyTermResponse>();
      CreateMap<PolicyRequest, PolicyEntity>()
        .ForMember(member => member.PolicyId, options => options.Ignore())
        .ForMember(member => member.PolicyNumber, options => options.Ignore())
        .ReverseMap()
        .ForMember(member => member.StartDate, options => options.Ignore())
        .ForMember(member => member.EndDate, options => options.Ignore());
      CreateMap<(
        PolicyEntity,
        CustomerEntity,
        VehicleEntity,
        ICollection<CoverageEntity>,
        PolicyTermEntity), PolicyResponse>()
        .ConvertUsing<PolicyResponseConverter>();
    }
  }
}
