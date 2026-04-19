using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetReservations([FromQuery] DateTime? date, [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var query = StaticData.Reservations.AsQueryable();

        if (date.HasValue)
            query = query.Where(r => r.Date.Date == date.Value.Date);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            query = query.Where(r => r.RoomId == roomId.Value);

        return Ok(query.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetReservationById(int id)
    {
        var reservation = StaticData.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound(new { message = $"Reservation with ID {id} not found." });

        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation newReservation)
    {
        var room = StaticData.Rooms.FirstOrDefault(r => r.Id == newReservation.RoomId);
        if (room == null)
            return NotFound(new { message = "Cannot create reservation. Associated room does not exist." });

        if (!room.IsActive)
            return BadRequest(new { message = "Cannot create reservation. Associated room is inactive." });

        var hasOverlap = StaticData.Reservations.Any(r =>
            r.RoomId == newReservation.RoomId &&
            r.Date.Date == newReservation.Date.Date &&
            newReservation.StartTime < r.EndTime &&
            newReservation.EndTime > r.StartTime);

        if (hasOverlap)
            return Conflict(new { message = "The room is already booked for the selected time slot." });

        newReservation.Id = StaticData.Reservations.Any() ? StaticData.Reservations.Max(r => r.Id) + 1 : 1;
        StaticData.Reservations.Add(newReservation);

        return CreatedAtAction(nameof(GetReservationById), new { id = newReservation.Id }, newReservation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
    {
        var existingReservation = StaticData.Reservations.FirstOrDefault(r => r.Id == id);
        if (existingReservation == null)
            return NotFound(new { message = $"Reservation with ID {id} not found." });

        var room = StaticData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
        if (room == null)
            return NotFound(new { message = "Associated room does not exist." });

        if (!room.IsActive)
            return BadRequest(new { message = "Associated room is inactive." });

        var hasOverlap = StaticData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date.Date == updatedReservation.Date.Date &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (hasOverlap)
            return Conflict(new { message = "The room is already booked for the selected time slot." });

        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return Ok(existingReservation);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = StaticData.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound(new { message = $"Reservation with ID {id} not found." });

        StaticData.Reservations.Remove(reservation);
        return NoContent();
    }
}