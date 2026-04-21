using WasteManagement.API.Models;

namespace WasteManagement.API.Repositories;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetPagedAsync(int page, int pageSize);
    Task<int> GetCountAsync();
    Task<Collection?> GetByIdAsync(int id);
    Task AddAsync(Collection collection);
}
