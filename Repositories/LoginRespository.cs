// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Rawwr.Api.Dtos;
using Rawwr.Api.Entities;
using Rawwr.Constants;


//! This is an interface for the repository of Login. 
//! Using this to have dependency inversion.  (Also to inject the dependency when req.)

namespace Rawwr.Api.Repositories
{
    public interface ILoginRepository
    {
        // Task<IEnumerable<User>> GetUsers();

        Task<LoginResponseDto?> Login(LoginUserDto loginDetails);

        // Task<Boolean> CheckExistingUser(string email);
    }



    public class LoginRepository : BaseRepository, ILoginRepository
    {
        private readonly IConfiguration _configuration;

        public LoginRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


        public async Task<LoginResponseDto?> Login(LoginUserDto loginDetails)
        {
            using (var connection = NewConnection)
            {
                string commandText = $"SELECT * FROM users WHERE email = @email";
                var _u = null as User;
                try
                {
                    _u = await connection.QuerySingleOrDefaultAsync<User>(commandText, new { email = loginDetails.Email });
                }
                catch (Exception e)
                {
                    Console.WriteLine($" Exception caught in login api : {e}");
                    return null;
                }
                if (_u == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        if (BCrypt.Net.BCrypt.Verify(loginDetails.Password, _u.Password))
                        {
                            // Console.WriteLine(" bcrypt verified");
                            var claims = new[]
                       {   
                    // Add Id claim to the token.
                    new Claim( ClaimNames.UserId, _u.Id.ToString())
                   };

                            // TOKEN Schema for Auth
                            var token = new JwtSecurityToken
                            (
                                issuer: _configuration["Jwt:Issuer"],
                                audience: _configuration["Jwt:Audience"],
                                claims: claims,
                                expires: DateTime.UtcNow.AddDays(60),
                                notBefore: DateTime.UtcNow,
                                signingCredentials: new SigningCredentials(
                                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                                    SecurityAlgorithms.HmacSha256)
                            );

                            // Generate new JWT token.
                            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                            return new LoginResponseDto(tokenString);
                        }
                        else
                        {
                            // Console.WriteLine(" bcrypt not verified");
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" Error in login ");
                        Console.WriteLine(e);
                        return null;
                    }
                }
            }

        }
    }

}

