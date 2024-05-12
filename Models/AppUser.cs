﻿using TS.Bootcamp.ECommerce.WebAPI.Models.Abstract;

namespace TS.Bootcamp.ECommerce.WebAPI.Models
{
    public class AppUser : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => string.Join(" ", FirstName, LastName);
    }
}