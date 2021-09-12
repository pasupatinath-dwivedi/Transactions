using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

namespace Transactions.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static T FromBody<T>(this HttpRequest request)
                    where T : class, new()
        {
            var content = new StreamReader(request.Body).ReadToEnd();
            if (content.Length == 0)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
