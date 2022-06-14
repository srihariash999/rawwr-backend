using Rawwr.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Rawwr.Api.Repositories;

namespace Podboi.Api.Controllers
{

    [ApiController]
    [Route("login")]

    public class LoginController : ControllerBase
    {

        private readonly ILoginRepository _repository;

        public LoginController(ILoginRepository repository)
        {
            this._repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> UserLogin(LoginUserDto loginDetails)
        {
            var res = await _repository.Login(loginDetails);
            if (res == null)
            {
                // return incorrect credentials error.
                return BadRequest(" Incorrect credentials or account not found!");
            }
            else
            {
                return Ok(res);
            }
        }


    }

}