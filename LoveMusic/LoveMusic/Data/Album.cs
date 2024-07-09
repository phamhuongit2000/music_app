
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Data
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public int? Likes { get; set; }

    }
}

