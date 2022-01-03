using AsyncHotel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Data
{
    public class AsyncHotelDBContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }

        public AsyncHotelDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoomAmenity>().HasKey((roomAmenity) => new { roomAmenity.AmenityId, roomAmenity.RoomId });

            modelBuilder.Entity<HotelRoom>().HasKey((hotelRoom) => new { hotelRoom.HotelId, hotelRoom.RoomId });


            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Id = 1, Name = "Lablas", StreetAddress = "Somewhere", City = "Nevada", State = "Nevada", Country = "Nevada", Phone ="911" }
            );
            modelBuilder.Entity<Room>().HasData(
              new Room { Id = 1, Name = "BigMac", Layout = 1 }
            );
            modelBuilder.Entity<Amenity>().HasData(
              new Amenity { Id = 1, Name = "Dunno" }
            );
        }


    }
}
