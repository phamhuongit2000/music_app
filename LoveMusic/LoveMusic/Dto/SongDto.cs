using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Dto.Song
{
    public class GetSongDto
    {
        public int SongId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? ImgUrl { get; set; }
        public string? AudioUrl { get; set; }
        public string? VideoUrl { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public int? Likes { get; set; }
        public int? SingerId { get; set; }
        public string? singerName { get; set; }
        public string? categoryName { get; set; }
        public string? categoryId { get; set; }
    }

    public class PostSongDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public IFormFile? ImgFile { get; set; }
        public IFormFile? AudioFile { get; set; }
        public IFormFile? VideoFile { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public int? Likes { get; set; }
        public int? SingerId { get; set; }
    }

}

