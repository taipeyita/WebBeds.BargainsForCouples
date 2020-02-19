using System.Collections.Generic;

namespace BargainsForCouples.MicroService.Dto
{
    /// <summary>
    /// Property business oject which holds the complete response data
    /// </summary>
    public class PropertyBO
    {
        public HotelBO Hotel { get; set; }
        public List<RateBO> Rates { get; set; }
    }
}
