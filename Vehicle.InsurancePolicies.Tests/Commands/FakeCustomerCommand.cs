using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeCustomerCommand
  {
    public static ObjectId CustomerId => new("64952899344c23461362816f");

    public static CustomerEntity Customer => new()
    {
      CustomerId = CustomerId,
      CustomerName = "Jessica Natalia Jimenez",
      DocumentNumber = 1022938312,
      BirthDate = new(1996, 1, 9),
      City = "Cali",
      Address = "Cl. 122c # 13 - 88"
    };

    public static IEnumerable<CustomerEntity> Customers => new CustomerEntity[]
    {
      new()
      {
        CustomerId = new("647825cf2c728e9a9974643d"),
        CustomerName = "Cristian Camilo Bonilla",
        DocumentNumber = 1023944678,
        BirthDate = new(1995, 8, 11),
        City = "Bogotá D.C",
        Address = "Cra. 12c # 135 - 43"
      },
      new()
      {
        CustomerId = new("647825cf2c728e9a9974643e"),
        CustomerName = "Andrea Camila Suarez",
        DocumentNumber = 1033512243,
        BirthDate = new(1998, 11, 22),
        City = "Bogotá D.C",
        Address = "Cl. 142 # 70 - 22"
      }
    };
  }
}
