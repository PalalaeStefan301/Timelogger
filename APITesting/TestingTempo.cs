using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;
using System.Net.Http.Json;

namespace APITesting
{
    public class TestingTempo
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public TestingTempo()
        {
            _httpClient = new HttpClient();
            _url = "http://localhost:3001/api";
        }
        [Fact]
        public async void GetTempo_ByProjectId_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("projectId", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var response = await _httpClient.GetAsync(Path.Combine(_url, "tempos", queryBuilder.ToString()));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void GetTempo_ById_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("id", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var response = await _httpClient.GetAsync(Path.Combine(_url, "tempos", queryBuilder.ToString()));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void PostTempo_Hours_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();

            queryDict.Add("usertype", "Freelancer");
            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id= 1,
                ProjectId = 1,
                StartDate = "2024-07-06T15:30:00",
                Hours = 1
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "tempos", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void PostTempo_EndDate_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id = 1,
                ProjectId = 1,
                StartDate = "2024-07-06T15:30:00",
                EndDate = "2024-07-06T16:30:00"
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "tempos", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void PostTempo_EndDate_400()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");

            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id = 1,
                ProjectId = 1,
                StartDate = "2024-07-06T15:30:00",
                EndDate = "2024-07-06T15:30:00"
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "tempos", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}