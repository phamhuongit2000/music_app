using LoveMusic.Data;
using LoveMusic.Dto;
using LoveMusic.Dto.Singer;
using LoveMusic.Dto.Song;
using LoveMusic.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SingerController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly FilesService _filesService;
        public SingerController(
            MusicDbContext musicDbContext,
            FilesService filesService
        )
        {
            _musicDbContext = musicDbContext;
            _filesService = filesService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll(int limit = 10, int offset = 0)
        {
            var dsSinger = _musicDbContext.Singers.Skip(offset).Take(limit).Select(s => new
            {
                Id = s.SingerId,
                Name = s.Name,
                Description = s.Description,
                AvatarUrl = s.AvatarUrl,
            }).ToList();

            return Ok(dsSinger);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var singer = _musicDbContext.Singers.Where(s => s.SingerId == id).FirstOrDefault();

            if (singer == null)
            {
                return NotFound();
            }

            var songs = _musicDbContext.Songs
               .Where(s => s.SingerId == id)
               .Select(s => new
               {
                   Id = s.SongId,
                   Name = s.Name,
                   SingerName = s.Singer.Name,
                   SingerId = s.Singer.SingerId,
                   Vip = s.Type,
                   ImgUrl = s.ImgUrl,
                   AudioUrl = s.AudioUrl,
                   Views = s.Views,
                   Likes = s.Likes,
                   Downloads = s.Downloads,
                   Description = s.Description,
               })
               .ToList();

            var response = new
            {
                Id = singer.SingerId,
                Name = singer.Name,
                Description = singer.Description,
                AvatarUrl = singer.AvatarUrl,
                ListSongsInfo = songs
            };

            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddSinger([FromForm] PostSingerDto postSingerDto)
        {
            if (postSingerDto == null)
            {
                return BadRequest();
            }
            var avatarUrl = "";

            if (postSingerDto.AvatarFile != null)
            {
                avatarUrl = await _filesService.UploadImgToCloudinary(postSingerDto.AvatarFile, Consts.SingerAvatarFolderUrl);
            }

            var singer = new Singer
            {
                Name = postSingerDto.Name,
                Description = postSingerDto.Description,
                AvatarUrl = avatarUrl,
            };

            _musicDbContext.Singers.Add(singer);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> UpdateSinger(int id, [FromForm] PostSingerDto postSingerDto)
        {
            var existingSinger = _musicDbContext.Singers.FirstOrDefault(s => s.SingerId == id);

            if (existingSinger == null)
            {
                return NotFound();
            }

            if (postSingerDto.AvatarFile != null)
            {
                existingSinger.AvatarUrl = await _filesService.UploadImgToCloudinary(postSingerDto.AvatarFile, Consts.SingerAvatarFolderUrl);
            }

            if (!string.IsNullOrEmpty(postSingerDto.Name))
            {
                existingSinger.Name = postSingerDto.Name;
            }

            if (!string.IsNullOrEmpty(postSingerDto.Description))
            {
                existingSinger.Description = postSingerDto.Description;
            }

            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Delete/{id}")]
        public IActionResult DeleteSinger(int id)
        {
            var singer = _musicDbContext.Singers.FirstOrDefault(s => s.SingerId == id);
            if (singer == null)
            {
                return NotFound();
            }

            _musicDbContext.Singers.Remove(singer);
            _musicDbContext.SaveChanges();

            return Ok();
        }
    }
}