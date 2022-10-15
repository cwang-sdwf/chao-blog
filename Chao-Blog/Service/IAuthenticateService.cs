using chao_blog.Entity;
using Chao_Blog.Entity;

namespace chao_blog.Service
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(LoginDTO request, out string token);
    }
}
