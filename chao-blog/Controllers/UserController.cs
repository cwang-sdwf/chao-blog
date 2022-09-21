using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chao_Blog.Data.Services;
using Chao_Blog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Chao_Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetResumesAsync();
            //var companyDtos = new List<User>();
            
            return Ok(users);
        }

        [HttpGet("{userId}")]

        public async Task<IActionResult> GetUser(Guid userId) // api/companies/123
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
