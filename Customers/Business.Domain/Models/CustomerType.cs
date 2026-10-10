using Business.Domain.Base;


namespace Business.Domain.Models
{
    public class CustomerType :BaseEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        
    }
}