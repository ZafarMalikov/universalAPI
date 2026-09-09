namespace WebApplication2.Services;

public interface IUniversalReqService
{
    Task<TResponse?> GetRequest<TResponse>(
        string endpoint,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default);

    Task<TResponse?> PostRequest<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default);

    Task<TResponse?> PutRequest<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default);

    Task<TResponse?> DeleteRequest<TResponse>(
        string endpoint,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default);

    Task<TResponse?> DeleteRequest<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default);
}