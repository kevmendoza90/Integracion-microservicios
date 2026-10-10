using Business.Domain.Base;


namespace Business.Domain.Models
{
    public class Company :BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sigla { get; set; }
        public string MainEmail { get; set; }
        
    }
}
