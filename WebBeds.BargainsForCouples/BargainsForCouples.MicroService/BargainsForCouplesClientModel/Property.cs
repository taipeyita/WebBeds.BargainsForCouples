using System.Collections.Generic;

namespace BargainsForCouples.MicroService.Model
{
    /// <summary>
    /// Contains the complete response data
    /// </summary>
    public class Property
    {
        public Hotel Hotel { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
