using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Infrastructure.Mongo;

namespace Vehicle.InsurancePolicies.Domain.Repositories
{
  public class VehicleRepository : Repository<VehicleInsurancePoliciesContext, VehicleEntity>, IVehicleRepository
  {
    public VehicleRepository(IVehicleInsurancePoliciesRepositoryContext context) : base(context) { }
  }
}
