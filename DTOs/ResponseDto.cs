using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ResponseDto
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
