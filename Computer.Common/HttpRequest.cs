using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Computer.Common
{
    public static class HttpRequest
    {

        public static string WebGet()
        {
            try
            {
                //var options = new RestClientOptions("https://hoadondientu.gdt.gov.vn:30000")
                //{
                //    MaxTimeout = -1,
                //};
                //var client = new RestClient(options);
                //var request = new RestRequest("/captcha", Method.Get);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddStringBody("", DataFormat.Json);
                //var response = client.ExecuteAsync(request).Result;
                //return response.Content;

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://hoadondientu.gdt.gov.vn:30000/captcha");
                request.Headers.Add("Cookie", "TS01c977ee=01dc12c85e81d749f36bacf1c03126ea70cd39d4537eb00d70690c173e158c98ccc19b31ce244e83e23ddb7ea4fc8bc7e7a74b0c91");
                //var content = new StringContent("", null, "text/plain");
                //request.Content = content;
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }



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
