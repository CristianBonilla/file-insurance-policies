using Microsoft.Extensions.DependencyInjection;
using Moq;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Domain.Services;

namespace Vehicle.InsurancePolicies.Tests.Domain
{
  [TestFixture]
  public class PolicyServiceTest
  {
    readonly IServiceCollection _serviceCollection = Startup.Instance.ServiceCollection;
    IPolicyService _policyService;
    readonly Mock<VehicleInsurancePoliciesContext> _mockContext = new();
    readonly Mock<IVehicleInsurancePoliciesRepositoryContext> _mockRepositoryContext = new();

    [SetUp]
    public void SetUp()
    {
      _serviceCollection.AddTransient(typeof(VehicleInsurancePoliciesContext), _ => _mockContext.Object);
      _serviceCollection.AddTransient(typeof(IVehicleInsurancePoliciesRepositoryContext), _ => _mockRepositoryContext.Object);
      _serviceCollection.AddTransient<IPolicyService, PolicyService>();
      _policyService = _serviceCollection.BuildServiceProvider()
        .CreateScope()
        .ServiceProvider.GetRequiredService<IPolicyService>();
    }

    [Test]
    public void Should_Add()
    {
    }
  }
}
