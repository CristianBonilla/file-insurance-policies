using Vehicle.InsurancePolicies.Contracts.Mongo;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Domain.Repositories
{
  public interface ICoverageRepository : IRepository<VehicleInsurancePoliciesContext, CoverageEntity> { }
}
