using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chao_blog.Entity;
using Chao_Blog.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace chao_blog.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly TokenManagement _tokenManagement;

        public AuthenticateService(IOptions<TokenManagement> tokenManagement)
        {
            this._tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticated(LoginDTO request, out string token)
        {
            //TODO:验证账户密码逻辑 自行补全
            token = string.Empty;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Email),
                new Claim(ClaimTypes.Name,request.Password)
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
