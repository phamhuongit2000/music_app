
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Dto
{
    public class SongCategoryDto
    {
        public int? SongId { get; set; }
        public int? CategoryId { get; set; }
    }
}

