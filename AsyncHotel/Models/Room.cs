using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Layout { get; set; }
        public List<RoomAmenity> RoomAmenities { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}
