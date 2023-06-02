namespace Vehicle.InsurancePolicies.Domain.Entities.Transfers
{
  public class PolicyTransfer
  {
    public PolicyEntity Policy { get; set; } = new();
    public CustomerEntity Customer { get; set; } = new();
    public VehicleEntity Vehicle { get; set; } = new();
    public ICollection<CoverageEntity> Coverages { get; set; } = new HashSet<CoverageEntity>();
    public PolicyTermEntity PolicyTerm { get; set; } = new();
  }
}
