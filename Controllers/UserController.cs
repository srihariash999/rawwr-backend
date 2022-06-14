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
            var res = await _repository.CreateUser(NewUser);
            if (res != null)
            {
                return GetUserDto.FromUser(res);
            }
            else
            {
                return BadRequest("Cannot create user with given data");
            }

        }


        // GET /users
        [HttpGet]
        ///<summary>
        /// Phone is an optional field
        public async Task<ActionResult<GetUserDto>> GetUserById(Int64 id)
        {
            if (id == 0)
            {
                return BadRequest("Id is required or a bad Id was sent.");
            }
            // Get User
            GetUserDto? res = await _repository.GetUser(id);

            if (res != null)
            {
                return res;
            }
            else
            {
                return NotFound("User not found");
            }

        }

    }

}