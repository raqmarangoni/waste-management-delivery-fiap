using Microsoft.EntityFrameworkCore;
using WasteManagement.API.Data;
using WasteManagement.API.Models;

namespace WasteManagement.API.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly AppDbContext _db;
    public CollectionRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Collection collection)
    {
        _db.Collections.Add(collection);
        await _db.SaveChangesAsync();
    }

    public async Task<Collection?> GetByIdAsync(int id)
        => await _db.Collections.FindAsync(id);

    public async Task<IEnumerable<Collection>> GetPagedAsync(int page, int pageSize)
        => await _db.Collections
            .OrderByDescending(c => c.CollectedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<int> GetCountAsync()
        => await _db.Collections.CountAsync();
}
