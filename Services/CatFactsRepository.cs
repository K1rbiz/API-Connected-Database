using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using API_Connected_Database.Models;

namespace API_Connected_Database.Services;

// Repository for managing CatFact entities in a SQLite database
public interface ICatFactsRepository
{
    Task InitAsync(); // Initialize the database and create the table if it doesn't exist
    Task InsertAllAsync(IEnumerable<CatFact> facts); // Insert multiple CatFact records into the database
    Task<List<CatFact>> GetAllAsync(); // Retrieve all CatFact records from the database
    Task ClearAsync(); // Clear all CatFact records from the database
}

// Implementation of the CatFactsRepository
public class CatFactsRepository : ICatFactsRepository
{
    private readonly string _dbPath; // Path to the SQLite database file
    private SQLiteAsyncConnection? _conn; // SQLite asynchronous connection


    // Constructor to initialize the repository with the database path
    public CatFactsRepository(string dbPath)
    {
        _dbPath = dbPath; // Set the database path
    }

    // Initialize the database and create the CatFact table if it doesn't exist
    public async Task InitAsync()
    {
        if (_conn is not null) return; // If already initialized, return
        _conn = new SQLiteAsyncConnection(_dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache); // Create a new SQLite connection
        await _conn.CreateTableAsync<CatFact>(); // Create the CatFact table
    }

    // Insert multiple CatFact records into the database
    public async Task InsertAllAsync(IEnumerable<CatFact> facts)
    {
        if (_conn is null) await InitAsync(); // Ensure the database is initialized
        await _conn!.RunInTransactionAsync(tran => // Run the insertion in a transaction
        {
            foreach (var f in facts)
                tran.Insert(f);
        });
    }

    // Retrieve all CatFact records from the database
    public async Task<List<CatFact>> GetAllAsync()
    {
        if (_conn is null) await InitAsync(); // Ensure the database is initialized
        return await _conn!.Table<CatFact>().OrderByDescending(f => f.Id).ToListAsync(); // Return all CatFact records ordered by Id in descending order
    }

    // Clear all CatFact records from the database
    public async Task ClearAsync()
    {
        if (_conn is null) await InitAsync(); // Ensure the database is initialized
        await _conn!.DeleteAllAsync<CatFact>(); // Delete all records from the CatFact table
    }
}
