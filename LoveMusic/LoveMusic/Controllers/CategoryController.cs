using LoveMusic.Data;
using LoveMusic.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Category : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public Category(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var dsSongs = _musicDbContext.Songs.Select(s => new
            {
                Id = s.SongId,
                Name = s.Name,
                Description = s.Description
            }).ToList();
            return Ok(dsSongs);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var song = _musicDbContext.Songs.Where(s => s.SongId == id).FirstOrDefault();
            return Ok(song);
        }

        [HttpPost]
        public IActionResult AddSong([FromBody] SongDto songDto)
        {
            if (songDto == null)
            {
                return BadRequest();
            }

            var song = new Song
            {
                Name = songDto.Name,
                Description = songDto.Description,
                Type = songDto.Type,
                ImgUrl = songDto.ImgUrl,
                AudioUrl = songDto.AudioUrl,
                VideoUrl = songDto.VideoUrl,
                Views = songDto.Views,
                Downloads = songDto.Downloads,
                Likes = songDto.Likes,
                SingerId = songDto.SingerId
            };

            _musicDbContext.Songs.Add(song);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSong(int id, [FromBody] SongDto songDto)
        {
            if (songDto == null)
            {
                return BadRequest();
            }

            var existingSong = _musicDbContext.Songs.FirstOrDefault(s => s.SongId == id);
            if (existingSong == null)
            {
                return NotFound();
            }

            existingSong.Name = songDto.Name;
            existingSong.Description = songDto.Description;
            existingSong.Type = songDto.Type;
            existingSong.ImgUrl = songDto.ImgUrl;
            existingSong.AudioUrl = songDto.AudioUrl;
            existingSong.VideoUrl = songDto.VideoUrl;
            existingSong.Views = songDto.Views;
            existingSong.Downloads = songDto.Downloads;
            existingSong.Likes = songDto.Likes;
            existingSong.SingerId = songDto.SingerId;

            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            var song = _musicDbContext.Songs.FirstOrDefault(s => s.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            _musicDbContext.Songs.Remove(song);
            _musicDbContext.SaveChanges();

            return Ok();
        }
    }
}