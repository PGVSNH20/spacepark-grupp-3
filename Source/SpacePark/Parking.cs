using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark
{
    public class Parking
    {
        public int Id { get; set; }
        public string Spaceship { get; set; }
        public string Name { get; set; }
        public DateTime ParkingStart { get; set; }
        public DateTime ParkingEnd { get; set; }
        public decimal Payment { get; set; }
    }
}
