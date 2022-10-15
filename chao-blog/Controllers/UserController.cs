using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using chao_blog.Service;
using Chao_Blog.Data.Services;
using Chao_Blog.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("email/{email}")]

        public async Task<IActionResult> GetUserByEmail(string email) // api/companies/123
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
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
        //
        // [HttpPost("login")]
        //
        // public async Task<IActionResult> GetUserLogin([FromBody] LoginRequestDTO req) // api/companies/123
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest("Invaild Request");
        //     }
        //
        //     string sToken;
        //
        //     if()
        //     // var user = await _userRepository.GetUserLoginAsync(email,password);
        //     // if (user == null)
        //     // {
        //     //     return NotFound();
        //     // }
        //     //
        //     // return Ok(user);
        // }


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
           
            _userRepository.AddUser(user);

            await _userRepository.SaveAsync();
            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return Ok(user); 
        }
    }
}
