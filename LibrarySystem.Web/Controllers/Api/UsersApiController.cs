using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace LibrarySystem.Web.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersApiController : ApiController
    {        
        private readonly IUserService _userService;
        public UsersApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = _userService.ValidateUser(model.Username, model.Password);
                if(!isValid) return Unauthorized();

                return Ok("Login bem sucedido!");
            }
            return BadRequest(ModelState);
        }
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