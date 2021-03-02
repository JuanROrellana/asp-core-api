using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IIdentityService
    {
         Task<AuthenticationResult> RegisterAsync(string Email, string Password);
    }
}