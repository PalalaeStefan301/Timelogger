using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;
using System.Net.Http.Json;

namespace APITesting
{
    public class TestingProject
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public TestingProject()
        {
            _httpClient = new HttpClient();
            _url = "http://localhost:3001/api";
        }
        [Fact]
        public async void GetProject_Freelancer_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("id", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var response = await _httpClient.GetAsync(Path.Combine(_url, "projects", queryBuilder.ToString()));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void PostProject_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("id", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id= 1,
                Name= "test",
                Deadline= "2024-07-06T15:30:00"
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "projects", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void PostProject__Name_400()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("id", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id = 1,
                Deadline = "2024-07-06T15:30:00"
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "projects", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void PostProject_Deadline_400()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("id", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id = 1,
                Name = "test3"
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "projects", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void PostProject_SameName_409()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("id", "1");
            var queryBuilder = new QueryBuilder(queryDict);

            var obj = new
            {
                Id = 1,
                Name = "test",
                Deadline = "2024-07-06T15:30:00"
            };

            JsonContent content = JsonContent.Create(obj);

            var response = await _httpClient.PostAsync(Path.Combine(_url, "projects", queryBuilder.ToString()), content);
            Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);
        }
        [Fact]
        public async void GetProjects_Ordered_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            var queryBuilder = new QueryBuilder(queryDict);

            var response = await _httpClient.GetAsync(Path.Combine(_url, "projects/GetProjectsOrdered", queryBuilder.ToString()));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void GetProjects_OrderedDesc_200()
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            queryDict.Add("usertype", "Freelancer");
            queryDict.Add("orderDesc", "true");
            var queryBuilder = new QueryBuilder(queryDict);

            var response = await _httpClient.GetAsync(Path.Combine(_url, "projects/GetProjectsOrdered", queryBuilder.ToString()));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}