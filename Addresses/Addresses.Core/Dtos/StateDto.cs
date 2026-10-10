using Addresses.Domain.Models;
using System.Collections.Generic;


namespace Addresses.Core.Dtos
{
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public List<City> Cities { get; set; }
    }
}
