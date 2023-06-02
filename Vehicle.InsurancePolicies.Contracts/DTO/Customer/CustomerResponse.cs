namespace Vehicle.InsurancePolicies.Contracts.DTO.Customer
{
  public class CustomerResponse
  {
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public long DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
  }
}
