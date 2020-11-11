using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{
    public class PurchaseNitroBody
    {
        public bool gift { get; set; }
        public string sku_subscription_plan_id { get; set; }
        public string payment_source_id { get; set; }
        public int expected_amount { get; set; }
    }
}
