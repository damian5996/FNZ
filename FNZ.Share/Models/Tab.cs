using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Tab
    {
        public int Id { get; set; }
        public Enums.TabCategory TabCategory { get; set; }
        public string Content { get; set; }
        public string PhotoPath { get; set; }
        public Moderator LastEditedBy { get; set; }
        public virtual Moderator Moderator { get; set; }
    }
}
