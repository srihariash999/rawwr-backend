// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
using Npgsql;
using Rawwr.Api.Dtos;
using Rawwr.Api.Entities;
using Rawwr.Constants;
// using Rawwr.Api.Entities;


//! This is an interface for the repository of users. 
//! Using this to have dependency inversion.  (Also to inject the dependency when req.)

namespace Rawwr.Api.Repositories
{
    public interface IUsersRepository
    {
        // Task<IEnumerable<User>> GetUsers();

        Task<GetUserDto?> GetUser(Guid id);

        Task<Boolean> CreateUser(User user);

        // Task<Boolean> CheckExistingUser(string email);
    }



    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Boolean> CreateUser(User user)
        {
            var con = NewConnection;

            await con.OpenAsync();
            string commandText = $"INSERT INTO {TableNames.users} (name, email, phone, password) VALUES ( @name, @email, @phone, @password)";
            await using (var cmd = new NpgsqlCommand(commandText, con))
            {

                cmd.Parameters.AddWithValue("name", user.Name!);
                cmd.Parameters.AddWithValue("email", user.Email!);
                cmd.Parameters.AddWithValue("phone", user.Phone ?? "");
                cmd.Parameters.AddWithValue("password", user.Password!);

                await cmd.ExecuteNonQueryAsync();
            }
            return true;


        }

        public Task<GetUserDto?> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }

}

