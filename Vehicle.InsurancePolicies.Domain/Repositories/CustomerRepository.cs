using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Infrastructure.MongoRepository;

namespace Vehicle.InsurancePolicies.Domain.Repositories
{
  public class CustomerRepository : Repository<VehicleInsurancePoliciesContext, CustomerEntity>, ICustomerRepository
  {
    public CustomerRepository(IVehicleInsurancePoliciesRepositoryContext context) : base(context) { }
  }
}
