using System.ComponentModel.DataAnnotations;

namespace chao_blog.Entity
{
    public class TokenRequest
    {
        /// <summary>
        /// 原 Token
        /// </summary>
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
