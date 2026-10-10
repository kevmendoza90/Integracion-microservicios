

namespace Business.Core.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sigla { get; set; }
        public string MainEmail { get; set; }
        public bool IsActive { get; set; }
    }
}
