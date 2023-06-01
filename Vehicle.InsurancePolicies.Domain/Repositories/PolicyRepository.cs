using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Infrastructure.Mongo;

namespace Vehicle.InsurancePolicies.Domain.Repositories
{
  public class PolicyRepository : Repository<VehicleInsurancePoliciesContext, PolicyEntity>, IPolicyRepository
  {
    public PolicyRepository(IVehicleInsurancePoliciesRepositoryContext context) : base(context) { }
  }
}
