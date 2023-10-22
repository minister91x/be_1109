using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer.Common
{
    public static class HttpRequest
    {
        public static string WebPost(string url, string baseurl, string jsonData)
        {
            try
            {
                var options = new RestClientOptions(url)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseurl, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddStringBody(jsonData, DataFormat.Json);
                RestResponse response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string WebPostWithToken(string url, string baseurl, string jsonData, string token)
        {
            try
            {
                var options = new RestClientOptions(url)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseurl, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddStringBody(jsonData, DataFormat.Json);
                RestResponse response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
