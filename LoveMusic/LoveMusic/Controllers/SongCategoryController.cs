using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongCategoryController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public SongCategoryController(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        [HttpGet(Name = "GetAllSongCategorys")]
        public IActionResult GetAll()
        {
            var dsStudents = _musicDbContext.SongCategorys.ToList();
            return Ok(dsStudents);
        }
    }
}