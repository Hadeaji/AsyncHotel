using AsyncHotel.Data;
using AsyncHotel.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Services
{
    public class RoomService : IRoom
    {
        private AsyncHotelDBContext _context;

        public RoomService(AsyncHotelDBContext context)
        {
            _context = context;
        }
        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(int id)
        {
            var room = await _context.Rooms.Include(x => x.RoomAmenities).ThenInclude(x => x.Amenity).FirstOrDefaultAsync(x => x.Id == id);
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var list = await _context.Rooms.Include(x => x.RoomAmenities).ThenInclude(x => x.Amenity).ToListAsync();

            return list;
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;

        }

        public async Task<RoomAmenity> AddAmenity(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                RoomId = roomId,
                AmenityId = amenityId
            };
            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            await _context.RoomAmenities
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.RoomId == roomId);

            await _context.RoomAmenities
                .Include(x => x.Amenity)
                .FirstOrDefaultAsync(x => x.AmenityId == amenityId);


            return roomAmenity;
        }

        public async Task RemoveAmenity(int roomId, int amenityId)
        {
            var roomAmenity = await _context.RoomAmenities
                .FirstOrDefaultAsync(x => (x.RoomId == roomId) && (x.AmenityId == amenityId));
            
            _context.Entry(roomAmenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
