using System.Text.Json.Serialization;

namespace BargainsForCouples.MicroService.Dto
{
    /// <summary>
    /// Rate business object
    /// </summary>
    public class RateBO
    {
        public string RateType { get; set; }

        public string BoardType { get; set; }

        [JsonPropertyName("value")]
        public double FinalPrice { get; set; }
    }
}
