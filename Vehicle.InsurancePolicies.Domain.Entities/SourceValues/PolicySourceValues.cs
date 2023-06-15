namespace Vehicle.InsurancePolicies.Domain.Entities.SourceValues
{
  public class PolicySourceValues
  {
    public string CustomerId { get; set; } = null!;
    public string VehicleId { get; set; } = null!;
    public ICollection<string> Coverages { get; set; } = new HashSet<string>();
  }
}
