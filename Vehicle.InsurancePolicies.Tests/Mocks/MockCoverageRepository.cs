using MongoDB.Bson;
using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Tests.Commands;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockCoverageRepository
  {
    static readonly IEnumerable<CoverageEntity> _coverages = FakeCoverageCommand.Coverages;
    static readonly IQueryable<CoverageEntity> _coveragesQuery = _coverages.AsQueryable();

    public static Mock<ICoverageRepository> GetMock()
    {
      Mock<ICoverageRepository> mockCoverageRepository = new();
      mockCoverageRepository.Setup(expression => expression.Get()).Returns(() => _coverages);
      mockCoverageRepository.Setup(expression => expression.Exists(It.IsAny<Expression<Func<CoverageEntity, bool>>>()))
        .Returns<Expression<Func<CoverageEntity, bool>>>(expression => _coveragesQuery.Any(expression));
      mockCoverageRepository.Setup(expression => expression.Find(It.IsAny<ObjectId>()))
        .Returns<ObjectId>(coverageId => _coveragesQuery.FirstOrDefault(coverage => coverage.CoverageId == coverageId));

      return mockCoverageRepository;
    }
  }
}
