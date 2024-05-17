using LoveMusic.Data;
using LoveMusic.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        public AlbumController(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var dsSongs = _musicDbContext.Albums.Select(s => new
            {
                Id = s.AlbumId,
                Name = s.Name,
                Description = s.Description
            }).ToList();
            return Ok(dsSongs);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var album = _musicDbContext.Albums.Where(s => s.AlbumId == id).FirstOrDefault();
            return Ok(album);
        }

        [HttpPost]
        public IActionResult AddSong([FromBody] AlbumDto albumDto)
        {
            if (albumDto == null)
            {
                return BadRequest();
            }

            var album = new Album
            {
                Name = albumDto.Name,
                Description = albumDto.Description,
                Type = albumDto.Type,
                ImgUrl = albumDto.ImgUrl,
                Views = albumDto.Views,
                Downloads = albumDto.Downloads,
                Likes = albumDto.Likes,
            };

            _musicDbContext.Albums.Add(album);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSong(int id, [FromBody] AlbumDto albumDto)
        {
            if (albumDto == null)
            {
                return BadRequest();
            }

            var existingAlbum = _musicDbContext.Albums.FirstOrDefault(s => s.AlbumId == id);
            if (existingAlbum == null)
            {
                return NotFound();
            }

            existingAlbum.Name = albumDto.Name;
            existingAlbum.Description = albumDto.Description;
            existingAlbum.Type = albumDto.Type;
            existingAlbum.ImgUrl = albumDto.ImgUrl;
            existingAlbum.Views = albumDto.Views;
            existingAlbum.Downloads = albumDto.Downloads;
            existingAlbum.Likes = albumDto.Likes;

            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            var album = _musicDbContext.Albums.FirstOrDefault(s => s.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            _musicDbContext.Albums.Remove(album);
            _musicDbContext.SaveChanges();

            return Ok();
        }
    }
}