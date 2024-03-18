using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels.User;

namespace ClinicCorporateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsuariosController(IUserService manager)
        {
            this._userService = manager;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] User usuario)
        {
            var usuarioLogado = await _userService.ValidateUserAndGenerateTokenAsync(usuario);
            if (usuarioLogado != null)
            {
                return Ok(usuarioLogado);
            }
            return Unauthorized();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string login = User.Identity.Name;
            var usuario = await _userService.GetAsync(login);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewUser user)
        {
            var usuarioInserido = await _userService.InsertAsync(user);
            return CreatedAtAction(nameof(Get), new { login = user.Login }, usuarioInserido);
        }
    }
}
