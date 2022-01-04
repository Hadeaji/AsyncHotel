namespace AsyncHotel.Models
{
    public class HotelRoom
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string Rate { get; set; }
        public string PetFriendly { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }

    }
}
