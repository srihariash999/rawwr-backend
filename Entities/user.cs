namespace Rawwr.Api.Entities
{
    public class User
    {
        public Int64 Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }
    }
}