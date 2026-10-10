using Addresses.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Addresses.Domain.Models
{
    public class Country:BaseEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
