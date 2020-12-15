using System.IO;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace HTTP
{
    class Client
    {
        public static RestClient init()
        {
            var (clientId, secret) = getCredentials();
            var client = new RestClient("https://api-m.sandbox.paypal.com/v1");
            client.Authenticator = new HttpBasicAuthenticator(clientId, secret);

            return client;
        }

        private static (string, string) getCredentials() {
            JObject config = JObject.Parse(File.ReadAllText("config.json"));
            
            var clientId = config.Value<string>("clientId");
            var secret = config.Value<string>("secret");

            return (clientId, secret);
        }

    }
}
