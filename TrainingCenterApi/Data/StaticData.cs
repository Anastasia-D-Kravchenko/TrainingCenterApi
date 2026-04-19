using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data;

public static class StaticData
{
    public static List<Room> Rooms { get; set; } = new List<Room>
    {
        new Room { Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Lecture Hall", BuildingCode = "A", Floor = 1, Capacity = 100, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "Meeting Room 1", BuildingCode = "B", Floor = 2, Capacity = 10, HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "Computer Lab", BuildingCode = "C", Floor = 3, Capacity = 25, HasProjector = true, IsActive = false },
        new Room { Id = 5, Name = "Workshop Space", BuildingCode = "B", Floor = 1, Capacity = 40, HasProjector = true, IsActive = true }
    };

    public static List<Reservation> Reservations { get; set; } = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "John Doe", Topic = "C# Basics", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Jane Smith", Topic = "Tech Keynote", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(15, 0, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 1, OrganizerName = "Anna Devil", Topic = "Advanced LINQ", Date = new DateTime(2026, 5, 11), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(13, 0, 0), Status = "confirmed" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Mark Lee", Topic = "Scrum Daily", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(9, 30, 0), Status = "canceled" }
    };
}