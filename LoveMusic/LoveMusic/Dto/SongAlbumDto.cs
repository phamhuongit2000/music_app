
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Dto
{
    public class SongAlbumDto
    {
        public int? SongId { get; set; }
        public int? albumId { get; set; }
    }
}

