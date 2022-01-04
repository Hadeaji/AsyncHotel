using AsyncHotel.Data;
using AsyncHotel.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private AsyncHotelDBContext _context;

        public HotelRoomService(AsyncHotelDBContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create([FromRoute]int hotelId, HotelRoom hotelRoom)
        {
            HotelRoom newHotelRoom = new HotelRoom()
            {
                HotelId = hotelId,
                RoomId = hotelRoom.RoomId,
                PetFriendly = hotelRoom.PetFriendly,
                Rate = hotelRoom.Rate
            };
            _context.Entry(newHotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;

            //_context.Entry(hotelRoom).State = EntityState.Added;
            //await _context.SaveChangesAsync();
            //return hotelRoom;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom deleted = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(deleted).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRoom
                            .Where(x => (x.HotelId == hotelId) && (x.RoomId == roomNumber))
                            .FirstOrDefaultAsync();
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var list = await _context.HotelRoom
                .Where(x => x.HotelId == hotelId)
                .ToListAsync();
            return list;
        }

        public async Task<HotelRoom> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
    }
}
