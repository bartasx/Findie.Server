using Newtonsoft.Json;

namespace Findie.Common.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public bool RememberMe { get; set; }
    }
}