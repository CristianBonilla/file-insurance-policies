namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class PolicyEntity
  {
    public string PolicyId { get; set; } = string.Empty;
    public Guid PolicyNumber { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string VehicleId { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public decimal MaxValueCovered { get; set; }
    public DateTime TakenDate { get; set; }
    public ICollection<string> Coverages { get; set; } = new HashSet<string>();
  }
}
