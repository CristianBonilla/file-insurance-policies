namespace Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm
{
  public class PolicyTermResponse
  {
    public string PolicyTermId { get; set; } = string.Empty;
    public string PolicyId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
