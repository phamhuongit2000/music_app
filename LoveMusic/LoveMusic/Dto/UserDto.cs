﻿
using System.ComponentModel.DataAnnotations;

namespace LoveMusic.Dto
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
        public string? Type { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Money { get; set; }
    }
}

