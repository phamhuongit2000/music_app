
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Data
{
    public class SongCategory
    {
        [Key]
        public int SongCategoryId { get; set; }
        public int? SongId { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey("SongId")]
        public Song song { get; set; }

        [ForeignKey("CategoryId")]
        public Category category { get; set; }
    }
}

