using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.BindingModels
{
    public class AnimalBindingModel
    {
        public DateTime AdoptionDate { get; set; }
        public DateTime AddedToSystemAt { get; set; }
        public DateTime FoundAt { get; set; }
        public Enums.Type Type { get; set; }
        public string Name { get; set; }
        public int MaxWeight { get; set; }
        public string Breed { get; set; }
        public double Age { get; set; }
        public string Author { get; set; }
        public Enums.Category Category { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
