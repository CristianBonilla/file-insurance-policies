namespace Vehicle.InsurancePolicies.Domain.Entities
{
  public class CustomerEntity
  {
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public long DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
  }
}
