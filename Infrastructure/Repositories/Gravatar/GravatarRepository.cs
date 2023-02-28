using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class GravatarRepository : IGravatarRepository
{
    private readonly RestClient client;

    public GravatarRepository(RestClient client)
    {
        this.client = client;
    }

    public byte[] getGravatar(string id)
    {
        string url = $"https://www.gravatar.com/avatar/{id}?s=32&d=identicon&r=PG";
        RestRequest request = new RestRequest(url, Method.Get);

        var response = client.DownloadDataAsync(request);
        response.Wait();

        return response.Result;

    }
}
