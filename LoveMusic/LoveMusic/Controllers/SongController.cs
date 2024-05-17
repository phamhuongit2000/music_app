using Microsoft.AspNetCore.Mvc;
using LoveMusic.Dto;
using LoveMusic.Data;
using Microsoft.EntityFrameworkCore;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public SongController(MusicDbContext musicDbContext)
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

        [HttpGet("Search")]
        public IActionResult Search(string singerName = "", string songName = "", string categoryName = "", string orderby = "desc")
        {
            var query = _musicDbContext.Songs
                .Include(s => s.Singer)
                .Where(s =>
                    (string.IsNullOrEmpty(singerName) || s.Singer.Name.Contains(singerName)) &&
                    (string.IsNullOrEmpty(songName) || s.Name.Contains(songName)));

            switch (orderby?.ToLower())
            {
                case "asc":
                    query = query.OrderBy(s => s.Name);
                    break;
                case "desc":
                    query = query.OrderByDescending(s => s.Name);
                    break;
                default:
                    query = query.OrderBy(s => s.SongId);
                    break;
            }

            var songs = query.Select(s => new
            {
                Id = s.SongId,
                Name = s.Name,
                Description = s.Description,
                Type = s.Type,
                ImgUrl = s.ImgUrl,
                AudioUrl = s.AudioUrl,
                VideoUrl = s.VideoUrl,
                Views = s.Views,
                Downloads = s.Downloads,
                Likes = s.Likes,
                SingerName = s.Singer.Name
            }).ToList();

            return Ok(songs);
        }
        [HttpGet("GetTop")]
        public IActionResult GetTop(string type = "views")
        {
            IQueryable<Song> query = null;

            switch (type?.ToLower())
            {
                case "views":
                    query = _musicDbContext.Songs.OrderByDescending(s => s.Views);
                    break;
                case "downloads":
                    query = _musicDbContext.Songs.OrderByDescending(s => s.Downloads);
                    break;
                case "likes":
                    query = _musicDbContext.Songs.OrderByDescending(s => s.Likes);
                    break;
                default:
                    return BadRequest("Invalid type parameter. Please specify views, downloads, or likes.");
            }

            var topSongs = query.Take(10)
                .Select(s => new
                {
                    Id = s.SongId,
                    Name = s.Name,
                    Description = s.Description,
                    Type = s.Type,
                    ImgUrl = s.ImgUrl,
                    AudioUrl = s.AudioUrl,
                    VideoUrl = s.VideoUrl,
                    Views = s.Views,
                    Downloads = s.Downloads,
                    Likes = s.Likes,
                    SingerName = s.Singer.Name
                })
                .ToList();

            return Ok(topSongs);
        }
    }
}