using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Models
{
    public class Parkingspot
    {
        [Key]
        public int ParkingspotId { get; set; }
        public int Size { get; set; }
        public bool Occupied { get; set; }
        public Parkinglot parkinglot { get; set; }
    }
}
