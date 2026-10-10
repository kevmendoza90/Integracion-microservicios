using Addresses.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Addresses.Domain.Models
{
    public class Position :BaseEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        [JsonIgnore]
        public City City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CustomerId { get; set; }


    }
}
