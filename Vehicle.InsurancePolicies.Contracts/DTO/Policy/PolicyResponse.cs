using Vehicle.InsurancePolicies.Contracts.DTO.Coverage;
using Vehicle.InsurancePolicies.Contracts.DTO.Customer;
using Vehicle.InsurancePolicies.Contracts.DTO.PolicyTerm;
using Vehicle.InsurancePolicies.Contracts.DTO.Vehicle;

namespace Vehicle.InsurancePolicies.Contracts.DTO.Policy
{
  public class PolicyResponse
  {
    public string PolicyId { get; set; } = null!;
    public Guid PolicyNumber { get; set; }
    public string CustomerId { get; set; } = null!;
    public string VehicleId { get; set; } = null!;
    public string PlanName { get; set; } = null!;
    public decimal MaxValueCovered { get; set; }
    public DateTime TakenDate { get; set; }
    public CustomerResponse? Customer { get; set; }
    public VehicleResponse? Vehicle { get; set; }
    public ICollection<CoverageResponse> Coverages { get; set; } = new HashSet<CoverageResponse>();
    public PolicyTermResponse? PolicyTerm { get; set; }
  }
}
