using AutoMapper;

namespace Vehicle.InsurancePolicies.API.Mappings
{
  class MapperStart
  {
    public static MapperConfiguration Build()
    {
      MapperConfiguration configuration = new(configuration => configuration.AddProfile<VehicleInsurancePoliciesProfile>());
      configuration.AssertConfigurationIsValid();

      return configuration;
    }
  }
}
