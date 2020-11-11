using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordNitroTool.JSON
{
    public class JoinGuildResponse
    {
        public string code { get; set; }
        public bool new_member { get; set; }
        public Guild guild { get; set; }
        public Channel channel { get; set; }
    }

    public class Guild
    {
        public string id { get; set; }
        public string name { get; set; }
        public string splash { get; set; }
        public string banner { get; set; }
        public object description { get; set; }
        public string icon { get; set; }
        public string[] features { get; set; }
        public int verification_level { get; set; }
        public string vanity_url_code { get; set; }
        public Welcome_Screen welcome_screen { get; set; }
    }

    public class Welcome_Screen
    {
        public string description { get; set; }
        public Welcome_Channels[] welcome_channels { get; set; }
    }

    public class Welcome_Channels
    {
        public string channel_id { get; set; }
        public string description { get; set; }
        public string emoji_id { get; set; }
        public string emoji_name { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }

}
