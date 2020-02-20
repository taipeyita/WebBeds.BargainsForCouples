namespace BargainsForCouples.MicroService.Model
{
    /// <summary>
    /// Hotel related information 
    /// </summary>
    public class Hotel
    {
        public int PropertyID { get; set; }
        public string Name { get; set; }

        public int GeoId { get; set; }

        public double? Rating { get; set; }
    }
}
