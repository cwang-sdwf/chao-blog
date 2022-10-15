using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chao_Blog.Data;
using chao_blog.Service;
using Chao_Blog.Data.Services;
using chao_blog.Entity;
using Chao_Blog.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Chao_Blog.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace chao_blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        // private readonly IAuthenticateService _authService;
        private readonly IUserRepository _userRepository;
        private readonly TokenManagement _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly RoutineDbContext _apiDbContext;

        public AccountController(
            // IAuthenticateService authService,
        IUserRepository userRepository,
        TokenValidationParameters tokenValidationParams,
        RoutineDbContext apiDbContext,
            IOptionsMonitor<TokenManagement> optionsMonitor)
        {
            // _authService = authService;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
            _apiDbContext = apiDbContext;
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO req)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(req.Email);

                if (existingUser == null)
                {
                    return BadRequest("Username not exist");
                }
                var isCorrect = await _userRepository.CheckPasswordAsync(req.Email, req.Password);
                if (!isCorrect)
                {
                    // 出于安全原因，我们不想透露太多关于请求失败的信息
                    return BadRequest("Password incorrent");
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(jwtToken);
            }
            return BadRequest("Invalid payload");
        }

        private AuthResult GenerateJwtToken(User user)
        {
            //现在，是时候定义 jwt token 了，它将负责创建我们的 tokens
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // 从 appsettings 中获得我们的 secret 
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            // 定义我们的 token descriptor
            // 我们需要使用 claims （token 中的属性）给出关于 token 的信息，它们属于特定的用户，
            // 因此，可以包含用户的 Id、名字、邮箱等。
            // 好消息是，这些信息由我们的服务器和 Identity framework 生成，它们是有效且可信的。
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    // Jti 用于刷新 token，我们将在下一篇中讲到
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                // token 的过期时间需要缩短，并利用 refresh token 来保持用户的登录状态，
                // 不过由于这只是一个演示应用，我们可以对其进行延长以适应我们当前的需求
                Expires = DateTime.UtcNow.AddMinutes(5),
                // 这里我们添加了加密算法信息，用于加密我们的 token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);


            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevorked = false,
                UserId = user.Id.ToString(),
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(25) + Guid.NewGuid()
            };

            _apiDbContext.RefreshTokens.AddAsync(refreshToken); 
            _apiDbContext.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);

                if (result == null)
                {
                    return BadRequest("Invalid tokens");

                }

                return Ok(result);
            }

            return BadRequest("Invalid payload");
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                // 此验证功能将确保 Token 满足验证参数，并且它是一个真正的 token 而不仅仅是随机字符串
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                // 检查 token 是否有有效的安全算法
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                // 验证原 token 的过期时间，得到 unix 时间戳
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Token has not yet expired"
                }
                    };
                }

                // validation 4 - validate existence of the token
                // 验证 refresh token 是否存在，是否是保存在数据库的 refresh token
                var storedRefreshToken = await _apiDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedRefreshToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Refresh Token does not exist"
                }
                    };
                }

                // Validation 5 - 检查存储的 RefreshToken 是否已过期
                // Check the date of the saved refresh token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "Refresh Token has expired, user needs to re-login" },
                        Success = false
                    };
                }

                // Validation 6 - validate if used
                // 验证 refresh token 是否已使用
                if (storedRefreshToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Refresh Token has been used"
                }
                    };
                }

                // Validation 7 - validate if revoked
                // 检查 refresh token 是否被撤销
                if (storedRefreshToken.IsRevorked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Refresh Token has been revoked"
                }
                    };
                }

                // Validation 8 - validate the id
                // 这里获得原 JWT token Id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                // 根据数据库中保存的 Id 验证收到的 token 的 Id
                if (storedRefreshToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "The token doesn't mateched the saved token"
                }
                    };
                }

                // update current token 
                // 将该 refresh token 设置为已使用
                storedRefreshToken.IsUsed = true;
                _apiDbContext.RefreshTokens.Update(storedRefreshToken);
                await _apiDbContext.SaveChangesAsync();

                // 生成一个新的 token
                var dbUser = await _userRepository.GetUserAsync(new Guid(storedRefreshToken.UserId));
                return GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Token has expired please re-login"
                }
                    };
                }
                else
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Something went wrong."
                }
                    };
                }
            }
        }


        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

    }
}
