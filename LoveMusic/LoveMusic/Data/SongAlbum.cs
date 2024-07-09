
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveMusic.Data
{
    public class SongAlbum
    {
        [Key]
        public int SongAlbumId { get; set; }
        public int? SongId { get; set; }
        public int? AlbumId { get; set; }

        [ForeignKey("SongId")]
        public Song song { get; set; }

        [ForeignKey("AlbumId")]
        public Album album { get; set; }
    }
}

