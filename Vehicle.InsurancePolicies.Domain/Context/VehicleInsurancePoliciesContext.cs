using MongoFramework;
using MongoFramework.Infrastructure.Mapping;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Domain.Context
{
  public partial class VehicleInsurancePoliciesContext : MongoDbContext
  {
    public VehicleInsurancePoliciesContext(IMongoDbConnection connection) : base(connection) { }

    protected override void OnConfigureMapping(MappingBuilder mappingBuilder)
    {
      CustomerConfig(mappingBuilder.Entity<CustomerEntity>());
      CoverageConfig(mappingBuilder.Entity<CoverageEntity>());
      VehicleConfig(mappingBuilder.Entity<VehicleEntity>());
      PolicyConfig(mappingBuilder.Entity<PolicyEntity>());
      PolicyTermConfig(mappingBuilder.Entity<PolicyTermEntity>());
    }

    private static void CustomerConfig(EntityDefinitionBuilder<CustomerEntity> builder)
    {
      builder
        .ToCollection("Customer")
        .HasKey(key => key.CustomerId)
        .HasIndex(
          index => index.DocumentNumber,
          config => config.HasName("CustomerDocumentNumberUniqueIndex").IsUnique())
        .HasIndex(
          index => new
          {
            index.DocumentNumber,
            index.CustomerName
          },
          config => config.HasName("CustomerIndex").IsDescending(true, false));
    }

    private static void CoverageConfig(EntityDefinitionBuilder<CoverageEntity> builder)
    {
      builder
        .ToCollection("Coverage")
        .HasKey(key => key.CoverageId);
    }

    private static void VehicleConfig(EntityDefinitionBuilder<VehicleEntity> builder)
    {
      builder
        .ToCollection("Vehicle")
        .HasKey(key => key.VehicleId)
        .HasIndex(
          index => new
          {
            index.Plate,
            index.Model
          },
          config => config.HasName("VehiclePlateIndex").IsDescending(true, false));
    }

    private static void PolicyConfig(EntityDefinitionBuilder<PolicyEntity> builder)
    {
      builder
        .ToCollection("Policy")
        .HasKey(key => key.PolicyId)
        .HasIndex(
          index => index.PolicyNumber,
          config => config.HasName("PolicyNumberUniqueIndex").IsUnique())
        .HasIndex(
          index => index.PlanName,
          config => config.HasName("PolicyPlanNameIndex").IsDescending(true));
    }

    private static void PolicyTermConfig(EntityDefinitionBuilder<PolicyTermEntity> builder)
    {
      builder
        .ToCollection("PolicyTerm")
        .HasKey(key => key.PolicyTermId);
    }
  }
}
