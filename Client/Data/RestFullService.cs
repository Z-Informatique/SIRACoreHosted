using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.Data
{
    public class RestFullService<T>
    {
        private readonly HttpClient _client;
        private readonly IConfiguration configuration;
        private readonly string _weburl;
        public RestFullService(HttpClient client, IConfiguration _config, string url)
        {
            _client = client;
            configuration = _config;
            _weburl = configuration.GetValue<string>("Link:url") + url;
        }
        public async Task<List<T>> GetAsync()
        {
            string json = await _client.GetStringAsync(_weburl);
            List<T> taskModels = JsonConvert.DeserializeObject<List<T>>(json);
            return taskModels;
        }
        public async Task<bool> Delete(int Id)
        {
            HttpResponseMessage result = await _client.DeleteAsync(_weburl + $"/{Id}");
            return result.IsSuccessStatusCode;
        }
        public async Task<List<T>> GetDataByIdAsync(string parameter, int Id)
        {
            string json = await _client.GetStringAsync(_weburl + $"/{parameter}={Id}");
            List<T> taskModels = JsonConvert.DeserializeObject<List<T>>(json);
            return taskModels;
        }
        public async Task<T> Login(string email, string password)
        {
            string result = await _client.GetStringAsync(_weburl + $"/Login?Email=" + email + "&" + "Password=" + password);
            T t = JsonConvert.DeserializeObject<T>(result);
            return t;
        }
        public async Task<T> ChangePassword(string email, string currentPassword, string password)
        {
            //var token = await localStorageService.GetItemAsync<string>("token");
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = await _client.GetStringAsync(_weburl + $"/ChangePassword?email={email}&current={currentPassword}&password={password}");
            T userModel = JsonConvert.DeserializeObject<T>(json);
            return userModel;
        }
        public async Task<T> GetDataByParameter(string url)
        {
            string json = await _client.GetStringAsync(_weburl + $"/{url}");
            T modelData = JsonConvert.DeserializeObject<T>(json);
            return modelData;
        }

        public async Task<List<T>> GetDataListByParameter(string urlParameter)
        {
            string json = await _client.GetStringAsync(_weburl + $"/{urlParameter}");
            List<T> taskModels = JsonConvert.DeserializeObject<List<T>>(json);
            return taskModels;
        }
        public async Task<T> PutAsync(int Id, T t)
        {
            string json = JsonConvert.SerializeObject(t);
            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await _client.PutAsync(_weburl + "/" + Id, content);
            T data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return data;
        }
        public async Task<T> PutAsyncByUrl(string urlParameter, int Id, T t)
        {
            string json = JsonConvert.SerializeObject(t);
            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await _client.PutAsync(_weburl + $"/{urlParameter}/" + Id, content);
            T data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return data;
        }
        public async Task<T> GetById(int Id)
        {
            string result = await _client.GetStringAsync(_weburl + $"/{Id}");
            T t = JsonConvert.DeserializeObject<T>(result);
            return t;
        }
        public async Task<T> GetDataByString(string data)
        {
            string result = await _client.GetStringAsync(_weburl + $"/GetData?data={data}");
            T t = JsonConvert.DeserializeObject<T>(result);
            return t;
        }
        public async Task<T> GetDataByTwoIntParameter(int IdOne, int IdTwo)
        {
            string result = await _client.GetStringAsync(_weburl + $"/GetData?IdOne={IdOne}&IdTwo={IdTwo}");
            T t = JsonConvert.DeserializeObject<T>(result);
            return t;
        }
        public async Task<int> CountAllData(string route)
        {
            string result = await _client.GetStringAsync(_weburl + $"/{route}");
            int count = JsonConvert.DeserializeObject<int>(result);
            return count;
        }
        public async Task<KeyValuePair<string, int>[]> getDataStatistique(string route)
        {
            string response = await _client.GetStringAsync(_weburl + $"/{route}");
            var result = JsonConvert.DeserializeObject<KeyValuePair<string, int>[]>(response);
            return result;
        }
        public async Task<T> PostAsync(T t)
        {
            string json = JsonConvert.SerializeObject(t);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await _client.PostAsync(_weburl, content);
            T data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return data;
        }
    }
}
