using System;
using System.ComponentModel.DataAnnotations;
using Rawwr.Api.Entities;

namespace Rawwr.Api.Dtos
{

    // DTO for returning User obj to client.
    public record GetUserDto( string Name, string Email, string Phone){
        //method to convert User to GetUserDto
        public static GetUserDto FromUser(User user)
        {
            return new GetUserDto(
                user.Name ?? "",
                user.Email ?? "",
                user.Phone ?? ""
            );
        }

        
    }

    // DTO for accepting User obj for creation.
    public record CreateUserDto([Required] string Name, [Required] string Email, string? Phone, [Required] string Password);

    // DTO for login.

    public record LoginUserDto([Required] string Email, [Required] string Password);


}