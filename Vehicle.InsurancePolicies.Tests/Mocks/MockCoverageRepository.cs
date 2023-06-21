using MongoDB.Bson;
using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockCoverageRepository
  {
    static IEnumerable<CoverageEntity> _coverages = new CoverageEntity[]
    {
      new()
      {
        CoverageId = new ObjectId("647825872c728e9a99746436"),
        CoverageName = "Responsabilidad civil bienes y personas"
      },
      new()
      {
        CoverageId = new ObjectId("647825872c728e9a99746437"),
        CoverageName = "Responsabilidad civil catastrófica"
      },
      new()
      {
        CoverageId = new ObjectId("647825872c728e9a99746438"),
        CoverageName = "Robo total"
      },
      new()
      {
        CoverageId = new ObjectId("647825872c728e9a99746439"),
        CoverageName = "Daños materiales"
      }
    };

    static readonly IQueryable<CoverageEntity> _coveragesQuery = _coverages.AsQueryable();

    public static Mock<ICoverageRepository> GetMock()
    {
      var mockCoverageRepository = new Mock<ICoverageRepository>();
      mockCoverageRepository.Setup(expression => expression.Get()).Returns(() => _coverages);
      mockCoverageRepository.Setup(expression => expression.Exists(It.IsAny<Expression<Func<CoverageEntity, bool>>>()))
        .Returns<Expression<Func<CoverageEntity, bool>>>(expression => _coveragesQuery.Any(expression));
      mockCoverageRepository.Setup(expression => expression.Find(It.IsAny<ObjectId>()))
        .Returns<ObjectId>(coverageId => _coveragesQuery.FirstOrDefault(coverage => coverage.CoverageId == coverageId));

      return mockCoverageRepository;
    }
  }
}
