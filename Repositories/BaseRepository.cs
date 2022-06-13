
using Npgsql;
using Rawwr.Settings;

namespace Rawwr.Api.Repositories
{
    public class BaseRepository
    {

        private readonly IConfiguration _configuration;

        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection NewConnection => new NpgsqlConnection(_configuration
                .GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString);



    }
}