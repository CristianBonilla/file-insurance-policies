using MongoDB.Bson;
using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockCustomerRepository
  {
    static IEnumerable<CustomerEntity> _customers = new CustomerEntity[]
    {
      new()
      {
        CustomerId = new ObjectId("647825cf2c728e9a9974643d"),
        CustomerName = "Cristian Camilo Bonilla",
        DocumentNumber = 1023944678,
        BirthDate = new DateTime(1995, 8, 11),
        City = "Bogotá D.C",
        Address = "Cra. 12c # 135 - 43"
      },
      new()
      {
        CustomerId = new ObjectId("647825cf2c728e9a9974643e"),
        CustomerName = "Andrea Camila Suarez",
        DocumentNumber = 1033512243,
        BirthDate = new DateTime(1998, 11, 22),
        City = "Bogotá D.C",
        Address = "Cl. 142 # 70 - 22"
      }
    };

    static readonly IQueryable<CustomerEntity> _customersQuery = _customers.AsQueryable();

    public static Mock<ICustomerRepository> GetMock()
    {
      var mockCustomerRepository = new Mock<ICustomerRepository>();
      mockCustomerRepository.Setup(expression => expression.Get()).Returns(() => _customers);
      mockCustomerRepository.Setup(expression => expression.Exists(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
        .Returns<Expression<Func<CustomerEntity, bool>>>(predicate => _customersQuery.Any(predicate));
      mockCustomerRepository.Setup(expression => expression.Find(It.IsAny<ObjectId>()))
        .Returns<ObjectId>(customerId => _customersQuery.FirstOrDefault(customer => customer.CustomerId == customerId));

      return mockCustomerRepository;
    }
  }
}
