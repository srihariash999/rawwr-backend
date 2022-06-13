using System;
using System.ComponentModel.DataAnnotations;

namespace Rawwr.Api.Dtos
{

    // DTO for returning User obj to client.
    public record GetUserDto(Guid Id, string Name, string Email, string Phone, DateTimeOffset CreatedDate);

    // DTO for accepting User obj for creation.
    public record CreateUserDto([Required] string Name, [Required] string Email, string? Phone, [Required] string Password);

    // DTO for login.

    public record LoginUserDto([Required] string Email, [Required] string Password);


}