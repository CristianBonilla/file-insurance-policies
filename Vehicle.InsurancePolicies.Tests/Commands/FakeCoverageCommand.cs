using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeCoverageCommand
  {
    public static ObjectId CoverageId => new("649526f8344c23461362816b");

    public static CoverageEntity Coverage => new()
    {
      CoverageId = CoverageId,
      CoverageName = "Defensa jurídica"
    };

    public static IEnumerable<CoverageEntity> Coverages => new CoverageEntity[]
    {
      new()
      {
        CoverageId = new("647825872c728e9a99746436"),
        CoverageName = "Responsabilidad civil bienes y personas"
      },
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
