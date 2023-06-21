using MongoDB.Bson;
using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockPolicyTermRepository
  {
    static IEnumerable<PolicyTermEntity> _policyTerms = new PolicyTermEntity[]
    {
      new()
      {
        PolicyTermId = new ObjectId("647874112c728e9a99746440"),
        PolicyId = new ObjectId("64782b6c2c728e9a9974643f"),
        StartDate = new DateTime(2023, 1, 23),
        EndDate = new DateTime(2023, 12, 31)
      },
      new()
      {
        PolicyTermId = new ObjectId("649023071010edc293bb3ad5"),
        PolicyId = new ObjectId("649023051010edc293bb3ad4"),
        StartDate = new DateTime(2024, 10, 12, 23, 20, 32),
        EndDate = new DateTime(2025, 5, 17, 0, 21, 30)
      }
    };

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
