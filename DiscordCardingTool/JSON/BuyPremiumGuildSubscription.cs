using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordCardingTool.JSON
{

    public class PremiumGuildSubscriptionBody
    {
        public Additional_Plans[] additional_plans { get; set; }
    }

    public class Additional_Plans
    {
        public string plan_id { get; set; }
        public int quantity { get; set; }
    }

}
