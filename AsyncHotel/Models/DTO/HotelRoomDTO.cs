namespace AsyncHotel.Models.DTO
{
    public class HotelRoomDTO
    {
        public int HotelID { get; set; }
        public string Rate { get; set; }
        public string PetFriendly { get; set; }
        public int RoomID { get; set; }
        public RoomDTO Room { get; set; }

    }
}