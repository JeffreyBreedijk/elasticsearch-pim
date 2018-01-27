namespace UtilizePim.Config
{
    public class ElasticConfig
    {
        public string[] Hosts { get; set; }
        public string Index { get; set; }
        public bool UseAuthentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}