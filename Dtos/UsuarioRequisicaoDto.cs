using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevagramCSharp.Dtos
{
    public class UsuarioRequisicaoDto
    {
		public string? Nome { get; set; }
		public string? Email { get; set; }
		public string? Senha { get; set; }
		public IFormFile? FotoPerfil {get; set; }
    }
}