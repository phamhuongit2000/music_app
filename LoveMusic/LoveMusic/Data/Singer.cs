
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Data
{
    public class Singer
    {
        [Key]
        public int SingerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

