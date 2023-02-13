using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IUsuarioRepository _usuarioRepository;

        public BaseController (IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        protected Usuario LerToken()
        {
            var idUsuario = User.Claims.Where(c => c.Type  == ClaimTypes.Sid).Select(c => c.Value).FirstOrDefault();

			if (String.IsNullOrEmpty(idUsuario))
			{
				return null;
			}
			else 
			{
				return _usuarioRepository.GetUsuarioPorId(int.Parse(idUsuario));
			}

        }
    }
}