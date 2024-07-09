
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Dto.History
{
    public class GetHistoryDto
    {
        public int HistoryId { get; set; }
        public int? SongId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateTime { get; set; }
    }

    public class PostHistoryDto
    {
        public int? SongId { get; set; }
        public int? UserId { get; set; }
    }
}

