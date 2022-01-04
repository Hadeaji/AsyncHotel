using AsyncHotel.Data;
using AsyncHotel.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Services
{
    public class AmenityService : IAmenity
    {
        private AsyncHotelDBContext _context;

        public AmenityService(AsyncHotelDBContext context)
        {
            _context = context;
        }
        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenity deleted = await GetAmenity(id);
            _context.Entry(deleted).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Amenity> GetAmenity(int id)
        {
            var amenity = await _context.Amenities.Include(x => x.Rooms).ThenInclude(x => x.Room).FirstOrDefaultAsync( x => x.Id == id);
            return amenity;
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var list = await _context.Amenities.Include(x => x.Rooms).ThenInclude(x => x.Room).ToListAsync();
            return list;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
