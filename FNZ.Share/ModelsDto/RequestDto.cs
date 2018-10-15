using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.ModelsDto
{
    public class RequestDto : BaseModelDto
    {
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? RefusalDate { get; set; }
        public DateTime SentAt { get; set; }
        public Moderator Moderator { get; set; }
        public Enums.Action Action { get; set; }
        public Post Post { get; set; }
        public string PostTitle { get; set; }
    }
}
