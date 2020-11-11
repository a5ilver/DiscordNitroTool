using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{
    public class PaymentSource
    {
        public string id { get; set; }
        public int type { get; set; }
        public bool invalid { get; set; }
        public string brand { get; set; }
        public string last_4 { get; set; }
        public int expires_month { get; set; }
        public int expires_year { get; set; }
        public Billing_Address billing_address { get; set; }
        public string country { get; set; }
        public bool _default { get; set; }
    }

    public class Billing_Address
    {
        public string name { get; set; }
        public string line_1 { get; set; }
        public object line_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }
    }

}
