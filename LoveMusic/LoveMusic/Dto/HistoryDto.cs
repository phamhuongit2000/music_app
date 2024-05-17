
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Dto
{
    public class HistoryDto
    {
        public int? SongId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

