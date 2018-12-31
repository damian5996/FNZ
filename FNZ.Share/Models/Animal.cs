﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Animal
    {
        public long Id { get; set; }
        public DateTime? AdoptionDate { get; set; }
        public DateTime? AddedToSystemAt { get; set; }
        public DateTime FoundAt { get; set; }
        public Enums.Type Type { get; set; }
        public string Name { get; set; }
        public string MaxWeight { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public virtual List<Application> Applications { get; set; }
    }
}
