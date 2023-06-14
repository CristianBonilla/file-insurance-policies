namespace Vehicle.InsurancePolicies.Contracts.DTO.Customer
{
  public class CustomerResponse
  {
    public string CustomerId { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public long DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
  }
}
