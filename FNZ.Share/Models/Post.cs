using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime AddedAt { get; set; }
        public string RequestSentBy { get; set; }
        public Enums.Category Category { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
