using Microsoft.Extensions.DependencyInjection;

namespace Vehicle.InsurancePolicies.Tests.Domain
{
  [TestFixture]
  public class PolicyServiceTest
  {
    readonly IServiceCollection _serviceCollection = Startup.Instance.ServiceCollection;

    [SetUp]
    public void SetUp()
    {

    }
  }
}
