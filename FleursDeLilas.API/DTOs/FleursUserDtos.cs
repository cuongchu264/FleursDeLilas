namespace FleursDeLilas.API.DTOs
{
    public class FleursUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateFleursUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UpdateFleursUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
