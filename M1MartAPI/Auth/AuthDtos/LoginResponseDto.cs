namespace M1MartAPI.Auth.AuthDtos
{
    public class LoginResponseDto
    {
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
