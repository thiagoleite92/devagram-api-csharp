using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;

        public LoginController (ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto loginrequisicao)
        {
            try
            {
                if(!String.IsNullOrEmpty(loginrequisicao.Senha) && !String.IsNullOrEmpty(loginrequisicao.Email) && !String.IsNullOrWhiteSpace(loginrequisicao.Senha) && !String.IsNullOrWhiteSpace(loginrequisicao.Email))
                {
                    string email = "thiago@email.com";
                    string senha = "senha123";

                    if (loginrequisicao.Email == email && loginrequisicao.Senha == senha) {

                        Usuario usuario = new Usuario()
                        {
                            Email = loginrequisicao.Email,
                            Nome = "Thiago Leite",
                            Id = 123
                        };

                        return Ok(new LoginRespostaDto()
                        {
                            Email = usuario.Email,
                            Nome = usuario.Nome,
                            Token = TokenService.CriarToken(usuario)
                        });
                    }
                    else 
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Descricao = "Email ou Senha inválidos",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }
                }
                else
                {
                return BadRequest(new ErrorRespostaDto()
                {
                    Descricao = "Usuário não preencheu os campos de login corretamente",
                    Status = StatusCodes.Status400BadRequest
                });
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro no login: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu um erro ao fazer o login",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }


    }
}
