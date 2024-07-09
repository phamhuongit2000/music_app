
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
    }
}

