using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Infrastructure.Mongo;

namespace Vehicle.InsurancePolicies.Domain.Repositories
{
  public class CoverageRepository : Repository<VehicleInsurancePoliciesContext, CoverageEntity>, ICoverageRepository
  {
    public CoverageRepository(IVehicleInsurancePoliciesRepositoryContext context) : base(context) { }
  }
}
