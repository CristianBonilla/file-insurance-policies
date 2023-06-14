namespace Vehicle.InsurancePolicies.Contracts.DTO.Vehicle
{
  public class VehicleResponse
  {
    public string VehicleId { get; set; } = null!;
    public string Plate { get; set; } = null!;
    public string Model { get; set; } = null!;
    public bool HasInspection { get; set; }
  }
}
