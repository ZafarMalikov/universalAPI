namespace WebApplication2.Services;

public class UniversalReqServiceImpl : IUniversalReqService
{
    private readonly HttpClient httpClient;

    public UniversalReqServiceImpl(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<TResponse?> GetRequest<TResponse>(
        string endpoint,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => SendRequest<object?, TResponse>(HttpMethod.Get, endpoint, null, headers, cancellationToken);

    public Task<TResponse?> PostRequest<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => SendRequest<TRequest, TResponse>(HttpMethod.Post, endpoint, request, headers, cancellationToken);

    public Task<TResponse?> PutRequest<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => SendRequest<TRequest, TResponse>(HttpMethod.Put, endpoint, request, headers, cancellationToken);

    public Task<TResponse?> DeleteRequest<TResponse>(
        string endpoint,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => SendRequest<object?, TResponse>(HttpMethod.Delete, endpoint, null, headers, cancellationToken);

    public Task<TResponse?> DeleteRequest<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => SendRequest<TRequest, TResponse>(HttpMethod.Delete, endpoint, request, headers, cancellationToken);

    /// <summary>
    /// Barcha HTTP metodlar uchun umumiy so'rov yuboruvchi yordamchi metod.
    /// </summary>
    private async Task<TResponse?> SendRequest<TRequest, TResponse>(
        HttpMethod method,
        string endpoint,
        TRequest? request,
        Dictionary<string, string>? headers,
        CancellationToken cancellationToken)
    {
        using var httpRequestMessage = new HttpRequestMessage(method, endpoint);

        // GET/DELETE (body'siz) da request null bo'lishi mumkin
        if (request is not null)
        {
            httpRequestMessage.Content = JsonContent.Create(request);
        }

        if (headers is not null)
        {
            foreach (var (key, value) in headers)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation(key, value);
            }
        }

        var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(
                $"So'rov muvaffaqiyatsiz tugadi: {method} {endpoint} -> " +
                $"{(int)response.StatusCode} {response.ReasonPhrase}. Javob: {errorBody}",
                null,
                response.StatusCode);
        }

        // 204 No Content yoki bo'sh javob (masalan, ko'pchilik DELETE so'rovlarida)
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent
            || response.Content.Headers.ContentLength == 0)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }
}