using System.Net;
using Vehicle.InsurancePolicies.Contracts.Exceptions;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Context;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;

namespace Vehicle.InsurancePolicies.Domain.Services
{
  public class PolicyService : IPolicyService
  {
    readonly IVehicleInsurancePoliciesRepositoryContext _context;
    readonly IPolicyRepository _policyRepository;
    readonly IVehicleRepository _vehicleRepository;

    public PolicyService(
      IVehicleInsurancePoliciesRepositoryContext context,
      IPolicyRepository policyRepository,
      IVehicleRepository vehicleRepository)
    {
      _context = context;
      _policyRepository = policyRepository;
      _vehicleRepository = vehicleRepository;
    }

    public async Task AddPolicy(PolicyEntity policy, DateTime startDate, DateTime endDate)
    {
      int takenDateValid = DateTime.Compare(policy.TakenDate, startDate);
      if (takenDateValid < 0)
        throw new ServiceErrorException(HttpStatusCode.BadRequest, "The start date cannot be earlier than the end date");
      int startDateValid = DateTime.Compare(startDate, endDate);
      if (startDateValid >= 0)
        throw new ServiceErrorException(HttpStatusCode.BadRequest, "The end date must be after the start date");
      int currentDateValid = DateTime.Compare(DateTime.Now, endDate);
      if (currentDateValid < 0)
        throw new ServiceErrorException(HttpStatusCode.BadRequest, "The policy cannot be created if it is not current");
      _policyRepository.Create(policy);
      await _context.SaveAsync();
    }

    public PolicyEntity? FindPolicyByNumber(Guid policyNumber)
    {
      PolicyEntity? policy = _policyRepository.Find(policy => policy.PolicyNumber == policyNumber);

      return policy ?? throw new ServiceErrorException(HttpStatusCode.NotFound, $"Policy not found with policy number \"{policyNumber}\"");
    }

    public PolicyEntity? FindPolicyByPlateVehicle(string? plate)
    {
      VehicleEntity? vehicle = _vehicleRepository.Find(vehicle => vehicle.Plate == plate)
        ?? throw new ServiceErrorException(HttpStatusCode.NotFound, $"Vehicle not found with plate \"{plate}\"");
      PolicyEntity? policy = _policyRepository.Find(policy => policy.VehicleId == vehicle.VehicleId);

      return policy ?? throw new ServiceErrorException(HttpStatusCode.NotFound, $"Policy not found with vehicle identifier \"{vehicle.VehicleId}\"");
    }
  }
}
