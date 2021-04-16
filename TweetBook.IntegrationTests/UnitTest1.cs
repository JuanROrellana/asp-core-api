using System.Net.Http;
using NUnit.Framework;

namespace TweetBook.IntegrationTests
{
    public class Tests
    {
        private readonly HttpClient _client;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}