using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using API_Connected_Database.Models;

namespace API_Connected_Database.Services;

public interface ICatFactsRepository
{
    Task InitAsync();
    Task InsertAllAsync(IEnumerable<CatFact> facts);
    Task<List<CatFact>> GetAllAsync();
    Task ClearAsync();
}

public class CatFactsRepository : ICatFactsRepository
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection? _conn;

    public CatFactsRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public async Task InitAsync()
    {
        if (_conn is not null) return;
        _conn = new SQLiteAsyncConnection(_dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        await _conn.CreateTableAsync<CatFact>();
    }

    public async Task InsertAllAsync(IEnumerable<CatFact> facts)
    {
        if (_conn is null) await InitAsync();
        await _conn!.RunInTransactionAsync(tran =>
        {
            foreach (var f in facts)
                tran.Insert(f);
        });
    }

    public async Task<List<CatFact>> GetAllAsync()
    {
        if (_conn is null) await InitAsync();
        return await _conn!.Table<CatFact>().OrderByDescending(f => f.Id).ToListAsync();
    }

    public async Task ClearAsync()
    {
        if (_conn is null) await InitAsync();
        await _conn!.DeleteAllAsync<CatFact>();
    }
}
