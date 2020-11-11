using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{
    public class PremiumSubscriptionSlot
    {
        public string id { get; set; }
        public string subscription_id { get; set; }
        public Premium_Guild_Subscription premium_guild_subscription { get; set; }
        public bool canceled { get; set; }
        public string cooldown_ends_at { get; set; }
    }

    public class Premium_Guild_Subscription
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string guild_id { get; set; }
        public bool ended { get; set; }
    }

}
