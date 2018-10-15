using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.BindingModels
{
    public class PostSearchParameterBindingModel
    {
        public int PageNumber { get; set; } = 1;
        public int Limit { get; set; } = 25;
        public string Sort { get; set; } = "AddedAt";
        public string Query { get; set; } = "";
        public bool Ascending { get; set; } = true;
        public Enums.Category? Category { get; set; }
    }
}
