namespace WasteManagement.API.ViewModels;

public class CollectionViewModel
{
    public int Id { get; set; }
    public string Location { get; set; } = null!;
    public DateTime CollectedAt { get; set; }
    public string? MaterialType { get; set; }
    public double WeightKg { get; set; }
}
