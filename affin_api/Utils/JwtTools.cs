using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace affin_api.Utils
{
    public class JwtTools
    {
        private readonly IConfiguration _configuration;
        private string _secretKey;
        private string _expires;

        public JwtTools(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration.GetSection("JWT").GetSection("SecretKey").ToString();
            _expires = _configuration["JWT:Expires"];
        }

        public string GenerateJsonWebToken(string email)
        {
            var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expires!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }
    }
}
