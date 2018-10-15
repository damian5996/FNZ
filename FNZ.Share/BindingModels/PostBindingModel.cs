using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.BindingModels
{
    public class PostBindingModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public Enums.Category Category { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
