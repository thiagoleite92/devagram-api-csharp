using System;
using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : BaseController
    {
        public readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult ObterUsuario() 
        {

            try
            {
            Usuario usuario = new Usuario()
            {
                Email = "thiago@email.com",
                Nome = "Thiago",
                Id = 100
            };
            return Ok(usuario);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao obter o usuário" + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu o seguinte erro: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError,
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SalvarUsuario([FromBody] SalvarUsuarioDto salvarUsuario)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}