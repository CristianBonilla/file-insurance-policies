using AutoMapper;
using Vehicle.InsurancePolicies.Contracts.DTO.Coverage;
using Vehicle.InsurancePolicies.Contracts.DTO.Customer;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm;
using Vehicle.InsurancePolicies.Contracts.DTO.Vehicle;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;

namespace Vehicle.InsurancePolicies.API.Mappings.Converters
{
  class PolicyResponseConverter : ITypeConverter<PolicyTransfer, PolicyResponse>
  {
    public PolicyResponse Convert(PolicyTransfer source, PolicyResponse destination, ResolutionContext context)
    {
      IRuntimeMapper mapper = context.Mapper;
      CustomerResponse customer = mapper.Map<CustomerResponse>(source.Customer);
      VehicleResponse vehicle = mapper.Map<VehicleResponse>(source.Vehicle);
      ICollection<CoverageResponse> coverages = source.Coverages
        .Select(mapper.Map<CoverageResponse>)
        .ToArray();
      PolicyTermResponse policyTerm = mapper.Map<PolicyTermResponse>(source.PolicyTerm);
      PolicyEntity policy = source.Policy;
      destination = new()
      {
        PolicyId = policy.PolicyId.ToString(),
        PolicyNumber = policy.PolicyNumber,
        CustomerId = policy.CustomerId.ToString(),
        VehicleId = policy.VehicleId.ToString(),
        PlanName = policy.PlanName,
        MaxValueCovered = policy.MaxValueCovered,
        TakenDate = policy.TakenDate,
        Customer = customer,
        Vehicle = vehicle,
        Coverages = coverages,
        PolicyTerm = policyTerm
      };

      return destination;
    }
  }
}
