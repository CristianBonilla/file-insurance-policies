namespace Vehicle.InsurancePolicies.Contracts.DTO.Vehicle
{
  public class VehicleResponse
  {
    public string VehicleId { get; set; } = string.Empty;
    public string Plate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public bool HasInspection { get; set; }
  }
}
