using DiscordNitroTool.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.Utils
{
    public static class ClientUtils
    {
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(client.BaseAddress + requestUri),
                Content = content,
            };

            return client.SendAsync(request);
        }

        public static bool CanUseBoost(this PremiumSubscriptionSlot subscription)
        {
            var date = subscription.cooldown_ends_at;
            var currentDate = DateTime.UtcNow;
            var year = int.Parse(date.Split('-')[0]);
            var month = int.Parse(date.Split('-')[1]);
            var time = DateTime.Parse(date.Split('T')[1].Replace(date.Split('+')[1], "").Replace("+", ""));
            var day = int.Parse(date.Split('-')[2].Replace(date.Split('T')[1], "").Replace("T", ""));

            if (year < currentDate.Year)
                return true;

            if (month < currentDate.Month)
                return true;

            if (day < currentDate.Day)
                return true;

            //work on time sorting later

            return false;
        }
    }
}
