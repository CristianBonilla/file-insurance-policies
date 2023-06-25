using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeCoverageCommand
  {
    public static ObjectId CoverageId => new("647825872c728e9a99746436");

    public static CoverageEntity Coverage => new()
    {
      CoverageId = CoverageId,
      CoverageName = "Responsabilidad civil bienes y personas"
    };

    public static ICollection<CoverageEntity> Coverages => new List<CoverageEntity>()
    {
      Coverage,
      new()
      {
        CoverageId = new("647825872c728e9a99746437"),
        CoverageName = "Responsabilidad civil catastrófica"
      },
      new()
      {
        CoverageId = new("647825872c728e9a99746438"),
        CoverageName = "Robo total"
      },
      new()
      {
        CoverageId = new("647825872c728e9a99746439"),
        CoverageName = "Daños materiales"
      }
    };
  }
}
