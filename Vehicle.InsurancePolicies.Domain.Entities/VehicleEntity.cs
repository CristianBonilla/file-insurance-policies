namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class VehicleEntity
  {
    public string VehicleId { get; set; } = string.Empty;
    public string Plate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public bool HasInspection { get; set; }
  }
}
