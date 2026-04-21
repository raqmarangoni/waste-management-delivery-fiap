using System.ComponentModel.DataAnnotations;

namespace WasteManagement.API.Models;

public class Collection
{
    public int Id { get; set; }
    [Required]
    public string Location { get; set; } = null!;
    public DateTime CollectedAt { get; set; }
    public string? MaterialType { get; set; }
    public double WeightKg { get; set; }
}
