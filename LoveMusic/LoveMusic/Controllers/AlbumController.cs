using LoveMusic.Data;
using LoveMusic.Dto;
using LoveMusic.Dto.Category;
using LoveMusic.Dto.Album;
using LoveMusic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly FilesService _filesService;
        public AlbumController(
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
            var dsAlbum = _musicDbContext.Albums.Skip(offset).Take(limit).Select(s => new
            {
                Id = s.AlbumId,
                Name = s.Name,
                Description = s.Description,
                ImgUrl = s.ImgUrl,
            }).ToList();

            return Ok(dsAlbum);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var album = _musicDbContext.Albums.Where(s => s.AlbumId == id).FirstOrDefault();
            if (album == null)
            {
                return NotFound();
            }
            var songs = _musicDbContext.SongAlbums
                .Where(sa => sa.AlbumId == id)
                .Join(_musicDbContext.Songs,
                      sa => sa.SongId,
                      s => s.SongId,
                      (sa, s) => new
                      {
                          Id = s.SongId,
                          SongAlbumID = sa.SongAlbumId,
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
                Id = album.AlbumId,
                Name = album.Name,
                Description = album.Description,
                ImgUrl = album.ImgUrl,
                ListSongsInfo = songs
            };

            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAlbum ([FromForm] PostAlbumDto postAlbumDto)
        {
            if (postAlbumDto == null)
            {
                return BadRequest();
            }
            var imgUrl = "";

            if (postAlbumDto.ImgFile != null)
            {
                imgUrl = await _filesService.UploadImgToCloudinary(postAlbumDto.ImgFile, Consts.AlbumImgFolderUrl);
            }

            var album = new Album
            {
                Name = postAlbumDto.Name,
                Description = postAlbumDto.Description,
                ImgUrl = imgUrl,
                Downloads = 0,
                Views = 0,
                Likes = 0
            };

            _musicDbContext.Albums.Add(album);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, [FromForm] PostAlbumDto postAlbumDto)
        {
            var existingAlbum = _musicDbContext.Albums.FirstOrDefault(s => s.AlbumId == id);

            if (existingAlbum == null)
            {
                return NotFound();
            }

            if (postAlbumDto.ImgFile != null)
            {
                existingAlbum.ImgUrl = await _filesService.UploadImgToCloudinary(postAlbumDto.ImgFile, Consts.AlbumImgFolderUrl);
            }

            if (!string.IsNullOrEmpty(postAlbumDto.Name))
            {
                existingAlbum.Name = postAlbumDto.Name;
            }

            if (!string.IsNullOrEmpty(postAlbumDto.Description))
            {
                existingAlbum.Description = postAlbumDto.Description;
            }

            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Delete/{id}")]
        public IActionResult DeleteAlbum(int id)
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

        [HttpPost("AddSongToAlbum")]
        public async Task<IActionResult> AddSongToAlbum([FromForm] AddSongToAlbumDto addSongToAlbumDto)
        {
            if (addSongToAlbumDto == null)
            {
                return BadRequest();
            }
            var imgUrl = "";

            var songAlbum = new SongAlbum
            {
                AlbumId = addSongToAlbumDto.AlbumId,
                SongId = addSongToAlbumDto.SongId
            };

            _musicDbContext.SongAlbums.Add(songAlbum);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("DeleteSongFromAlbum")]
        public IActionResult DeleteSongFromAlbum([FromForm] AddSongToAlbumDto addSongToAlbumDto)
        {
            var songAlbums = _musicDbContext.SongAlbums
                .FirstOrDefault(s => s.SongId == addSongToAlbumDto.SongId && s.AlbumId == addSongToAlbumDto.AlbumId);

            if (songAlbums == null)
            {
                return NotFound();
            }

            _musicDbContext.SongAlbums.Remove(songAlbums);
            _musicDbContext.SaveChanges();
             
            return Ok();
        }
    }
}