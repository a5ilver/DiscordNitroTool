using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{

    public class BillingSubscriptionPostResponse
    {
        public string id { get; set; }
        public int type { get; set; }
        public DateTime created_at { get; set; }
        public object canceled_at { get; set; }
        public DateTime current_period_start { get; set; }
        public DateTime current_period_end { get; set; }
        public int status { get; set; }
        public string payment_source_id { get; set; }
        public object payment_gateway { get; set; }
        public string payment_gateway_plan_id { get; set; }
        public string plan_id { get; set; }
        public Item[] items { get; set; }
        public string currency { get; set; }
        public AdditionalPlanss[] additional_plans { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string plan_id { get; set; }
        public int quantity { get; set; }
    }

    public class AdditionalPlanss
    {
        public string plan_id { get; set; }
        public int quantity { get; set; }
    }

}
