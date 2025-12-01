public abstract class ApiServiceBase
{
    protected readonly HttpClient Http;

    protected ApiServiceBase(IHttpClientFactory factory)
    {
        Http = factory.CreateClient("ApiClient");
    }
}