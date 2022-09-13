using Chao_Blog.Data;
using Chao_Blog.Data.Services;
using Chao_Blog.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chao_Blog.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly RoutineDbContext _context;
        public UserRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Id = Guid.NewGuid();
            foreach(var resume in user.Resumes)
            {
                resume.Id = Guid.NewGuid();
            }

            _context.Users.Add(user);
        }

       

        public void AddResume(Guid userId, Resume resume)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (resume == null)
            {
                throw new ArgumentNullException(nameof(resume));
            }
            resume.UserId = userId;
            _context.Resumes.Add(resume);
        }

        public async Task<bool> UserExistAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Users.AnyAsync(x => x.Id == userId);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
        }

        public void DeleteResume(Resume resume)
        {
            if (resume == null)
            {
                throw new ArgumentNullException(nameof(resume));
            }
            _context.Resumes.Remove(resume);
        }

        public async Task<IEnumerable<User>> GetResumesAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<Guid> userIds)
        {
           if(userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }
            return await _context.Users.Where(x => userIds.Contains(x.Id))
                 .OrderBy(x => x.FirstName)
                 .ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<Resume> GetResumeAsync(Guid userId, Guid resumeId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (resumeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(resumeId));
            }
            return await _context.Resumes.Where(x => x.UserId == userId && x.Id == resumeId)
               .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Resume>> GetResumesAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Resumes.Where(x => x.UserId == userId)
                .OrderBy(x => x.Introduction)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateUser(User user)
        {
            //throw new NotImplementedException();
        }

        public void UpdateResume(Resume resume)
        {
            //throw new NotImplementedException();
        }
    }
}

