using Chao_Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chao_Blog.Data.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetResumesAsync();
        Task<User> GetUserAsync(Guid userId);
        Task<IEnumerable<User>> GetUsersAsync(IEnumerable<Guid> userIds);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<bool> UserExistAsync(Guid userId);
        Task<IEnumerable<Resume>> GetResumesAsync(Guid userId);
        Task<Resume> GetResumeAsync(Guid userId, Guid resumeId);
        void AddResume(Guid userId, Resume resume);
        void UpdateResume(Resume resume);
        void DeleteResume(Resume resume);

        Task<bool> SaveAsync();

    }
}
