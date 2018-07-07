using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Application
    {
        public long Id { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
