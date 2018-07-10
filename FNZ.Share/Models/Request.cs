using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Request
    {
        public long Id { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public DateTime RefusalDate { get; set; }
        public DateTime SentAt { get; set; }
        public virtual Post Post { get; set; }
        public virtual Moderator Moderator { get; set; }
    }
}
