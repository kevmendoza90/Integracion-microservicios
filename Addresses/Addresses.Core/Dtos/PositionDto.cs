

namespace Addresses.Core.Dtos
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }
    }
}
