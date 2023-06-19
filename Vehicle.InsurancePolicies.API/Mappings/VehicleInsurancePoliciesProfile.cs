using AutoMapper;
using Vehicle.InsurancePolicies.API.Mappings.Converters;
using Vehicle.InsurancePolicies.Contracts.DTO.Coverage;
using Vehicle.InsurancePolicies.Contracts.DTO.Customer;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm;
using Vehicle.InsurancePolicies.Contracts.DTO.Vehicle;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;

namespace Vehicle.InsurancePolicies.API.Mappings
{
  class VehicleInsurancePoliciesProfile : Profile
  {
    public VehicleInsurancePoliciesProfile()
    {
      CreateMap<CustomerEntity, CustomerResponse>()
        .ForMember(member => member.CustomerId, options => options.MapFrom(map => map.CustomerId.ToString()));
      CreateMap<VehicleEntity, VehicleResponse>()
        .ForMember(member => member.VehicleId, options => options.MapFrom(map => map.VehicleId.ToString()));
      CreateMap<CoverageEntity, CoverageResponse>()
        .ForMember(member => member.CoverageId, options => options.MapFrom(map => map.CoverageId.ToString()));
      CreateMap<PolicyTermEntity, PolicyTermResponse>()
        .ForMember(member => member.PolicyTermId, options => options.MapFrom(map => map.PolicyTermId.ToString()));
      CreateMap<PolicyEntity, PolicyRequest>()
        .ReverseMap()
        .ConvertUsing<PolicyEntityConverter>();
      CreateMap<PolicyTransfer, PolicyResponse>()
        .ConvertUsing<PolicyResponseConverter>();
      CreateMap<IAsyncEnumerable<PolicyTransfer>, IAsyncEnumerable<PolicyResponse>>()
        .ConvertUsing<PolicyResponsesAsyncConverter>();
    }
  }
}
