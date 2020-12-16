using OverviewRkiData.Components.Data;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OverviewRkiData.Components.Service
{
    public class ServiceConnector : IServiceConnector
    {
        private static ServiceConnector _instance;


        public string ServerAddress { get; private set; }

        private ServiceConnector(string serverAddress)
        {
            this.ServerAddress = serverAddress;
        }

        public static ServiceConnector GetInstance(string serverAddress)
        {
            if (_instance != null && !_instance.ServerAddress.Equals(serverAddress))
            {
                _instance = null;
            }

            if (_instance == null)
            {
                _instance = new ServiceConnector(serverAddress);
            }

            return _instance;
        }

        public void SetAddress(string serverAddres) => this.ServerAddress = serverAddres;

        private string TryDownloadString(string urlString)
        {
            var client = new WebClient
            {
                BaseAddress = this.ServerAddress
            };

            client.Credentials = CredentialCache.DefaultCredentials;

            string result = string.Empty;

            try
            {
                result = client.DownloadString(urlString);
            }
            catch (WebException webException)
            {
                Console.WriteLine($"Kann keine Verbindung zu {urlString} herstellen");
                Console.WriteLine(webException.Message);
            }

            return result;
        }

        private async Task<UploadResult> TrySendRequest(string url, RequestMethod method, IRequest request)
        {
            var result = new UploadResult();
            using var client = new WebClient
            {
                BaseAddress = this.ServerAddress
            };

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Credentials = CredentialCache.DefaultCredentials;

            var jsonData = JsonConvert.SerializeObject(request);

            try
            {
                result.Content = client.UploadString(new Uri(url), this.GetMethodString(method), jsonData);

                result.Success = true;
                return await Task.FromResult(result);
            }
            catch (WebException webException)
            {
                Console.WriteLine(webException.Message);
                result.Content = webException.Message;
                result.IsConnectionError = true;
            }

            return await Task.FromResult(result);
        }

        private string GetMethodString(RequestMethod method)
        {
            return method == RequestMethod.Post ? "POST" : "UPDATE";
        }

        public async Task<IResponse> AddDvdItem(IRequest request)
        {
            string url = $"{this.ServerAddress}/dvdarchive";

            var responseMessage = await this.TrySendRequest(url, RequestMethod.Post, request);

            if (responseMessage.Success)
            {
                return JsonConvert.DeserializeObject<Response>(responseMessage.Content);
            }

            return await Task.FromResult(new Response() { Success = false, Message = responseMessage.Content, IsConnectionError = responseMessage.IsConnectionError });
        }

        public async Task<IResponse> GetDvdItem(long id)
        {
            string urlQuery = $"{this.ServerAddress}/dvdarchive";

            if (id != 0)
            {
                urlQuery += $"?id={id}";
            }

            using var client = new WebClient
            {
                BaseAddress = this.ServerAddress
            };

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                var result = client.DownloadString(urlQuery);
                return await Task.FromResult(JsonConvert.DeserializeObject<Response>(result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response() { Success = false, Message = $"ERROR: {ex}" });
            }
        }

        public async Task<IResponse> GetDvdItems()
        {
            return await this.GetDvdItem(0);
        }

        public async Task<IResponse> EditDvdItem(IRequest request)
        {
            string url = $"{this.ServerAddress}/dvdarchive";

            var responseObj = await this.TrySendRequest(url, RequestMethod.Update, request);

            if (responseObj.Success)
            {
                return JsonConvert.DeserializeObject<Response>(responseObj.Content);
            }

            return new Response() { Success = false, Message = responseObj.Content, IsConnectionError = responseObj.IsConnectionError };
        }
    }
}
