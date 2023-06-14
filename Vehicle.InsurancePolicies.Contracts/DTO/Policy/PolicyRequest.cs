namespace Vehicle.InsurancePolicies.Contracts.DTO.Policy
{
  public class PolicyRequest
  {
    public string CustomerId { get; set; } = null!;
    public string VehicleId { get; set; } = null!;
    public string PlanName { get; set; } = null!;
    public decimal MaxValueCovered { get; set; }
    public DateTime TakenDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<string> Coverages { get; set; } = new HashSet<string>();
  }
}
