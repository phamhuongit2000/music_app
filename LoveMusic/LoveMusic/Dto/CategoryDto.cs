
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Dto.Category
{
    public class GetCategoryDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
    }

    public class PostCategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImgFile { get; set; }
    }
}

