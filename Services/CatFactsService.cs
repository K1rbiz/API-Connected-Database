using System.Net.Http.Json;
using API_Connected_Database.Models;

namespace API_Connected_Database.Services;

public interface ICatFactsService
{
    Task<List<CatFact>> GetCatFactsAsync(int limit = 5, CancellationToken ct = default);
}

public class CatFactsService(HttpClient http) : ICatFactsService
{
    private const string BaseUrl = "https://catfact.ninja/facts";

    // Keeps track of which "page" was last fetched
    private static int _pageCounter = 1;

    public async Task<List<CatFact>> GetCatFactsAsync(int limit = 5, CancellationToken ct = default)
    {
        // The CatFacts API supports pagination with a 'page' parameter
        var url = $"{BaseUrl}?limit={limit}&page={_pageCounter}";
        Console.WriteLine($"Fetching Cat Facts from: {url}");

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
        public List<CatFact> data { get; set; } = new();
    }
}
