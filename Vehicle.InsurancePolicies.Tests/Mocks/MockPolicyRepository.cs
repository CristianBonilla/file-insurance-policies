using MongoDB.Bson;
using Moq;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  static class MockPolicyRepository
  {
    static IEnumerable<PolicyEntity> _policies = new PolicyEntity[]
    {
      new()
      {
        PolicyId = new ObjectId("64782b6c2c728e9a9974643f"),
        CustomerId = new ObjectId("647825cf2c728e9a9974643d"),
        VehicleId = new ObjectId("647825af2c728e9a9974643c"),
        PlanName = "plan-1",
        MaxValueCovered = 120000000,
        TakenDate = new DateTime(2023, 1, 13),
        PolicyNumber = new Guid("f249878b-d86e-4789-96a4-4640ac31e8ed"),
        Coverages = new[]
        {
          new ObjectId("647825872c728e9a99746436"),
          new ObjectId("647825872c728e9a99746437"),
          new ObjectId("647825872c728e9a99746438"),
          new ObjectId("647825872c728e9a99746439")
        }
      },
      new()
      {
        PolicyId = new ObjectId("649023051010edc293bb3ad4"),
        CustomerId = new ObjectId("647825cf2c728e9a9974643d"),
        VehicleId = new ObjectId("647825af2c728e9a9974643b"),
        PlanName = "plan-2",
        MaxValueCovered = 322000000,
        TakenDate = new DateTime(2023, 3, 13),
        PolicyNumber = new Guid("f249878b-d86e-4789-96a4-4640ac31e8ed"),
        Coverages = new[]
        {
          new ObjectId("647825872c728e9a99746436"),
          new ObjectId("647825872c728e9a99746437"),
          new ObjectId("647825872c728e9a99746438")
        }
      }
    };

    static readonly IQueryable<PolicyEntity> _policiesQuery = _policies.AsQueryable();

    public static Mock<IPolicyRepository> GetMock()
    {
      var mockPolicyRepository = new Mock<IPolicyRepository>();
      mockPolicyRepository.Setup(expression => expression.Get()).Returns(() => _policies);
      mockPolicyRepository.Setup(expression => expression.Create(It.IsAny<PolicyEntity>()))
        .Callback<PolicyEntity>(policy => _policies = _policies.Concat(new[] { policy }));
      mockPolicyRepository.Setup(expression => expression.Find(It.IsAny<ObjectId>()))
        .Returns<ObjectId>(policyId => _policiesQuery.FirstOrDefault(policy => policy.PolicyId == policyId));

      return mockPolicyRepository;
    }
  }
}
