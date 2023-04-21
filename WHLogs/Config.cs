using System.ComponentModel;
using Exiled.API.Interfaces;

namespace WHLogs
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Set the delay between log messages [This is the minimum, if this number is lower the plugin will not load to avoid discord ratelimit]")] 
        public float LogQueueDelay { get; set; } = 1.2f;

        [Description("Should the IP addresses be censored?")]
        public bool ShowIPAdresses { get; set; } = true;

        [Description("Set the webhook username")]
        public string Username { get; set; } = "Logs";

        [Description("Set the webhook avatar url")]
        public string AvatarUrl { get; set; } = "https://i.imgur.com/SaqRzfU.png";
        
        [Description("Set the webhook url for game events logs")]
        public string GameEventsLogsWebhookUrl { get; set; } = "fill me";
        
        [Description("Set the webhook url for command logs")]
        public string CommandLogsWebhookUrl { get; set; } = "fill me";
        
        [Description("Set the webhook url for pvp events logs")]
        public string PvpEventsLogsWebhookUrl { get; set; } = "fill me";
    }
}