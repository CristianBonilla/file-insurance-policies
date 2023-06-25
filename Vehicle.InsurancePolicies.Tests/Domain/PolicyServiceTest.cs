using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Moq;
using Vehicle.InsurancePolicies.Contracts.Exceptions;
using Vehicle.InsurancePolicies.Contracts.Helpers;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.SourceValues;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;
using Vehicle.InsurancePolicies.Domain.Extensions;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Domain.Services;
using Vehicle.InsurancePolicies.Tests.Commands;
using Vehicle.InsurancePolicies.Tests.Mocks;

namespace Vehicle.InsurancePolicies.Tests.Domain
{
  [TestFixture]
  public class PolicyServiceTest
  {
    readonly IServiceCollection _serviceCollection = Startup.Instance.ServiceCollection;
    readonly Mock<IHelper> _mockHelper = new();
    readonly Mock<IVehicleInsurancePoliciesRepositoryContext> _mockRepositoryContext = new();
    readonly Mock<IVehicleRepository> _mockVehicleRepository = MockVehicleRepository.GetMock();
    readonly Mock<ICustomerRepository> _mockCustomerRepository = MockCustomerRepository.GetMock();
    readonly Mock<ICoverageRepository> _mockCoverageRepository = MockCoverageRepository.GetMock();
    readonly Mock<IPolicyRepository> _mockPolicyRepository = MockPolicyRepository.GetMock();
    readonly Mock<IPolicyTermRepository> _mockPolicyTermRepository = MockPolicyTermRepository.GetMock();
    IPolicyService _policyService;

    [SetUp]
    public void SetUp()
    {
      _serviceCollection.AddScoped(_ => _mockHelper.Object);
      _serviceCollection.AddTransient(_ => _mockRepositoryContext.Object);
      _serviceCollection.AddTransient(_ => _mockVehicleRepository.Object);
      _serviceCollection.AddTransient(_ => _mockCustomerRepository.Object);
      _serviceCollection.AddTransient(_ => _mockCoverageRepository.Object);
      _serviceCollection.AddTransient(_ => _mockPolicyRepository.Object);
      _serviceCollection.AddTransient(_ => _mockPolicyTermRepository.Object);
      _serviceCollection.AddTransient<IPolicyService, PolicyService>();
      _policyService = _serviceCollection.BuildServiceProvider()
        .CreateScope()
        .ServiceProvider.GetRequiredService<IPolicyService>();
    }

    [Test]
    public async Task Should_Add_Policy_Correctly()
    {
      // Arrange
      PolicyEntity policy = FakePolicyCommand.PolicyRequest;
      FakePolicyCommand.UpdatePolicySourceValues(policy);
      var (startDate, endDate) = FakePolicyTermCommand.PolicyTermDates;
      _mockHelper.SetupGet(expression => expression.RandomDates)
        .Returns((startDate, endDate));

      // Act
      PolicyTransfer policyTransfer = await _policyService.AddPolicy(policy);
      ObjectId customerId = policyTransfer.Customer.CustomerId;
      ObjectId vehicleId = policyTransfer.Vehicle.VehicleId;
      var coverages = policyTransfer.Coverages;

      // Assert
      _mockCustomerRepository.Verify(expression => expression.Exists(customer => customer.CustomerId == customerId), Times.Once());
      _mockVehicleRepository.Verify(expression => expression.Exists(vehicle => vehicle.VehicleId == vehicleId), Times.Once());
      _mockCoverageRepository.Verify(expression => expression.Exists(It.IsAny<Expression<Func<CoverageEntity, bool>>>()), Times.Exactly(coverages.Count));
      _mockPolicyRepository.Verify(expression => expression.Create(policy), Times.Once());
      _mockPolicyTermRepository.Verify(expression => expression.Create(It.IsAny<PolicyTermEntity>()), Times.Once());
      _mockRepositoryContext.Verify(expression => expression.SaveAsync(), Times.Once());
      Assert.That(policyTransfer, Is.Not.Null);
      Assert.That(policyTransfer.Policy, Is.EqualTo(policy));
    }

    [Test]
    public void Should_Throw_Exception_If_Non_Existent_Customer()
    {
      // Arrange
      PolicyEntity policy = FakePolicyCommand.PolicyRequest;
      FakePolicyCommand.UpdatePolicySourceValues(policy);
      policy.CustomerId = ObjectId.GenerateNewId();
      PolicySourceValues sourceValues = policy.GetSourceValues();
      var (startDate, endDate) = FakePolicyTermCommand.PolicyTermDates;
      _mockHelper.SetupGet(expression => expression.RandomDates)
        .Returns((startDate, endDate));

      // Act
      async Task addPolicyAsync() => await _policyService.AddPolicy(policy);

      // Assert
      Assert.ThrowsAsync(Is.TypeOf<ServiceErrorException>()
          .And.Message.EqualTo($"The client with the id \"{sourceValues.CustomerId}\" does not exist"),
        addPolicyAsync);
    }

    [Test]
    public void Should_Throw_Exception_If_Non_Existent_Vehicle()
    {
      // Arrange
      PolicyEntity policy = FakePolicyCommand.PolicyRequest;
      FakePolicyCommand.UpdatePolicySourceValues(policy);
      policy.VehicleId = ObjectId.GenerateNewId();
      PolicySourceValues sourceValues = policy.GetSourceValues();
      var (startDate, endDate) = FakePolicyTermCommand.PolicyTermDates;
      _mockHelper.SetupGet(expression => expression.RandomDates)
        .Returns((startDate, endDate));

      // Act
      async Task addPolicyAsync() => await _policyService.AddPolicy(policy);

      // Assert
      Assert.ThrowsAsync(Is.TypeOf<ServiceErrorException>()
          .And.Message.EqualTo($"The vehicle with the id \"{sourceValues.VehicleId}\" does not exist"),
        addPolicyAsync);
    }

    [Test]
    public void Should_Throw_Exception_If_Non_Existent_Coverages()
    {
      // Arrange
      PolicyEntity policy = FakePolicyCommand.PolicyRequest;
      ObjectId[] fakeCoverageIds = { ObjectId.GenerateNewId(), ObjectId.GenerateNewId() };
      policy.Coverages.Add(fakeCoverageIds[0]);
      policy.Coverages.Add(fakeCoverageIds[1]);
      FakePolicyCommand.UpdatePolicySourceValues(policy);
      PolicySourceValues sourceValues = policy.GetSourceValues();
      var (startDate, endDate) = FakePolicyTermCommand.PolicyTermDates;
      _mockHelper.SetupGet(expression => expression.RandomDates)
        .Returns((startDate, endDate));

      // Act
      async Task addPolicyAsync() => await _policyService.AddPolicy(policy);

      // Assert
      Assert.ThrowsAsync(Is.TypeOf<ServiceErrorException>()
          .And.Message.EqualTo($"There are non-existent coverages: {string.Join(", ", fakeCoverageIds)}"),
        addPolicyAsync);
    }
  }
}
