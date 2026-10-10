using Addresses.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addresses.Domain.Models
{
    public class City :BaseEntity
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<Position> Locations { get; set; }
    }
}
