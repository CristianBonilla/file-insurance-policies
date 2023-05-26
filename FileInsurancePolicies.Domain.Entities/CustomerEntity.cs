namespace FileInsurancePolicies.Domain.Entities
{
  public class CustomerEntity
  {
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
  }
}
