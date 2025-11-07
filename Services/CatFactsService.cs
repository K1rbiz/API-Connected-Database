using System.Net.Http.Json;
using API_Connected_Database.Models;

namespace API_Connected_Database.Services;

public interface ICatFactsService
{
    Task<List<CatFact>> GetCatFactsAsync(int limit = 5, CancellationToken ct = default);
}

// This service fetches cat facts from the Cat Facts API
public class CatFactsService(HttpClient http) : ICatFactsService
{
    private const string BaseUrl = "https://catfact.ninja/facts"; // Base URL for the Cat Facts API

    // Keeps track of which "page" was last fetched
    private static int _pageCounter = 1;

    // Fetches cat facts from the API with pagination
    public async Task<List<CatFact>> GetCatFactsAsync(int limit = 5, CancellationToken ct = default)
    {
        // The CatFacts API supports pagination with a 'page' parameter
        var url = $"{BaseUrl}?limit={limit}&page={_pageCounter}"; // Construct the URL with limit and page parameters
        Console.WriteLine($"Fetching Cat Facts from: {url}"); // Log the URL being fetched

        var payload = await http.GetFromJsonAsync<CatFactsResponse>(url, cancellationToken: ct); 

        // Increment page counter for next call
        _pageCounter++;

        // If we've gone too far (past ~30 pages), loop back to start
        if (_pageCounter > 30)
            _pageCounter = 1;

        return payload?.data ?? new List<CatFact>();
    }

    // Internal record type matching API response
    private sealed class CatFactsResponse
    {
        public List<CatFact> data { get; set; } = new(); // List of cat facts returned by the API
    }
}
