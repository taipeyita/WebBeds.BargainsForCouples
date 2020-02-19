namespace BargainsForCouples.MicroService.Dto
{
    /// <summary>
    /// Hotel business object
    /// </summary>
    public class HotelBO
    {
        public int PropertyID { get; set; }
        public string Name { get; set; }

        public int GeoId { get; set; }

        public double? Rating { get; set; }
    }
}
