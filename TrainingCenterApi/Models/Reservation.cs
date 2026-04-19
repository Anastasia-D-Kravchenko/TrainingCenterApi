using System.ComponentModel.DataAnnotations;

namespace TrainingCenterApi.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [Required(ErrorMessage = "Organizer name is required.")]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Topic is required.")]
    public string Topic { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string Status { get; set; } = "planned";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be later than StartTime.",
                new[] { nameof(EndTime) });
        }
    }
}