using System.IdentityModel.Tokens.Jwt;
namespace PZProject.Data.Responses
{
    public class LoginUserResponse
    {
        public string token { get; set; }

        public LoginUserResponse(string token)
        {
            this.token = token;
        }
    }
}
