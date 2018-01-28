namespace UtilizePim.Config
{
    public class ElasticConfig
    {
        public string Hosts { get; set; }
        public string IndexName { get; set; }
        public bool UseAuthentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string[] GetHosts()
        {
            return Hosts.Split(",");
        }
    }
}