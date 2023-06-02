using AutoMapper;
using Vehicle.InsurancePolicies.Contracts.DTO.Coverage;
using Vehicle.InsurancePolicies.Contracts.DTO.Customer;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm;
using Vehicle.InsurancePolicies.Contracts.DTO.Vehicle;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.API.Mappings.Converters
{
  public class PolicyResponseConverter : ITypeConverter<(
    PolicyEntity policy,
    CustomerEntity customer,
    VehicleEntity vehicle,
    ICollection<CoverageEntity> coverages,
    PolicyTermEntity policyTerm), PolicyResponse>
  {
    public PolicyResponse Convert((
      PolicyEntity policy,
      CustomerEntity customer,
      VehicleEntity vehicle,
      ICollection<CoverageEntity> coverages,
      PolicyTermEntity policyTerm) source,
      PolicyResponse destination,
      ResolutionContext context)
    {
      IRuntimeMapper mapper = context.Mapper;
      CustomerResponse customer = mapper.Map<CustomerResponse>(source.customer);
      VehicleResponse vehicle = mapper.Map<VehicleResponse>(source.vehicle);
      ICollection<CoverageResponse> coverages = source.coverages
        .Select(coverage => mapper.Map<CoverageResponse>(coverage))
        .ToArray();
      PolicyTermResponse policyTerm = mapper.Map<PolicyTermResponse>(source.policyTerm);
      PolicyEntity policy = source.policy;
      destination = new()
      {
        PolicyId = policy.PolicyId,
        PolicyNumber = policy.PolicyNumber,
        CustomerId = policy.CustomerId,
        VehicleId = policy.VehicleId,
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
