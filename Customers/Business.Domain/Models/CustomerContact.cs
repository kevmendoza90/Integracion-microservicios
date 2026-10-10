using Business.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace Business.Domain.Models
{
    public class CustomerContact :BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        
        
}
}
