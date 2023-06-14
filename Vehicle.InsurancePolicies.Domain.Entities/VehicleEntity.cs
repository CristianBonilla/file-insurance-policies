using MongoDB.Bson;

namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class VehicleEntity
  {
    public ObjectId VehicleId { get; set; }
    public string Plate { get; set; } = null!;
    public string Model { get; set; } = null!;
    public bool HasInspection { get; set; }
  }
}
