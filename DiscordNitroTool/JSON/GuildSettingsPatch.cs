using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{
    public class GuildSettingsPatch
    {
        public bool suppress_everyone { get; set; }

        public bool suppress_roles { get; set; }

        public bool muted { get; set; }

        public string[] restricted_guilds { get; set; }
    }
}
