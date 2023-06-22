using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Tests.Commands;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockPolicyRepository
  {
    static IEnumerable<PolicyEntity> _policies = FakePolicyCommand.Policies;
    static readonly IQueryable<PolicyEntity> _policiesQuery = _policies.AsQueryable();

    public static Mock<IPolicyRepository> GetMock()
    {
      Mock<IPolicyRepository> mockPolicyRepository = new();
      mockPolicyRepository.Setup(expression => expression.Get()).Returns(() => _policies);
      mockPolicyRepository.Setup(expression => expression.Create(It.IsAny<PolicyEntity>()))
        .Callback<PolicyEntity>(policy => _policies = _policies.Concat(new[] { policy }));
      mockPolicyRepository.Setup(expression => expression.Find(It.IsAny<Expression<Func<PolicyEntity, bool>>>()))
        .Returns<Expression<Func<PolicyEntity, bool>>>(expression => _policiesQuery.FirstOrDefault(expression));

      return mockPolicyRepository;
    }
  }
}
