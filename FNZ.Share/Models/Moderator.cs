using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Moderator
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
