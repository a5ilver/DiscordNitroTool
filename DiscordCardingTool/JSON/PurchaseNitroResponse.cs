using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordCardingTool.JSON
{

    public class PurchaseNitroResponse
    {
        public Entitlement[] entitlements { get; set; }
        public string gift_code { get; set; }
    }

    public class Entitlement
    {
        public string id { get; set; }
        public string sku_id { get; set; }
        public string application_id { get; set; }
        public string user_id { get; set; }
        public int type { get; set; }
        public bool deleted { get; set; }
        public bool consumed { get; set; }
        public Subscription_Plan subscription_plan { get; set; }
    }

    public class Subscription_Plan
    {
        public string id { get; set; }
        public string name { get; set; }
        public int interval { get; set; }
        public int interval_count { get; set; }
        public bool tax_inclusive { get; set; }
        public string sku_id { get; set; }
    }

}
