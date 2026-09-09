namespace WebApplication2.Services;

public class UniversalReqServiceImpl(HttpClient httpClient) : IUniversalReqService
    
{
    public Task<TResponse> PostRequest<TRequest, TResponse>(TRequest request)
    {
        throw new NotImplementedException();
    }
}