using System.Net;
using Vehicle.InsurancePolicies.Contracts.Exceptions;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Domain.Services
{
  public class PolicyService : IPolicyService
  {
    readonly IVehicleInsurancePoliciesRepositoryContext _context;
    readonly IVehicleRepository _vehicleRepository;
    readonly ICustomerRepository _customerRepository;
    readonly ICoverageRepository _coverageRepository;
    readonly IPolicyRepository _policyRepository;
    readonly IPolicyTermRepository _policyTermRepository;

    public PolicyService(
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
    }

    public async Task AddPolicy(PolicyEntity policy, DateTime startDate, DateTime endDate)
    {
      CheckPolicy(policy, startDate, endDate);
      policy.PolicyNumber = Guid.NewGuid();
      _policyRepository.Create(policy);
      PolicyTermEntity policyTerm = new()
      {
        PolicyId = policy.PolicyId,
        StartDate = startDate,
        EndDate = endDate
      };
      _policyTermRepository.Create(policyTerm);
      await _context.SaveAsync();
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
      CustomerExists(policy.CustomerId);
      VehicleExists(policy.VehicleId);
      ValidatePolicyDates(policy.TakenDate, startDate, endDate);
      CoverageExists(policy.Coverages);

      void CustomerExists(string customerId)
      {
        bool customerExists = _customerRepository.Exists(customer => customer.CustomerId == customerId);
        if (!customerExists)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The client with the id \"{customerId}\" does not exist");
      }

      void VehicleExists(string vehicleId)
      {
        bool vehicleExists = _vehicleRepository.Exists(vehicle => vehicle.VehicleId == vehicleId);
        if (!vehicleExists)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, $"The vehicle with the id \"{vehicleId}\" does not exist");
      }

      static void ValidatePolicyDates(DateTime takenDate, DateTime startDate, DateTime endDate)
      {
        int takenDateValid = DateTime.Compare(takenDate, startDate);
        if (takenDateValid < 0)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, "The start date cannot be earlier than the end date");
        int startDateValid = DateTime.Compare(startDate, endDate);
        if (startDateValid >= 0)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, "The end date must be after the start date");
        int currentDateValid = DateTime.Compare(DateTime.Now, endDate);
        if (currentDateValid < 0)
          throw new ServiceErrorException(HttpStatusCode.BadRequest, "The policy cannot be created if it is not current");
      }

      void CoverageExists(IEnumerable<string> coverageIDs)
      {
        var nonExistent = coverageIDs.Where(coverageId => !_coverageRepository.Exists(coverage => coverage.CoverageId == coverageId));
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
