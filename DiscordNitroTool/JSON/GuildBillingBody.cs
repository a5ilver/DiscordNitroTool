using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{

    public class GuildBillingBody
    {
        public string plan_id { get; set; }
        public string payment_source_id { get; set; }
        public AdditionalPlans[] additional_plans { get; set; }
    }

    public class AdditionalPlans
    {
        public string plan_id { get; set; }
        public int quantity { get; set; }
    }

}
