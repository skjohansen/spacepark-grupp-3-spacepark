using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Models
{
    public class Parkinglot
    {
        [Key]
        public int ParkinglotId { get; set; }
        [MinLength(2,ErrorMessage ="Name should be longer than 2 characters")]
        public string Name { get; set; }
        public ICollection<Parkingspot> Parkingspot { get; set; }

    }
}
