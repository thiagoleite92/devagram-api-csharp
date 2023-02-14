using System;
using System.Collections.Generic;
using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
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

        public UsuarioController(
            ILogger<UsuarioController> logger,
            IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult ObterUsuario() 
        {
            try
            {
                 Usuario usuario = LerToken();


                return Ok(new UsuarioRespostaDto
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email
                });
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
        public IActionResult SalvarUsuario([FromForm] UsuarioRequisicaoDto usuarioDto)
        {
            try
            {
                if (usuarioDto != null)
                {
                    var erros = new List<string>();

                    if(String.IsNullOrEmpty(usuarioDto.Nome) || String.IsNullOrWhiteSpace(usuarioDto.Nome))
                    {
                        erros.Add("Nome Inválido");
                    }

                     if(String.IsNullOrEmpty(usuarioDto.Email) || String.IsNullOrWhiteSpace(usuarioDto.Email) || !usuarioDto.Email.Contains("@"))
                    {
                        erros.Add("Email Inválido");
                    }

                     if(String.IsNullOrEmpty(usuarioDto.Senha) || String.IsNullOrWhiteSpace(usuarioDto.Senha))
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


                    CosmicService cosmicService = new CosmicService();

                    Usuario usuario = new Usuario()
                    {
                        Email = usuarioDto.Email,
                        Senha = usuarioDto.Senha,
                        Nome = usuarioDto.Nome,
                        FotoPerfil = "teste"
                    };



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