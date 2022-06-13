using System.Security.Claims;
using Rawwr.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rawwr.Api.Entities;
using Rawwr.Api.Repositories;
using Rawwr.Constants;


namespace Podboi.Api.Controllers
{

    [ApiController]
    [Route("users")]

    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _repository;

        public UserController(IUsersRepository repository)
        {
            this._repository = repository;
        }


        // POST /users
        [HttpPost]
        public async Task<ActionResult<GetUserDto>> CreateUserAsync(CreateUserDto NewUser)
        {

            // Create new user

            User _user = new User
            {
                Id = Guid.NewGuid(),
                Name = NewUser.Name,
                Email = NewUser.Email,
                Password = NewUser.Password,
                CreatedDate = DateTimeOffset.UtcNow
            };

            // hash password
            _user.Password = BCrypt.Net.BCrypt.HashPassword(_user.Password);

            Boolean res = await _repository.CreateUser(_user);
            if (res)
            {
                return new GetUserDto(
                _user.Id,
                _user.Name ?? "",
                _user.Email ?? "",
                _user.Phone ?? "",
                _user.CreatedDate
            );
            }
            else
            {
                return BadRequest("Cannot create user with given data");
            }

        }

    }

}