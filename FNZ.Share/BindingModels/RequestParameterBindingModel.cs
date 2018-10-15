using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;

namespace FNZ.Share.BindingModels
{
    public class RequestParameterBindingModel
    {
        public bool ShowAccepted = false;
        public bool ShowRefused = false;
        public int PageNumber { get; set; } = 1;
        public int Limit { get; set; } = 25;
        public string Sort { get; set; } = "SendAt";
        public string Query { get; set; }
        public bool Ascending { get; set; } = true;
    }
}
