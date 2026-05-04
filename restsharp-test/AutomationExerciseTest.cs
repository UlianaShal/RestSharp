using NUnit.Framework;
using RestSharp;
using System.Net;

namespace AutomationExerciseTests
{
    [TestFixture]
    public class AutomationExerciseTest
    {
        private RestClient _client;
        private const string BaseUrl = "https://automationexercise.com/api";

        [OneTimeSetUp]
        public void Setup()
        {
            _client = new RestClient(BaseUrl);
        }

        [Test]
        public void GetAllProductsList()
        {
            var request = new RestRequest("/productsList", Method.Get);
            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Does.Contain("products"));
        }

        [Test]
        public void PostToAllProductsList()
        {
            var request = new RestRequest("/productsList", Method.Post);
            var response = _client.Execute(request);

            Assert.That(response.Content, Does.Contain("This request method is not supported"));
        }

        [Test]
        public void SearchProduct_WithoutParameter()
        {
            var request = new RestRequest("/searchProduct", Method.Post);
            var response = _client.Execute(request);

            Assert.That(response.Content, Does.Contain("Bad request, search_product parameter is missing"));
        }

        [Test]
        public void VerifyLoginWithInvalidCredentials()
        {
            var request = new RestRequest("/verifyLogin", Method.Post);
            
            request.AddParameter("email", "testuser123@example.com");
            request.AddParameter("password", "password123");

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Does.Contain("User not found!"));
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}