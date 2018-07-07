using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class MoneyCollection
    {
        public long Id { get; set; }
        public long MoneyTarget { get; set; }
        public DateTime EndDate { get; set; }
        public Post Post { get; set; }
    }
}
