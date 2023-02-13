using System;
using System.Collections.Generic;
using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
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
        public readonly IUsuarioRepository _usuarioRepository; 

        public UsuarioController(ILogger<UsuarioController> logger,  IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;

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
        public IActionResult SalvarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    var erros = new List<string>();

                    if(String.IsNullOrEmpty(usuario.Nome) || String.IsNullOrWhiteSpace(usuario.Nome))
                    {
                        erros.Add("Nome Inválido");
                    }

                     if(String.IsNullOrEmpty(usuario.Email) || String.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
                    {
                        erros.Add("Email Inválido");
                    }

                     if(String.IsNullOrEmpty(usuario.Senha) || String.IsNullOrWhiteSpace(usuario.Senha))
                    {
                        erros.Add("Senha Inválida");
                    }

                    if (erros.Count > 0)
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Erros = erros
                        });
                    }

                    usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
                    usuario.Email = usuario.Email.ToLower();

                    if (!_usuarioRepository.VerificarEmail(usuario.Email))
                    {
                    _usuarioRepository.Salvar(usuario);

                    }
                    else 
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Descricao = "Usuário já está cadastrado"
                        });
                    }
                }


                return StatusCode(StatusCodes.Status201Created, "Usuário criado com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao salvar o usuário" + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu o seguinte erro: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError,
                });
            }
        }
    }
}