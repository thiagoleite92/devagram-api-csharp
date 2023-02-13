using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IUsuarioRepository
    {
        public void Salvar(Usuario usuario);

        public bool VerificarEmail(string email);
    }
}