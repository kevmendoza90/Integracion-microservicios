

namespace Addresses.Core.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        
        public bool IsActive { get; set; }
    }
}
