using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;


namespace PersonalPortfolio.Models
{
    public class Star
    {
        private static readonly HttpClient client = new HttpClient();

        public string name { get; set; }
        public string html_url { get; set; }
        //public void Send()
        //{
        //    var client = new RestClient("https://api.github.com");
        //    var request = new RestRequest("user/hamzilitary/starred.json", Method.POST);
        //    request.AddParameter("Starred", Starred);
        //    client.ExecuteAsync(request, response => {
        //        Console.WriteLine(response.Content);
        //    });
        //}

    public static List<Star> GetStar()
    
       
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/search/repositories?q=user:hamzilitary&sort=stars&order=desc&per_page=3", Method.GET);
            request.AddHeader("User-Agent", "hamzilitary");

            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            List<Star> getStar = JsonConvert.DeserializeObject<List<Star>>(jsonResponse["items"].ToString());
            List<Star> starList = new List<Star> { getStar[0], getStar[1], getStar[2] };

            return starList;
        }


    public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
    {
        var tcs = new TaskCompletionSource<IRestResponse>();
        theClient.ExecuteAsync(theRequest, response => {
            tcs.SetResult(response);
        });
        return tcs.Task;
        }

    }
}
