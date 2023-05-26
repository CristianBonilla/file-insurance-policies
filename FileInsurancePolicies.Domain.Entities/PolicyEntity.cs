namespace FileInsurancePolicies.Domain.Entities
{
  public class PolicyEntity
  {
    public Guid PolicyId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid VehicleId { get; set; }
    public string? PlanName { get; set; }
    public decimal MaxValueCovered { get; set; }
    public DateTime WasTaken { get; set; }
    public CustomerEntity? Customer { get; set; }
    public VehicleEntity? Vehicle { get; set; }
    public ICollection<CoverageEntity> Coverages { get; set; } = new HashSet<CoverageEntity>();
  }
}
