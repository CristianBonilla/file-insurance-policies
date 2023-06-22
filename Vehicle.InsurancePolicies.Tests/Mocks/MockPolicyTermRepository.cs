using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Tests.Commands;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockPolicyTermRepository
  {
    static IEnumerable<PolicyTermEntity> _policyTerms = FakePolicyTermCommand.PolicyTerms;
    static readonly IQueryable<PolicyTermEntity> _policyTermsQuery = _policyTerms.AsQueryable();

    public static Mock<IPolicyTermRepository> GetMock()
    {
      Mock<IPolicyTermRepository> mockPolicyTermRepository = new();
      mockPolicyTermRepository.Setup(expression => expression.Get()).Returns(() => _policyTerms);
      mockPolicyTermRepository.Setup(expression => expression.Create(It.IsAny<PolicyTermEntity>()))
        .Callback<PolicyTermEntity>(policyTerm => _policyTerms = _policyTerms.Concat(new[] { policyTerm }));
      mockPolicyTermRepository.Setup(expression => expression.Find(It.IsAny<Expression<Func<PolicyTermEntity, bool>>>()))
        .Returns<Expression<Func<PolicyTermEntity, bool>>>(expression => _policyTermsQuery.FirstOrDefault(expression));

      return mockPolicyTermRepository;
    }
  }
}
