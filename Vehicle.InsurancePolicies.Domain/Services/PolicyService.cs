using System.Net;
using MongoDB.Bson;
using Vehicle.InsurancePolicies.Contracts.Exceptions;
using Vehicle.InsurancePolicies.Contracts.Helpers;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.SourceValues;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;
using Vehicle.InsurancePolicies.Domain.Extensions;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Domain.Services
{
  public class PolicyService : IPolicyService
  {
    readonly IHelper _helper;
    readonly IVehicleInsurancePoliciesRepositoryContext _context;
    readonly IVehicleRepository _vehicleRepository;
    readonly ICustomerRepository _customerRepository;
    readonly ICoverageRepository _coverageRepository;
    readonly IPolicyRepository _policyRepository;
    readonly IPolicyTermRepository _policyTermRepository;

    public PolicyService(
      IHelper helper,
      IVehicleInsurancePoliciesRepositoryContext context,
      IVehicleRepository vehicleRepository,
      ICustomerRepository customerRepository,
      ICoverageRepository coverageRepository,
      IPolicyRepository policyRepository,
      IPolicyTermRepository policyTermRepository)
    {
      _context = context;
      _vehicleRepository = vehicleRepository;
      _customerRepository = customerRepository;
      _coverageRepository = coverageRepository;
      _policyRepository = policyRepository;
      _policyTermRepository = policyTermRepository;
      _helper = helper;
    }

    public async Task<PolicyTransfer> AddPolicy(PolicyEntity policy)
    {
      var (startDate, endDate) = _helper.RandomDates;
      CheckPolicy(policy, startDate, endDate);
      _policyRepository.Create(policy);
      PolicyTermEntity policyTerm = new()
      {
        PolicyTermId = ObjectId.GenerateNewId(),
        PolicyId = policy.PolicyId,
        StartDate = startDate,
        EndDate = endDate
      };
      _policyTermRepository.Create(policyTerm);
      await _context.SaveAsync();
      PolicyTransfer policyTransfer = FindPolicyByNumber(policy.PolicyNumber);

      return policyTransfer;
    }

    public async IAsyncEnumerable<PolicyTransfer> GetPolicies()
    {
      var policies = _policyRepository.Get().ToAsyncEnumerable();
      await foreach (PolicyEntity policy in policies)
        yield return GetPolicyTransfer(policy);
    }

    public PolicyTransfer FindPolicyByNumber(Guid policyNumber)
    {
      PolicyEntity? policy = _policyRepository.Find(policy => policy.PolicyNumber == policyNumber)
        ?? throw new ServiceErrorException(HttpStatusCode.NotFound, $"Policy not found with policy number \"{policyNumber}\"");

      return GetPolicyTransfer(policy);
    }

    public PolicyTransfer FindPolicyByPlateVehicle(string? plate)
    {
      VehicleEntity? vehicle = _vehicleRepository.Find(vehicle => vehicle.Plate == plate)
        ?? throw new ServiceErrorException(HttpStatusCode.NotFound, $"Vehicle not found with plate \"{plate}\"");
      PolicyEntity? policy = _policyRepository.Find(policy => policy.VehicleId == vehicle.VehicleId)
        ?? throw new ServiceErrorException(HttpStatusCode.NotFound, $"Policy not found with vehicle identifier \"{vehicle.VehicleId}\"");

      return GetPolicyTransfer(policy);
    }

    private void CheckPolicy(PolicyEntity policy, DateTime startDate, DateTime endDate)
    {
      PolicySourceValues sourceValues = policy.GetSourceValues();
      CustomerExists(policy.CustomerId);
      VehicleExists(policy.VehicleId);
      ValidatePolicyDates(policy.TakenDate);
      CoverageExists(policy.Coverages.Zip(sourceValues.Coverages));

      void CustomerExists(ObjectId customerId)
      {
        bool customerExists = _customerRepository.Exists(customer => customer.CustomerId == customerId);
        if (!customerExists)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The client with the id \"{sourceValues.CustomerId}\" does not exist");
      }

      void VehicleExists(ObjectId vehicleId)
      {
        bool vehicleExists = _vehicleRepository.Exists(vehicle => vehicle.VehicleId == vehicleId);
        if (!vehicleExists)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The vehicle with the id \"{sourceValues.VehicleId}\" does not exist");
      }

      void ValidatePolicyDates(DateTime takenDate)
      {
        int startDateValid = DateTime.Compare(takenDate, startDate);
        if (startDateValid >= 0)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The start date cannot be earlier than the taken date. \"Taken date: {takenDate}\" \"Start date random: {startDate}\"");
        int endDateValid = DateTime.Compare(startDate, endDate);
        if (endDateValid > 0)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The end date must be after the start date. \"Start date random: {startDate}\". \"End date random: {endDate}\"");
        int currentDateValid = DateTime.Compare(endDate, DateTime.Now);
        if (currentDateValid < 0)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The policy cannot be created if it is not current. \"End date random: {endDate}\". \"Current date: {DateTime.Now}\"");
      }

      void CoverageExists(IEnumerable<(ObjectId, string)> coverageIds)
      {
        var nonExistent = coverageIds.Where(coverageId => !_coverageRepository.Exists(coverage => coverage.CoverageId == coverageId.Item1))
          .Select(coverageId => coverageId.Item2);
        if (nonExistent.Any())
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"There are non-existent coverages: {string.Join(", ", nonExistent)}");
      }
    }

    private PolicyTransfer GetPolicyTransfer(PolicyEntity policy)
    {
      CustomerEntity customer = _customerRepository.Find(policy.CustomerId)!;
      VehicleEntity vehicle = _vehicleRepository.Find(policy.VehicleId)!;
      var coverages = policy.Coverages.Select(coverageID => _coverageRepository.Find(coverageID)!)
        .ToArray();
      PolicyTermEntity policyTerm = _policyTermRepository.Find(policyTerm => policyTerm.PolicyId == policy.PolicyId)!;

      return new()
      {
        Policy = policy,
        Customer = customer,
        Vehicle = vehicle,
        Coverages = coverages,
        PolicyTerm = policyTerm
      };
    }
  }
}
