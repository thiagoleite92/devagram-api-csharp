using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevagramCSharp.Dtos
{
    public class ImagemDto
    {
        public string? Nome { get; set; }
        public IFormFile? Imagem { get; set; }
    }
}