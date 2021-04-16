using System.Threading.Tasks;
using Refit;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Sdk
{
    public interface IIdentityApi
    {
        [Post("/api/v1/identity/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest registrationRequest);
        
        [Post("/api/v1/identity/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest userLoginRequest);
        
        [Post("/api/v1/identity/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshToken);
        
        [Get("/api/v1/identity/test")]
        Task<ApiResponse<AuthSuccessResponse>> TestAsync();
    }
}