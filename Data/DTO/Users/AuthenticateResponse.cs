
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data.DTO.Users
{

    public class AuthenticateResponse
    {
        public string JwtToken { get; set; }


        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

  
    }
}