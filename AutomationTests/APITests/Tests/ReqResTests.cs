using AutomationTests.APITests.Models;
using RestSharp;
using NLog;

namespace AutomationTests.APITests.Tests
{
    public class ReqResTests
    {
        private APIService.APIService _apiClient;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = DependencyRegister.GetService<ILogger>();
            _apiClient = DependencyRegister.GetService<APIService.APIService>();
            _apiClient.SetBaseUrl(Config.BaseAPIUrl);
        }

        [Test]
        public void VerifyFetchingUserList()
        {
            _logger.Info("***** Get users endpoint test *****");

            _apiClient.SetEndpointAndMethod("api/users?page=2", Method.Get);
            _apiClient.ExecuteRequest();

            _apiClient.VerifyResponceStatusCode(200);
            _apiClient.VerifyUsersInformationExistsInResponse();
            _apiClient.VerifyIfFirstUserIdIsNotEmpty();
        }

        [Test]
        [TestCase("John Smith", "QA Engineer")]
        public void VerifyCreatingNewUser(string name, string job)
        {
            _logger.Info("***** Create user endpoint test *****");

            var body = new CreateUser { Name = name, Job = job };
            _apiClient.SetEndpointAndMethod("api/users", Method.Post);
            _apiClient.AddBody(body);
            _apiClient.ExecuteRequest();

            _apiClient.VerifyResponceStatusCode(201);
            _apiClient.VerifyThatCreateUserResponseContainValidData(body);
        }
    }
}
