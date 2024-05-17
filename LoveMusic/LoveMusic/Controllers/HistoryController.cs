using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public HistoryController(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        [HttpGet(Name = "GetAllHistorys")]
        public IActionResult GetAll()
        {
            var dsStudents = _musicDbContext.Historys.ToList();
            return Ok(dsStudents);
        }
    }
}