using affin_api.Models.DataAccess;
using affin_api.Utils;
using affin_objects;
using affin_objects.Responses;

namespace affin_api.Models.BusinessLogic
{
    public class BLAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly JwtTools _jwtTools;
        private readonly DAAuthentication _daAuthentication;

        public BLAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtTools = new JwtTools(_configuration);
            _daAuthentication = new DAAuthentication(_configuration);
        }

        public LoginResponse Login(Credentials data)
        {
            User? user = _daAuthentication.Login(data);
            string token = "";

            if (user != null)
                token = _jwtTools.GenerateJsonWebToken(user.CorreoElectronico);

            return new LoginResponse
            {
                User = user,
                Token = token
            };
        }
    }
}
