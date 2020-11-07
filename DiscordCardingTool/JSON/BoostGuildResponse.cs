using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordCardingTool.JSON
{
    public class BoostGuildResponse
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string guild_id { get; set; }
        public bool ended { get; set; }
    }
}
