using Microsoft.AspNetCore.Mvc;
using LoveMusic.Dto;
using LoveMusic.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using LoveMusic.Service;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using LoveMusic.Dto.User;

namespace LoveMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly FilesService _filesService;
        public UserController(
            MusicDbContext musicDbContext,
            FilesService filesService
        )
        {
            _musicDbContext = musicDbContext;
            _filesService = filesService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] string email = "",[FromForm] string passWord = "")
        {
            var user = _musicDbContext.Users.Where(x => x.Email == email && x.Password == passWord).FirstOrDefault();
            if (user == null)
            {
                return BadRequest(new { message = "Wrong email or password" });
            }
            else
            {
                return Ok(new { message = "Login success", userInfo = user });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            if (
                registerDto == null||
                registerDto.Name == null ||
                registerDto.Email == null ||
                registerDto.Password == null
            )
            {
                return BadRequest("Lack of information");
            }

            var avatarUrl = "";

            if (registerDto.AvatarFile != null)
            {
                avatarUrl = await _filesService.UploadImgToCloudinary(registerDto.AvatarFile, Consts.AvatarImgFolderUrl);
            }

            var user = new User
            {
               Name = registerDto.Name,
               Password = registerDto.Password,
               PhoneNumber = registerDto.PhoneNumber,
               Email = registerDto.Email,
               Address = registerDto.Address,
               AvatarUrl = avatarUrl,
               Gender = registerDto.Gender == Consts.Male ? Consts.Male : Consts.Female,
               Age = registerDto.Age,
               Type = Consts.Normal
            };  

            _musicDbContext.Users.Add(user);
            _musicDbContext.SaveChanges();

            return Ok(new { message = "Register success", userInfo = user });
        }

        [HttpGet("getUserInfo")]
        public IActionResult getUserInfo(int id)
        {
            var user = _musicDbContext.Users.Where(s => s.UserId == id).FirstOrDefault();
            return Ok(user);
        }

        [HttpPut("updateInformation/{id}")]
        public async Task<IActionResult> updateInformation(int id, [FromForm] UpdateUserDto updateUserDto)
        {
            var existingUser = _musicDbContext.Users.FirstOrDefault(s => s.UserId == id);
            if (existingUser == null)
            {
                return NotFound("Cannot find user by id");
            }

            if (!string.IsNullOrEmpty(updateUserDto.Name))
            {
                existingUser.Name = updateUserDto.Name;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Email))
            {
                existingUser.Email = updateUserDto.Email;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                existingUser.Email = updateUserDto.Password;
            }

            if (!string.IsNullOrEmpty(updateUserDto.PhoneNumber))
            {
                existingUser.PhoneNumber = updateUserDto.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Address))
            {
                existingUser.Address = updateUserDto.Address;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Gender))
            {
                existingUser.Gender = updateUserDto.Gender;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Type))
            {
                existingUser.Type = updateUserDto.Type;
            }

            if (updateUserDto.AvatarFile != null)
            {
                existingUser.AvatarUrl = await _filesService.UploadImgToCloudinary(updateUserDto.AvatarFile, Consts.AvatarImgFolderUrl);
            }

            _musicDbContext.SaveChanges();

            return Ok(existingUser);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var dsStudents = _musicDbContext.Users.ToList();
            return Ok(dsStudents);
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteUser(int id)
        {
            var user = _musicDbContext.Users.FirstOrDefault(s => s.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _musicDbContext.Users.Remove(user);
            _musicDbContext.SaveChanges();

            return Ok();
        }
    }
}