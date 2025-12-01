using System.Net.Http.Json;
using System.Text.Json;
using Playground.Domain.ApiResponses;

public class CurrencyService : ApiServiceBase, ICurrencyService
{
    private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
    private static List<CurrencyResponse>? _cached = null;
    private static DateTime _lastUpdated;

    public CurrencyService(IHttpClientFactory factory) : base(factory) { }

    public async Task<List<CurrencyResponse>> GetCurrencyConversionAsync(CancellationToken ct = default)
    {
        // Check for cache
        if (_cached != null && DateTime.UtcNow - _lastUpdated < TimeSpan.FromMinutes(10))
        {
            Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] Cache HIT (thread {Environment.CurrentManagedThreadId})");
            return _cached;
        }

        Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] Waiting for lock (thread {Environment.CurrentManagedThreadId})");

        await _lock.WaitAsync(ct);

        try
        {
            Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] ENTER lock (thread {Environment.CurrentManagedThreadId})");

            // Double checking for cache
            if (_cached != null && DateTime.UtcNow - _lastUpdated < TimeSpan.FromMinutes(10))
            {
                Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] Cache filled by another thread (thread {Environment.CurrentManagedThreadId})");
                return _cached;
            }

            Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] Fetching from API... (thread {Environment.CurrentManagedThreadId})");

            var result = await Http.GetFromJsonAsync<List<CurrencyResponse>>(
                "currency",
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
                ct
            );

            _cached = result ?? new List<CurrencyResponse>();
            _lastUpdated = DateTime.UtcNow;
            Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] Finished API fetch + cached (thread {Environment.CurrentManagedThreadId})");

            return _cached;
        }
        finally
        {
            Console.WriteLine($"Semaphore test - [{DateTime.Now:HH:mm:ss.fff}] EXIT lock (thread {Environment.CurrentManagedThreadId})");

            _lock.Release();
        }

    }
}