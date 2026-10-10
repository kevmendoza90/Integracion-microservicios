

namespace Business.Core.Dtos
{
    public class CustomerDto 
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CustomerTypeId { get; set; }
        public int CompanyId { get; set; }
        public int? LocationId { get; set; }
        public bool IsActive { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}
