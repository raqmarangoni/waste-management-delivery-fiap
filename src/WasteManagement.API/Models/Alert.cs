using System.ComponentModel.DataAnnotations;

namespace WasteManagement.API.Models;

public class Alert
{
    public int Id { get; set; }
    [Required]
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Resolved { get; set; }
}
