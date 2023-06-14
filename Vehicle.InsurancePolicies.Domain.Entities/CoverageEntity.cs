using MongoDB.Bson;

namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class CoverageEntity
  {
    public ObjectId CoverageId { get; set; }
    public string CoverageName { get; set; } = null!;
  }
}
