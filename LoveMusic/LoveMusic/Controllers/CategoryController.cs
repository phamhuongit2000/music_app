using LoveMusic.Data;
using LoveMusic.Dto;
using LoveMusic.Dto.Category;
using LoveMusic.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly FilesService _filesService;
        public CategoryController(
            MusicDbContext musicDbContext,
            FilesService filesService
        )
        {
            _musicDbContext = musicDbContext;
            _filesService = filesService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll(int limit = 10, int offset = 0)
        {
            var dsCategory = _musicDbContext.Categorys.Skip(offset).Take(limit).Select(s => new
            {
                Id = s.CategoryId,
                Name = s.Name,
                Description = s.Description,
                ImgUrl = s.ImgUrl,
            }).ToList();

            return Ok(dsCategory);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var category = _musicDbContext.Categorys.Where(s => s.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromForm] PostCategoryDto postCategoryDto)
        {
            if (postCategoryDto == null)
            {
                return BadRequest();
            }
            var imgUrl = "";

            if (postCategoryDto.ImgFile != null)
            {
                imgUrl = await _filesService.UploadImgToCloudinary(postCategoryDto.ImgFile, Consts.CategoryImgFolderUrl);
            }

            var cateogry = new Category
            {
                Name = postCategoryDto.Name,
                Description = postCategoryDto.Description,
                ImgUrl = imgUrl,
            };

            _musicDbContext.Categorys.Add(cateogry);
            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] PostCategoryDto postCategoryDto)
        {
            var existingCategory = _musicDbContext.Categorys.FirstOrDefault(s => s.CategoryId == id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            if (postCategoryDto.ImgFile != null)
            {
                existingCategory.ImgUrl = await _filesService.UploadImgToCloudinary(postCategoryDto.ImgFile, Consts.CategoryImgFolderUrl);
            }

            if (!string.IsNullOrEmpty(postCategoryDto.Name))
            {
                existingCategory.Name = postCategoryDto.Name;
            }

            if (!string.IsNullOrEmpty(postCategoryDto.Description))
            {
                existingCategory.Description = postCategoryDto.Description;
            }

            _musicDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteSinger(int id)
        {
            var category = _musicDbContext.Categorys.FirstOrDefault(s => s.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _musicDbContext.Categorys.Remove(category);
            _musicDbContext.SaveChanges();

            return Ok();
        }
    }
}