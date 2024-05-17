
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Dto
{
    public class AlbumDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? ImgUrl { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public int? Likes { get; set; }

    }
}

