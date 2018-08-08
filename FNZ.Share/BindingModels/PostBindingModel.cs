using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.BindingModels
{
    public class PostBindingModel
    {
        public string Author { get; set; }
        public Enums.Category Category { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
