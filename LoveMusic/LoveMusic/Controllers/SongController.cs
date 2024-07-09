using Microsoft.AspNetCore.Mvc;
using LoveMusic.Dto;
using LoveMusic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using LoveMusic.Service;
using LoveMusic.Dto.Song;
using LoveMusic.Dto.User;
using System.Reflection;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly FilesService _filesService;
        public SongController(
            MusicDbContext musicDbContext,
            FilesService filesService
        )
        {
            _musicDbContext = musicDbContext;
            _filesService = filesService;
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            try
            {
                return Ok("ngon roi");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll(int limit = 10, int offset = 0)
        {
            try
            {
                IQueryable<Song> query = _musicDbContext.Songs.Include(s => s.Singer).OrderBy(s => s.SongId).Skip(offset).Take(limit);

                var dsSongs = query.Select(s => new
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
                }).ToList();

                return Ok(dsSongs);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var song = _musicDbContext.Songs.Where(s => s.SongId == id).FirstOrDefault();
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddSong([FromForm] PostSongDto postSongDto)
        {
            if (postSongDto == null)
            {
                return BadRequest();
            }
            var imgUrl = "";
            var audioUrl = "";
            var videoUrl = "";

            if (postSongDto.ImgFile != null)
            {
                imgUrl = await _filesService.UploadImgToCloudinary(postSongDto.ImgFile, Consts.SongImgFolderUrl);
            }

            if (postSongDto.AudioFile != null)
            {
                audioUrl = await _filesService.UploadAudioToCloudinary(postSongDto.AudioFile, Consts.SongAudioFolderUrl);
            }

            if (postSongDto.VideoFile != null)
            {
                videoUrl = await _filesService.UploadVideoToCloudinary(postSongDto.VideoFile, Consts.SongVideoFolderUrl);
            }

            var song = new Song
            {
                Name = postSongDto.Name,
                Description = postSongDto.Description,
                Type = postSongDto.Type == Consts.Vip ? Consts.Vip : Consts.Normal,
                Views = 0,
                Downloads = 0,
                Likes = 0,
                SingerId = postSongDto.SingerId,
                VideoUrl = videoUrl,
                ImgUrl = imgUrl,
                AudioUrl = audioUrl
            };

            _musicDbContext.Songs.Add(song);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> UpdateSong(int id, [FromForm] PostSongDto songDto)
        {
            var existingSong = _musicDbContext.Songs.FirstOrDefault(s => s.SongId == id);
            if (existingSong == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(songDto.Name))
            {
                existingSong.Name = songDto.Name;
            }

            if (!string.IsNullOrEmpty(songDto.Description))
            {
                existingSong.Description = songDto.Description;
            }

            if (!string.IsNullOrEmpty(songDto.Description))
            {
                existingSong.Description = songDto.Description;
            }

            if (!string.IsNullOrEmpty(songDto.Type))
            {
                existingSong.Type = songDto.Type;
            }

            if (!string.IsNullOrEmpty(songDto.Views.ToString()))
            {
                existingSong.Views = songDto.Views;
            }

            if (!string.IsNullOrEmpty(songDto.Downloads.ToString()))
            {
                existingSong.Downloads = songDto.Downloads;
            }

            if (!string.IsNullOrEmpty(songDto.Likes.ToString()))
            {
                existingSong.Likes = songDto.Likes;
            }

            if (!string.IsNullOrEmpty(songDto.SingerId.ToString()))
            {
                existingSong.SingerId = songDto.SingerId;
            }

            if (songDto.ImgFile != null)
            {
                existingSong.ImgUrl = await _filesService.UploadImgToCloudinary(songDto.ImgFile, Consts.SongImgFolderUrl);
            }

            if (songDto.AudioFile != null)
            {
                existingSong.AudioUrl = await _filesService.UploadAudioToCloudinary(songDto.AudioFile, Consts.SongAudioFolderUrl);
            }

            if (songDto.VideoFile != null)
            {
                existingSong.VideoUrl = await _filesService.UploadVideoToCloudinary(songDto.VideoFile, Consts.SongVideoFolderUrl);
            }

            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Delete/{id}")]
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
        public IActionResult Search(string singerName = "", string songName = "", string categoryName = "", string orderby = "desc", int limit = 10, int offset = 0)
        {
            var query = _musicDbContext.Songs
                .Include(s => s.Singer)
                .Where(s =>
                    (string.IsNullOrEmpty(singerName) || s.Singer.Name.Contains(singerName)) &&
                    (string.IsNullOrEmpty(songName) || s.Name.Contains(songName)));

            switch (orderby?.ToLower())
            {
                case "asc":
                    query = query.OrderBy(s => s.SongId);
                    break;
                case "desc":
                    query = query.OrderByDescending(s => s.SongId);
                    break;
                default:
                    query = query.OrderBy(s => s.SongId);
                    break;
            }

            query = query.Skip(offset).Take(limit);

            var songs = query.Select(s => new
            {
                Id = s.SongId,
                Name = s.Name,
                SingerName = s.Singer.Name,
                SingerID  =  s.Singer.SingerId,
                Vip = s.Type,
                ImgUrl = s.ImgUrl,
                AudioUrl = s.AudioUrl,
                Views = s.Views,
                Likes = s.Likes,
                Downloads = s.Downloads,
                Description = s.Description,
            }).ToList();

            return Ok(songs);
        }
        [HttpGet("GetTop")]
        public IActionResult GetTop(string type = Consts.TopViews)
        {
            IQueryable<Song> query = null;

            switch (type?.ToLower())
            {
                case Consts.TopViews:
                    query = _musicDbContext.Songs.OrderByDescending(s => s.Views);
                    break;
                case Consts.TopLikes:
                    query = _musicDbContext.Songs.OrderByDescending(s => s.Downloads);
                    break;
                case Consts.TopDownloads:
                    query = _musicDbContext.Songs.OrderByDescending(s => s.Likes);
                    break;
                default:
                    return BadRequest("Invalid type parameter. Please specify views, downloads, or likes.");
            }

            var topSongs = query.Take(20)
                .Select(s => new
                {
                    Id = s.SongId,
                    Name = s.Name,
                    SingerName = s.Singer.Name,
                    SingerID = s.Singer.SingerId,
                    Vip = s.Type,
                    ImgUrl = s.ImgUrl,
                    AudioUrl = s.AudioUrl,
                    Views = s.Views != null ? s.Views : 0,
                    Likes = s.Likes,
                    Downloads = s.Downloads,
                    Description = s.Description,
                })
                .ToList();

            return Ok(topSongs);
        }
    }
}