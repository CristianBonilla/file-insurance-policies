using Autofac;
using Vehicle.InsurancePolicies.API.Services;
using Vehicle.InsurancePolicies.Contracts.Identity;

namespace Vehicle.InsurancePolicies.API.Modules
{
  class IdentityModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<IdentityService>()
        .As<IIdentityService>()
        .InstancePerLifetimeScope();
    }
  }
}
