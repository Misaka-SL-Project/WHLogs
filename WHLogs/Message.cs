namespace WHLogs
{
    public class Message
    {
        public Message(string content)
        {
            Username = Plugin.Singleton.Config.Username;
            AvatarUrl = Plugin.Singleton.Config.AvatarUrl;
            Content = content;
        }
            
        public string Username { get; }
        public  string AvatarUrl { get; }
        public  string Content { get; }
    }
}