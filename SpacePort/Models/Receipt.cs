using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Models
{
    public class Receipt
    {
        [Key] 
        public int ReceiptId { get; set; }

        public int Price { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public Parkingspot Parkingspot { get; set; }
        public Driver Driver { get; set; }
    }
}
