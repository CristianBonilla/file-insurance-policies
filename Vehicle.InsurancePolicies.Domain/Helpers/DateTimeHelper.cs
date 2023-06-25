namespace Vehicle.InsurancePolicies.Domain.Helpers
{
  public class DateTimeHelper
  {
    public static DateTime GetRandomDate(int yearRange = 10)
    {
      int middle = yearRange / 2;
      DateTime start = new(DateTime.Now.Year - middle, 1, 1);
      DateTime end = DateTime.Now.AddYears(middle);
      Random random = new();
      int range = (end - start).Days;

      return start.AddDays(random.Next(range))
        .AddHours(random.Next(0, 24))
        .AddMinutes(random.Next(0, 60))
        .AddSeconds(random.Next(0, 60));
    }
  }
}
