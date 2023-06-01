using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Contracts.Services
{
  public interface IPolicyService
  {
    Task AddPolicy(PolicyEntity policy, DateTime startDate, DateTime endDate);
    PolicyEntity? FindPolicyByNumber(Guid policyNumber);
    PolicyEntity? FindPolicyByPlateVehicle(string? plate);
  }
}
