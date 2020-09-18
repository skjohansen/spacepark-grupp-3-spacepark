using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Models
{
    public class Driver
    {
        [Key] 
        public int DriverId { get; set; }

        public string Name { get; set; }
    }
}
