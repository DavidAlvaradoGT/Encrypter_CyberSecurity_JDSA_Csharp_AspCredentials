using System;
using System.Net;
using System.Net.Http;

namespace Encrypter_JDSA
{
    public static class NetworkTimeHelper
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(4)
        };
        public static DateTime GetInternetDateTime()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            string url = "https://www.Google.com";
           
                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Head, url))
                    using (var response = httpClient.SendAsync(request).GetAwaiter().GetResult())
                    {
                        if (response.Headers.Date.HasValue)
                        {
                            DateTimeOffset utcDate = response.Headers.Date.Value;
                            return TimeZoneInfo.ConvertTimeFromUtc(utcDate.UtcDateTime, TimeZoneInfo.Local);
                        }
                    }
                }
                catch
                {
                    // Try & Catch
                }

            
            return DateTime.Now;
        }
    }
}
