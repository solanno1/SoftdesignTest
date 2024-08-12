using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace LibrarySystem.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/users")]
    public class UsersApiController : ApiController
    {        
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UsersApiController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = _userService.ValidateUser(model.Username, model.Password);
                if(!isValid) return Unauthorized();
                var token = _authService.GenerateJwtToken(model.Username);

                return Ok(new {Token = token});
            }
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] RegisterUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                _userService.RegisterUser(model.Username, model.Password);
                return Ok("Registrado bem sucedido");
            }
            return BadRequest(ModelState);
        }
    }
}