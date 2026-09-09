namespace WebApplication2.Services;

public interface IUniversalReqService
{
    
    Task<TResponse> PostRequest<TRequest,TResponse>( TRequest request);

}