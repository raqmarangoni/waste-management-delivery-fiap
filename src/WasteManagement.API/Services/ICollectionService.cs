using WasteManagement.API.ViewModels;

namespace WasteManagement.API.Services;

public interface ICollectionService
{
    Task<(IEnumerable<CollectionViewModel> items, int total)> GetPagedAsync(int page, int pageSize);
    Task<CollectionViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(CollectionViewModel vm);
}
