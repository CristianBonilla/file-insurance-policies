namespace Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm
{
  public class PolicyTermResponse
  {
    public string PolicyTermId { get; set; } = null!;
    public string PolicyId { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
