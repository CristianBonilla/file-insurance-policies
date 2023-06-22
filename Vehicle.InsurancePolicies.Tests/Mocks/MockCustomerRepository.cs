using MongoDB.Bson;
using Moq;
using System.Linq.Expressions;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Tests.Commands;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockCustomerRepository
  {
    static readonly IEnumerable<CustomerEntity> _customers = FakeCustomerCommand.Customers;
    static readonly IQueryable<CustomerEntity> _customersQuery = _customers.AsQueryable();

    public static Mock<ICustomerRepository> GetMock()
    {
      Mock<ICustomerRepository> mockCustomerRepository = new();
      mockCustomerRepository.Setup(expression => expression.Get()).Returns(() => _customers);
      mockCustomerRepository.Setup(expression => expression.Exists(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
        .Returns<Expression<Func<CustomerEntity, bool>>>(predicate => _customersQuery.Any(predicate));
      mockCustomerRepository.Setup(expression => expression.Find(It.IsAny<ObjectId>()))
        .Returns<ObjectId>(customerId => _customersQuery.FirstOrDefault(customer => customer.CustomerId == customerId));

      return mockCustomerRepository;
    }
  }
}
