using System.Text;
using System.Security.Cryptography;

namespace Vehicle.InsurancePolicies.API.Options
{
  class JwtOptions
  {
    public string Secret { get; set; } = null!;
    public int? ExpiresInDays { get; set; }

    public (byte[] Bytes, string Hash) GetGeneratedKey()
    {
      using SHA512 sha512 = SHA512.Create();
      byte[] hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(Secret));

      return (hash, Convert.ToHexString(hash));
    }
  }
}
