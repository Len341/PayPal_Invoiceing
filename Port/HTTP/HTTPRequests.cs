using RestSharp;

namespace HTTP
{
    class Requests
    {
        public static IRestResponse auth(RestClient client)
        {
            var request = new RestRequest("oauth2/token", Method.POST, DataFormat.Xml);
            request.AddParameter("grant_type", "client_credentials");

            return client.Execute(request);
        }
    }
}
