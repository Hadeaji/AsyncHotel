using AsyncHotel.Data;
using AsyncHotel.Models.DTO;
using AsyncHotel.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Services
{
    public class HotelService : IHotel
    {
        private AsyncHotelDBContext _context;

        public HotelService(AsyncHotelDBContext context)
        {
            _context = context;
        }
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelDTO> GetHotel(int id)
        {

            var hotel = await _context.Hotels
                .Select(hotel => new HotelDTO()
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms
                                    .Select(hotelRoom => new HotelRoomDTO()
                                    {
                                        HotelID = hotelRoom.HotelId,
                                        Rate = hotelRoom.Rate,
                                        PetFriendly = hotelRoom.PetFriendly,
                                        RoomID = hotelRoom.RoomId,
                                        Room = 
                                                new RoomDTO()
                                                {
                                                    ID = hotelRoom.Room.Id,
                                                    Name = hotelRoom.Room.Name,
                                                    Layout = hotelRoom.Room.Layout,
                                                    Amenities = hotelRoom.Room.RoomAmenities
                                                        .Select(roomAmenity => new AmenityDTO()
                                                        {
                                                            ID = roomAmenity.AmenityId,
                                                            Name = roomAmenity.Amenity.Name,
                                                        }).ToList()

                                                }

                                    }).ToList()

                }
                ).FirstOrDefaultAsync(x => x.ID == id);
            return hotel;
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            var list = await _context.Hotels
                .Select(hotel => new HotelDTO()
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms
                                    .Select(hotelRoom => new HotelRoomDTO()
                                    {
                                        HotelID = hotelRoom.HotelId,
                                        Rate = hotelRoom.Rate,
                                        PetFriendly = hotelRoom.PetFriendly,
                                        RoomID = hotelRoom.RoomId,
                                        Room =
                                                new RoomDTO()
                                                {
                                                    ID = hotelRoom.Room.Id,
                                                    Name = hotelRoom.Room.Name,
                                                    Layout = hotelRoom.Room.Layout,
                                                    Amenities = hotelRoom.Room.RoomAmenities
                                                        .Select(roomAmenity => new AmenityDTO()
                                                        {
                                                            ID = roomAmenity.AmenityId,
                                                            Name = roomAmenity.Amenity.Name,
                                                        }).ToList()

                                                }

                                    }).ToList()

                }
                ).ToListAsync();
            return list;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
