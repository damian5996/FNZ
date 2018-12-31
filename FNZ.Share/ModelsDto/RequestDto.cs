using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.ModelsDto
{
    public class RequestDto : BaseModelDto
    {
        public long Id { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? RefusalDate { get; set; }
        public DateTime SentAt { get; set; }
        public ModeratorDto Moderator { get; set; }
        public Enums.Action Action { get; set; }
        public PostDto PostDto { get; set; }
        public string PostTitle { get; set; }
    }
}
