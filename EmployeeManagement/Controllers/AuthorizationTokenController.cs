using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationTokenController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            var client = new RestClient("https://sairam-kunche.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"pY15vtMSwZUB064oKFq7DJ1Ns2U5femF\",\"client_secret\":\"UfRo_iombA5oW10WOFDgjtynYCJnmYKaqCZxnYM5dyzCe7zsoYT1dj68O5rqIkc9\",\"audience\":\"https://employee-management/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var tokenParams = JsonConvert.DeserializeObject<Dictionary<string, string >>(response.Content);
            return Ok(tokenParams["token_type"] + " " + tokenParams["access_token"]);
        }
    }
}
