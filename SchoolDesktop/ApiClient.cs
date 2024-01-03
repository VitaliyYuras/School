using Newtonsoft.Json;
using SchoolDesktop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchoolDesktop
{
    public class ApiClient
    {
        private readonly string baseUri;
        private readonly string authToken;

        public ApiClient(string baseUri, string authToken)
        {
            this.baseUri = baseUri;
            this.authToken = authToken;
        }
        private HttpClient GetAuthorizedClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            return client;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            using (var client = GetAuthorizedClient())
            {
                var response = await client.GetStringAsync($"{baseUri}/api/student");
                return JsonConvert.DeserializeObject<List<Student>>(response);
            }
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            using (var client = GetAuthorizedClient())
            {
                var response = await client.GetStringAsync($"{baseUri}/api/student/{id}");
                return JsonConvert.DeserializeObject<Student>(response);
            }
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            using (var client = GetAuthorizedClient())
            {
                var json = JsonConvert.SerializeObject(student);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUri}/api/student", content);
                response.EnsureSuccessStatusCode();

                var createdStudent = JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());
                return createdStudent;
            }
        }

        public async Task UpdateStudentAsync(Guid id, Student updatedStudent)
        {
            using (var client = GetAuthorizedClient())
            {
                var json = JsonConvert.SerializeObject(updatedStudent);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{baseUri}/api/student/{id}", content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            using (var client = GetAuthorizedClient())
            {
                var response = await client.DeleteAsync($"{baseUri}/api/student/{id}");
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
