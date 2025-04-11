using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        [HttpGet]
        public ActionResult<IList<UsuarioModel>> GetAll()
        {
            var usuarios = _usuarioRepository.FindAll();
            return Ok(usuarios);
        }


        [HttpGet("{id}")]
        public ActionResult<UsuarioModel> GetById(int id)
        {
            var usuario = _usuarioRepository.FindById(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }


        [HttpPost]
        public ActionResult<UsuarioModel> Post([FromBody] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = _usuarioRepository.Insert(usuarioModel);
            usuarioModel.UsuarioId = usuarioId;

            return CreatedAtAction(nameof(GetById), new { id = usuarioId }, usuarioModel);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.UsuarioId)
                return BadRequest("ID da URL diferente do corpo da requisição.");

            _usuarioRepository.Update(usuarioModel);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.FindById(id);
            if (usuario == null)
                return NotFound();

            _usuarioRepository.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<LoginResponseVM> Login([FromBody] LoginRequestVM usuarioRequest)
        {
            if (ModelState.IsValid)
            {

                var usuario = _usuarioRepository.FindByEmailAndSenha(usuarioRequest.EmailUsuario, usuarioRequest.Senha);

                if (usuario != null)
                {   

                    var tokenJWT = AutenticationService.GetToken(usuario);

                    var loginResponse = new LoginResponseVM();
                    loginResponse.Token = tokenJWT;
                    loginResponse.NomeUsuario = usuario.NomeUsuario;
                    loginResponse.Regra = usuario.Regra;
                    loginResponse.EmailUsuario = usuario.EmailUsuario;
                    loginResponse.UsuarioId = usuario.UsuarioId;


                    return Ok(loginResponse);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                var erros = ModelState.Values
                                      .SelectMany(x => x.Errors)
                                      .Select(x => x.ErrorMessage);

                return BadRequest(erros);
            }
        }

    }
}