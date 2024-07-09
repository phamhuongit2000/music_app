
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Data
{
    public class History
    {
        [Key]
        public int HistoryId { get; set; }
        public int? SongId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateTime { get; set; }

        [ForeignKey("SongId")]
        public Song song { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }
    }
}

