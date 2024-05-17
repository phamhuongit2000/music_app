using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SingerController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public SingerController(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            var dsStudents = _musicDbContext.Singers.ToList();
            return Ok(dsStudents);
        }
    }
}