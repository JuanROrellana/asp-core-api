using System;
using System.Threading.Tasks;
using Refit;
using TweetBook.Contracts.V1.Requests;

namespace TweetBook.Sdk.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var serverUrl = "https://localhost:5001";
            var token = string.Empty;

            var identityApi = RestService.For<IIdentityApi>(serverUrl);
            var postApi = RestService.For<IPostApi>(serverUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(token)
            });
            
            var loginResponse = await identityApi.LoginAsync(new UserLoginRequest()
            {
                Email = "admin@admin.com",
                Password = "Password123..."
            });
            
            token = loginResponse.Content.Token;

            var post = await postApi.GetPostAsync(new Guid("EDF5A2B6-C41E-4AAD-A687-74E53DAF3D92"));
        }
    }
}