using Microsoft.Extensions.DependencyInjection;
using Moq;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Domain.Services;
using Vehicle.InsurancePolicies.Tests.Mocks;

namespace Vehicle.InsurancePolicies.Tests.Domain
{
  [TestFixture]
  public class PolicyServiceTest
  {
    readonly IServiceCollection _serviceCollection = Startup.Instance.ServiceCollection;
    IPolicyService _policyService;
    readonly Mock<IVehicleRepository> _mockVehicleRepository = MockVehicleRepository.GetMock();
    readonly Mock<ICustomerRepository> _mockCustomerRepository = MockCustomerRepository.GetMock();
    readonly Mock<ICoverageRepository> _mockCoverageRepository = MockCoverageRepository.GetMock();
    readonly Mock<IPolicyRepository> _mockPolicyRepository = MockPolicyRepository.GetMock();
    readonly Mock<IPolicyTermRepository> _mockPolicyTermRepository = MockPolicyTermRepository.GetMock();

    [SetUp]
    public void SetUp()
    {
      _serviceCollection.AddTransient(_ => _mockVehicleRepository.Object);
      _serviceCollection.AddTransient(_ => _mockCustomerRepository.Object);
      _serviceCollection.AddTransient(_ => _mockCoverageRepository.Object);
      _serviceCollection.AddTransient(_ => _mockPolicyRepository.Object);
      _serviceCollection.AddTransient(_ => _mockPolicyTermRepository.Object);
      _serviceCollection.AddTransient<IPolicyService, PolicyService>();
      _policyService = _serviceCollection.BuildServiceProvider()
        .CreateScope()
        .ServiceProvider.GetRequiredService<IPolicyService>();
    }

    [Test]
    public async Task Should_Add_Policy_Correctly()
    {
      // Arrange

    }
  }
}
