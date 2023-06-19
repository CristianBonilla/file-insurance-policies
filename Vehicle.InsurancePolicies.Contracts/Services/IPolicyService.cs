using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;

namespace Vehicle.InsurancePolicies.Contracts.Services
{
  public interface IPolicyService
  {
    Task<PolicyTransfer> AddPolicy(PolicyEntity policy);
    PolicyTransfer FindPolicyByNumber(Guid policyNumber);
    PolicyTransfer FindPolicyByPlateVehicle(string? plate);
  }
}
