using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FNZ.Share.ModelsDto
{
    public class ResponseDto<T> where T : BaseModelDto
    {
        public T Object { get; set; }

        public bool ErrorOccurred => Errors.Any();

        public Dictionary<string,string> Errors { get; set; }

        public ResponseDto()
        {
            Errors = new Dictionary<string, string>();
        }
    }
}
