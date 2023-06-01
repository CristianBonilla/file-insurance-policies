using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Infrastructure.MongoRepository;

namespace Vehicle.InsurancePolicies.Domain.Repositories
{
  public class PolicyTermRepository : Repository<VehicleInsurancePoliciesContext, PolicyTermEntity>, IPolicyTermRepository
  {
    public PolicyTermRepository(IVehicleInsurancePoliciesRepositoryContext context) : base(context) { }
  }
}
