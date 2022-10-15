using Chao_Blog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace chao_blog.Entity
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
