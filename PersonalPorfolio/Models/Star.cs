using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PersonalPortfolio.Models
{
    public class Star
    {
        public string Repository { get; set; }
    }

    public static List<Star> GetStar()
    {
        var client = new RestClient("url goes here");
        var request = new RestRequest("request", Method.GET );
        client.Authentication = new HttpBasicAuthentication("{{}}", "{ { } }");
        var response = new RestResponse();
        Task.Run(async () =>
        {
            response = await GetResponseContentAsync(client, request) as RestResponse;
        }).Wait();
        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
        var starList = JsonConvert.DeserializeObject<List<Star>>(jsonResponse["?"].ToString());
        return starList;
    }

    public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
    {
        var tcs = new TaskCompletionSource<IRestResponse>();
        theClient.ExecuteAsync(theRequest, response =>
        {
            tcs.SetResult(response);
        });
        return tcs.Task;
    }
}
