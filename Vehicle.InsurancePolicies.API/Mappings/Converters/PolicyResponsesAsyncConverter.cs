using AutoMapper;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;

namespace Vehicle.InsurancePolicies.API.Mappings.Converters
{
  public class PolicyResponsesAsyncConverter : ITypeConverter<IAsyncEnumerable<PolicyTransfer>, IAsyncEnumerable<PolicyResponse>>
  {
    public async IAsyncEnumerable<PolicyResponse> Convert(
      IAsyncEnumerable<PolicyTransfer> source,
      IAsyncEnumerable<PolicyResponse> destination,
      ResolutionContext context)
    {
      IRuntimeMapper mapper = context.Mapper;
      var policyTransfers = await source.ToArrayAsync();
      foreach (PolicyTransfer policyTransfer in policyTransfers)
        yield return mapper.Map<PolicyResponse>(policyTransfer);
    }
  }
}
