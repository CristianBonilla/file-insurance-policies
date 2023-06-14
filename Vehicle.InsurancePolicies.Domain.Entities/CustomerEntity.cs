using MongoDB.Bson;

namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class CustomerEntity
  {
    public ObjectId CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public long DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
  }
}
