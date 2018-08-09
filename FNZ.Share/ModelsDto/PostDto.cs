using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.ModelsDto
{
    public class PostDto : BaseModelDto
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime? AddedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public Moderator EditedBy { get; set; }
        public Enums.Category Category { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
