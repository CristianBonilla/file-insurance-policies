namespace Vehicle.InsurancePolicies.API.Options
{
  class JwtOptions
  {
    public string Secret { get; set; } = string.Empty;
    public int? ExpiresInDays { get; set; }
  }
}
