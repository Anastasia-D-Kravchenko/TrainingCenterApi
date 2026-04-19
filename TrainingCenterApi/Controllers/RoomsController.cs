using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
    {
        var query = StaticData.Rooms.AsQueryable();
        
        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity.Value);
            
        if (hasProjector.HasValue)
            query = query.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly.HasValue && activeOnly.Value)
            query = query.Where(r => r.IsActive);

        return Ok(query.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetRoomById(int id)
    {
        var room = StaticData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound(new { message = $"Room with ID {id} not found." });

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomsByBuilding(string buildingCode)
    {
        var rooms = StaticData.Rooms.Where(r => r.BuildingCode.Equals(buildingCode, System.StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room newRoom) 
    {
        newRoom.Id = StaticData.Rooms.Any() ? StaticData.Rooms.Max(r => r.Id) + 1 : 1;
        StaticData.Rooms.Add(newRoom);

        return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, newRoom);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
    {
        var existingRoom = StaticData.Rooms.FirstOrDefault(r => r.Id == id);
        if (existingRoom == null)
            return NotFound(new { message = $"Room with ID {id} not found." });

        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingCode = updatedRoom.BuildingCode;
        existingRoom.Floor = updatedRoom.Floor; 
        existingRoom.Capacity = updatedRoom.Capacity; 
        existingRoom.HasProjector = updatedRoom.HasProjector;
        existingRoom.IsActive = updatedRoom.IsActive;
        
        return Ok(existingRoom);
    }
    
    [HttpDelete("{id}")] 
    public IActionResult DeleteRoom(int id) 
    { 
        var room = StaticData.Rooms.FirstOrDefault(r => r.Id == id); 
        if (room == null) 
            return NotFound(new { message = $"Room with ID {id} not found." });
        
        if (StaticData.Reservations.Any(r => r.RoomId == id)) 
            return Conflict(new { message = "Cannot delete room. There are reservations associated with it." });

        StaticData.Rooms.Remove(room); 
        return NoContent();
    }
}