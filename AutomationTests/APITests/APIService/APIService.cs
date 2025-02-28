using AutomationTests.APITests.Models;
using Newtonsoft.Json;
using NLog;
using RestSharp;

namespace AutomationTests.APITests.APIService
{
    public class APIService
    {
        private readonly ILogger _logger;
        private readonly RestRequest _request;
        private RestResponse _response;
        private RestClient _client;

        public APIService()
        {
            _logger = DependencyRegister.GetService<ILogger>();
            _request = new RestRequest();
        }

        public APIService SetBaseUrl(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            return this;
        }

        public void SetEndpointAndMethod(string endpoint, Method method)
        {
            _request.Resource = endpoint;
            _request.Method = method;
            _logger.Info($"Creating {method} request to {endpoint} endpoint.");
        }

        public void AddBody(object body)
        {
            _request.AddBody(body);
            _logger.Info($"Request body was added.");
        }

        public RestResponse ExecuteRequest()
        {            
            _response = _client.Execute(_request);
            _logger.Info($"Request was executed.");
            return _response;
        }

        public T GetResponseBody<T>()
        {
            return JsonConvert.DeserializeObject<T>(_response.Content);
        }

        public void VerifyResponceStatusCode(int expectedStatusCode)
        {
            var actualStatusCode = (int)_response.StatusCode;
            _logger.Info($"Verify response status code is {expectedStatusCode}.");
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "details: {0}", _response.Content);
        }

        public void VerifyUsersInformationExistsInResponse()
        {
            var countOfUsers = GetResponseBody<GetUsers>().Data.Count;
            _logger.Info($"Checking if user info is present in the response.");
            Assert.Greater(countOfUsers, 0, "details: {0}", _response.Content);
        }

        public void VerifyIfFirstUserIdIsNotEmpty()
        {
            _logger.Info($"Checking if first user id is not empty.");
            Assert.IsNotEmpty(GetResponseBody<GetUsers>().Data.FirstOrDefault()?.Id.ToString(), "details: {0}", _response.Content);
        }

        public void VerifyThatCreateUserResponseContainValidData(CreateUser expectedUserInfo)
        {
            var actualUserInfo = GetResponseBody<CreateUserResponse>();
            _logger.Info($"Verifying if user was created with correct information.");
            Assert.AreEqual(expectedUserInfo.Name, actualUserInfo.Name, "Name mismatch");
            Assert.AreEqual(expectedUserInfo.Job, actualUserInfo.Job, "Job mismatch");
            Assert.IsNotEmpty(actualUserInfo.Id.ToString(), "ID is missing");
        }
    }
}
