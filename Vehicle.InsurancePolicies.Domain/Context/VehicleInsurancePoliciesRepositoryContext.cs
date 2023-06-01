using Vehicle.InsurancePolicies.Infrastructure.Mongo;

namespace Vehicle.InsurancePolicies.Domain.Context
{
  public class VehicleInsurancePoliciesRepositoryContext
    : RepositoryContext<VehicleInsurancePoliciesContext>, IVehicleInsurancePoliciesRepositoryContext
  {
    public VehicleInsurancePoliciesRepositoryContext(VehicleInsurancePoliciesContext context) : base(context) { }
  }
}
