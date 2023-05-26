namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class VehicleEntity
  {
    public Guid VehicleId { get; set; }
    public string? Plate { get; set; }
    public string? Model { get; set; }
    public bool HasInspection { get; set; }
  }
}
