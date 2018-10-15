using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FNZ.Share.BindingModels
{
    public class RegisterBindingModel
    {
        [Required]
        [StringLength(16, ErrorMessage = "Login powinien zawierać od 3 do 16 znaków", MinimumLength = 3)] //pozbyc sie magic stringa
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [StringLength(30, ErrorMessage = "Hasło powinno zawierać od 7 do 30 znaków", MinimumLength = 7)] //pozbyc sie magic stringa
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
