using Autofac;
using Vehicle.InsurancePolicies.Contracts.MongoRepository;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Domain.Services;
using Vehicle.InsurancePolicies.Infrastructure.MongoRepository;

namespace Vehicle.InsurancePolicies.API.Modules
{
  class MongoDomainModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterGeneric(typeof(RepositoryContext<>))
        .As(typeof(IRepositoryContext<>))
        .InstancePerLifetimeScope();
      builder.RegisterGeneric(typeof(Repository<,>))
        .As(typeof(IRepository<,>))
        .InstancePerLifetimeScope();

      builder.RegisterType<VehicleInsurancePoliciesRepositoryContext>()
        .As<IVehicleInsurancePoliciesRepositoryContext>()
        .InstancePerLifetimeScope();

      builder.RegisterType<CustomerRepository>()
        .As<ICustomerRepository>()
        .InstancePerLifetimeScope();
      builder.RegisterType<CoverageRepository>()
        .As<ICoverageRepository>()
        .InstancePerLifetimeScope();
      builder.RegisterType<PolicyRepository>()
        .As<IPolicyRepository>()
        .InstancePerLifetimeScope();
      builder.RegisterType<PolicyTermRepository>()
        .As<IPolicyTermRepository>()
        .InstancePerLifetimeScope();
      builder.RegisterType<VehicleRepository>()
        .As<IVehicleRepository>()
        .InstancePerLifetimeScope();

      builder.RegisterType<PolicyService>()
        .As<IPolicyService>()
        .InstancePerLifetimeScope();
    }
  }
}
