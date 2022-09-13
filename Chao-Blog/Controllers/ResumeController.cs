using System;
using System.Threading.Tasks;
using Chao_Blog.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chao_Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController:ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public ResumeController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetResumes(Guid companyId)
        {
            var resumes = await _userRepository.GetResumesAsync(companyId);
            //var companyDtos = new List<User>();

            return Ok(resumes);
        }


    }
}
