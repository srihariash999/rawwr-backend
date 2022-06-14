// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
using System.Data;
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

        Task<Boolean> CreateUser(CreateUserDto user);

        // Task<Boolean> CheckExistingUser(string email);
    }



    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Boolean> CreateUser(CreateUserDto user)
        {
            var con = NewConnection;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            string commandText = $"INSERT INTO {TableNames.users} (name, email, phone, password) VALUES ( @name, @email, @phone, @password)";

            await using (var cmd = new NpgsqlCommand(commandText, con))
            {

                cmd.Parameters.AddWithValue("name", user.Name!);
                cmd.Parameters.AddWithValue("email", user.Email!);
                if (user.Phone == null)
                { cmd.Parameters.AddWithValue("phone", DBNull.Value); }
                else
                { cmd.Parameters.AddWithValue("phone", user.Phone); }

                cmd.Parameters.AddWithValue("password", user.Password!);

                try
                {
                    var res = await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine("result of insert: " + res);
                    if (res == 1)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Got exception " + e);
                    Console.WriteLine(" detail maybe ? : " + e.Message);
                    Console.WriteLine(" data maybe ? : " + e.Data);

                    return false;
                }
            }
            return false;


        }

        public async Task<GetUserDto?> GetUser(Int64 id)
        {
            var con = NewConnection;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                // Console.WriteLine(" connection is open with DB");
            }

            // Console.WriteLine("Trying to find user with id : " + id);
            string commandText = $"SELECT * FROM users WHERE id = @id";

            await using (var cmd = new NpgsqlCommand(commandText, con))
            {
                 NpgsqlDataReader? reader = null ;

                try
                {
                    cmd.Parameters.AddWithValue("id", id);

                    await using ( reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                        {
                            var user = ReadUser(reader);
                             reader.Close();
                            // give back GetUserDto converted from User
                            return GetUserDto.FromUser(user);
                        }

                }
                catch (Exception e)
                {
                    if(reader!=null)
                    {
                        reader.Close();
                    }
                    Console.WriteLine("Got exception " + e);
                    Console.WriteLine(" detail maybe ? : " + e.Message);
                    Console.WriteLine(" data maybe ? : " + e.Data);
                }
            }
            return null;
        }


        private static User ReadUser(NpgsqlDataReader reader)
        {
            int? id = reader["id"] as int?;
            string name = reader["name"] as string ?? "";
            string email = reader["email"] as string ?? "";
            string phone = reader["phone"] as string ?? "";

            User user = new User
            {
                Id = id ?? 0,
                Name = name,
                Email = email,
                Phone = phone,
            };
            return user;
        }
    }

}

