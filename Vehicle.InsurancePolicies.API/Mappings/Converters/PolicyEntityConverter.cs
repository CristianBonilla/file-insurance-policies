using AutoMapper;
using MongoDB.Bson;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.API.Mappings.Converters
{
  class PolicyEntityConverter : ITypeConverter<PolicyRequest, PolicyEntity>
  {
    public PolicyEntity Convert(PolicyRequest source, PolicyEntity destination, ResolutionContext context)
    {
      destination = new()
      {
        PolicyId = ObjectId.GenerateNewId(),
        PolicyNumber = Guid.NewGuid(),
        CustomerId = GetObjectId(source.CustomerId),
        VehicleId = GetObjectId(source.VehicleId),
        PlanName = source.PlanName,
        MaxValueCovered = source.MaxValueCovered,
        TakenDate = source.TakenDate,
        Coverages = source.Coverages.Select(GetObjectId).ToArray()
      };

      return destination;
    }

    private static ObjectId GetObjectId(string sourceValue)
    {
      _ = ObjectId.TryParse(sourceValue, out ObjectId objectIdValue);

      return objectIdValue;
    }
  }
}
