using MongoDB.Bson;

namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class PolicyTermEntity
  {
    public ObjectId PolicyTermId { get; set; }
    public ObjectId PolicyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
