using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.SourceValues;

namespace Vehicle.InsurancePolicies.Domain.Extensions
{
  public static class PolicyEntityExtensions
  {
    public static readonly Dictionary<PolicyEntity, PolicySourceValues> Policies = new();

    public static void SetSourceValues(this PolicyEntity policy, PolicySourceValues sourceValues) => Policies[policy] = sourceValues;

    public static PolicySourceValues GetSourceValues(this PolicyEntity policy) => Policies[policy];
  }
}
