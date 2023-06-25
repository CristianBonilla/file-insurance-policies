using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeCustomerCommand
  {
    public static ObjectId CustomerId = new("647825cf2c728e9a9974643d");

    public static CustomerEntity Customer => new()
    {
      CustomerId = CustomerId,
      CustomerName = "Cristian Camilo Bonilla",
      DocumentNumber = 1023944678,
      BirthDate = new(1995, 8, 11),
      City = "Bogotá D.C",
      Address = "Cra. 12c # 135 - 43"
    };

    public static ICollection<CustomerEntity> Customers => new List<CustomerEntity>()
    {
      Customer,
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
