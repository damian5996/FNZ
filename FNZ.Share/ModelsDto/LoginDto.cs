using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.ModelsDto
{
    public class LoginDto : BaseModelDto
    {
        public string Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
