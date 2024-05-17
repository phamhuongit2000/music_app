using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public UserController(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAll()
        {
            var dsStudents = _musicDbContext.Users.ToList();
            return Ok(dsStudents);
        }
    }
}