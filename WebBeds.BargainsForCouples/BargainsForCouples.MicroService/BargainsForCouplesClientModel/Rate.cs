namespace BargainsForCouples.MicroService.Model
{
    /// <summary>
    /// Rate related information
    /// </summary>
    public class Rate
    {
        public string RateType { get; set; }

        public string BoardType { get; set; }

        public double Value { get; set; }
    }
}
