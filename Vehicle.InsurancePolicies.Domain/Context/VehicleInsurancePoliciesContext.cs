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
        .HasProperty(
          property => property.CustomerName,
          config => config.HasElementName("customerName"))
        .HasProperty(
          property => property.DocumentNumber,
          config => config.HasElementName("documentNumber"))
        .HasProperty(
          property => property.BirthDate,
          config => config.HasElementName("birthDate"))
        .HasProperty(
          property => property.City,
          config => config.HasElementName("city"))
        .HasProperty(
          property => property.Address,
          config => config.HasElementName("address"))
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
        .HasKey(key => key.CoverageId)
        .HasProperty(
          property => property.CoverageName,
          config => config.HasElementName("coverageName"));
    }

    private static void VehicleConfig(EntityDefinitionBuilder<VehicleEntity> builder)
    {
      builder
        .ToCollection("Vehicle")
        .HasKey(key => key.VehicleId)
        .HasProperty(
          property => property.Plate,
          config => config.HasElementName("plate"))
        .HasProperty(
          property => property.Model,
          config => config.HasElementName("model"))
        .HasProperty(
          property => property.HasInspection,
          config => config.HasElementName("hasInspection"))
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
        .HasProperty(
          property => property.PolicyNumber,
          config => config.HasElementName("policyNumber"))
        .HasProperty(
          property => property.CustomerId,
          config => config.HasElementName("customerId"))
        .HasProperty(
          property => property.VehicleId,
          config => config.HasElementName("vehicleId"))
        .HasProperty(
          property => property.PlanName,
          config => config.HasElementName("planName"))
        .HasProperty(
          property => property.MaxValueCovered,
          config => config.HasElementName("maxValueCovered"))
        .HasProperty(
          property => property.TakenDate,
          config => config.HasElementName("takenDate"))
        .HasProperty(
          property => property.Coverages,
          config => config.HasElementName("coverages"))
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
        .HasKey(key => key.PolicyTermId)
        .HasProperty(
          property => property.PolicyId,
          config => config.HasElementName("policyId"))
        .HasProperty(
          property => property.StartDate,
          config => config.HasElementName("startDate"))
        .HasProperty(
          property => property.EndDate,
          config => config.HasElementName("endDate"));
    }
  }
}
