namespace MeerPflege.API.DTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }

    public class ResetPasswordDto
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}