
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Dto.Singer
{
    public class GetSingerDto
    {
        public int SingerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class PostSingerDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? AvatarFile { get; set; }
    }
}

