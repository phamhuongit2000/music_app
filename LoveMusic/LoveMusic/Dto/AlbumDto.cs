
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Dto.Album
{
    public class GetAlbumDto
    {
        public int AlbumId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public int? Likes { get; set; }
    }
    public class PostAlbumDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImgFile { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public int? Likes { get; set; }
    }

    public class AddSongToAlbumDto
    {
        public int? SongId { get; set; }
        public int? AlbumId { get; set; }
    }
}

