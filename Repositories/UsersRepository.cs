// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
using System.Data;
using Dapper;
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

        Task<GetUserDto?> GetUser(Int64 id);

        Task<User?> CreateUser(CreateUserDto user);

        // Task<Boolean> CheckExistingUser(string email);
    }



    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<User?> CreateUser(CreateUserDto user)
        {
            var connection = NewConnection;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }


            string commandText = $"INSERT INTO {TableNames.users} (name, email, phone, password) VALUES ( @name, @email, @phone, @password) RETURNING *";

            await using (var con = connection)
            {

                String pass = BCrypt.Net.BCrypt.HashPassword(user.Password);
                try
                {
                    var res = await con.QueryAsync<User>(commandText, new
                    {
                        name = user.Name!,
                        email = user.Email!,
                        phone = user.Phone,
                        password = pass,
                    });
                    Console.WriteLine("result of insert: " + res);
                    var r = res.ToList();
                    if (r.Count > 0)
                    {
                        return r[0];
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Got exception " + e);
                    Console.WriteLine(" detail maybe ? : " + e.Message);
                    Console.WriteLine(" data maybe ? : " + e.Data);

                    return null;
                }
            }

        }

        public async Task<GetUserDto?> GetUser(Int64 id)
        {
            var connection = NewConnection;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            await using (var con = connection)
            {
                try
                {
                    string commandText = $"SELECT * FROM users WHERE id = @id";

                    var res = await con.QuerySingleAsync<User>(commandText, new { id });
                    if (res != null) return GetUserDto.FromUser(res);
                    else return null;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Got exception " + e);
                    Console.WriteLine(" detail maybe ? : " + e.Message);
                    Console.WriteLine(" data maybe ? : " + e.Data);
                    return null;
                }
            }

        }
    }

}

