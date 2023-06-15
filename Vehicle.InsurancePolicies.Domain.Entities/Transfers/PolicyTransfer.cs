namespace Vehicle.InsurancePolicies.Domain.Entities.Transfers
{
  public class PolicyTransfer
  {
    public PolicyEntity Policy { get; set; } = null!;
    public CustomerEntity Customer { get; set; } = null!;
    public VehicleEntity Vehicle { get; set; } = null!;
    public ICollection<CoverageEntity> Coverages { get; set; } = new HashSet<CoverageEntity>();
    public PolicyTermEntity PolicyTerm { get; set; } = null!;
  }
}
