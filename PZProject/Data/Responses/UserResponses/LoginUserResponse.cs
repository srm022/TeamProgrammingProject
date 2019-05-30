using System.IdentityModel.Tokens.Jwt;
namespace PZProject.Data.Responses
{
    public class LoginUserResponse
    {
        public string token { get; set; }
        public int userId { get; set; }

        public LoginUserResponse(string token, int userId)
        {
            this.token = token;
            this.userId = userId;
        }
    }
}
