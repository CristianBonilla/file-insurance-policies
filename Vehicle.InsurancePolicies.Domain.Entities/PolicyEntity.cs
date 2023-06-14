using MongoDB.Bson;

namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class PolicyEntity
  {
    public ObjectId PolicyId { get; set; }
    public Guid PolicyNumber { get; set; }
    public ObjectId CustomerId { get; set; }
    public ObjectId VehicleId { get; set; }
    public string PlanName { get; set; } = null!;
    public decimal MaxValueCovered { get; set; }
    public DateTime TakenDate { get; set; }
    public ICollection<ObjectId> Coverages { get; set; } = new HashSet<ObjectId>();
  }
}
