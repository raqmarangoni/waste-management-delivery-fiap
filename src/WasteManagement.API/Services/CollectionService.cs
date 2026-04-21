using WasteManagement.API.Repositories;
using WasteManagement.API.ViewModels;
using WasteManagement.API.Models;

namespace WasteManagement.API.Services;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _repo;
    public CollectionService(ICollectionRepository repo) => _repo = repo;

    public async Task<int> CreateAsync(CollectionViewModel vm)
    {
        var model = new Collection
        {
            Location = vm.Location,
            CollectedAt = vm.CollectedAt == default ? DateTime.UtcNow : vm.CollectedAt,
            MaterialType = vm.MaterialType,
            WeightKg = vm.WeightKg
        };
        await _repo.AddAsync(model);
        return model.Id;
    }

    public async Task<CollectionViewModel?> GetByIdAsync(int id)
    {
        var m = await _repo.GetByIdAsync(id);
        if (m == null) return null;
        return new CollectionViewModel
        {
            Id = m.Id,
            Location = m.Location,
            CollectedAt = m.CollectedAt,
            MaterialType = m.MaterialType,
            WeightKg = m.WeightKg
        };
    }

    public async Task<(IEnumerable<CollectionViewModel> items, int total)> GetPagedAsync(int page, int pageSize)
    {
        var items = await _repo.GetPagedAsync(page, pageSize);
        var total = await _repo.GetCountAsync();
        var vms = items.Select(m => new CollectionViewModel
        {
            Id = m.Id,
            Location = m.Location,
            CollectedAt = m.CollectedAt,
            MaterialType = m.MaterialType,
            WeightKg = m.WeightKg
        });
        return (vms, total);
    }
}
