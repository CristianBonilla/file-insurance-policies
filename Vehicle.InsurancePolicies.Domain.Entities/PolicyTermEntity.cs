namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class PolicyTermEntity
  {
    public string PolicyTermId { get; set; } = string.Empty;
    public string PolicyId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
