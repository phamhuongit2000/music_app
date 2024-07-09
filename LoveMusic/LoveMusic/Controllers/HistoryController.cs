using LoveMusic.Data;
using LoveMusic.Dto.Category;
using LoveMusic.Dto.History;
using LoveMusic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        [HttpGet("GetHistorysByUserId/{id}")]
        public IActionResult GetHistorysByUserId(int id, int limit = 10, int offset = 0)
        {
            IQueryable<History> query = _musicDbContext.Historys.Include(s => s.song)
                .Where(s => s.user.UserId == id)
                .Skip(offset).Take(limit)
                .OrderByDescending(s => s.HistoryId);

            var dsHistory = query.Select(s => new
            {
                SongId = s.SongId,
                UserId = s.UserId,
                Name = s.song.Name,
                DateTime = s.DateTime.HasValue ? s.DateTime.Value.ToString("dd/MM/yyyy HH:mm") : "",
                SongDescription = s.song.Description,
                SingerName = s.song.Singer.Name,
                SingerId = s.song.Singer.SingerId,
                ImgUrl = s.song.ImgUrl,
                AudioUrl = s.song.AudioUrl,
            }).ToList();

            return Ok(dsHistory);
        }

        [HttpPost("AddHistory")]
        public IActionResult AddHistory([FromForm] PostHistoryDto postHistoryDto)
        {
            if (postHistoryDto == null)
            {
                return BadRequest();
            }

            var history = new History
            {
                SongId = postHistoryDto.SongId,
                UserId = postHistoryDto.UserId,
                DateTime = DateTime.Now,
            };

            _musicDbContext.Historys.Add(history);
            _musicDbContext.SaveChanges();

            return Ok();
        }
    }
}