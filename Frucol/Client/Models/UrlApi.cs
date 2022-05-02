using System;
using System.Net.Http;

namespace Frucol.Client.Models
{
    public class UrlApi
    {
        HttpClient http;

        public UrlApi()
        {
            http.BaseAddress = new Uri("https://localhost:5001");
        }
    }
}
