

namespace Business.Core.Dtos
{
    public class CustomerContactsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public int CustomerId { get; set; }
        public bool IsActive { get; set; }

        



    }
}
