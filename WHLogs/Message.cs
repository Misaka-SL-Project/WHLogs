namespace WHLogs
{
    public class Message
    {
        public Message(string content)
        {
            username = Plugin.Singleton.Config.Username;
            avatar_url = Plugin.Singleton.Config.AvatarUrl;
            this.content = content;
        }
            
        public string username { get; }
        public  string avatar_url { get; }
        public  string content { get; }
    }
}