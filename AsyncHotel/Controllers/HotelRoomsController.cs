using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncHotel.Data;
using AsyncHotel.Models;
using AsyncHotel.Models.Interfaces;

namespace AsyncHotel.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        // GET: api/HotelRooms
        [HttpGet("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelRoom.GetHotelRoom(hotelId, roomNumber);
            return Ok(hotelRoom);
        }

        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var list = await _hotelRoom.GetHotelRooms(hotelId);
            return Ok(list);
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            if (hotelId != hotelRoom.HotelId || roomNumber != hotelRoom.RoomId)
            {
                return BadRequest();
            }

            var updatedHotelRoom = await _hotelRoom.UpdateHotelRoom(hotelId, roomNumber,hotelRoom);

            return Ok(updatedHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom([FromRoute] int hotelId, HotelRoom hotelRoom)
        {
            var newHotelRoom = await _hotelRoom.Create(hotelId, hotelRoom);
            return CreatedAtAction("GetHotelRoom", new { hotelId = hotelId, roomNumber = hotelRoom.RoomId }, newHotelRoom);
 
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(hotelId, roomNumber);
            return NoContent();
        }

    }
}
