
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Data
{
    public class Song
    {
        [Key]
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

        [ForeignKey("SingerId")]
        public Singer Singer { get; set; }
    }
}

