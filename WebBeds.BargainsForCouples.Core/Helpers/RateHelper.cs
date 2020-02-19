namespace WebBeds.BargainsForCouples.Core.Helpers
{
    /// <summary>
    /// Rate Static class to calculate the final price
    /// </summary>
    public static class RateHelper
    {
        public static double Calculate(string rateType, int nights, double value)
        {
            return rateType.ToLower() == "pernight" ? (value * nights) : value;
        }
    }
}
