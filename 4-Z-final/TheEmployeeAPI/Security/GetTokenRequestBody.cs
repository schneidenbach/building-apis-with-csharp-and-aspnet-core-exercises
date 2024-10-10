namespace TheEmployeeAPI.Security
{
    public class GetTokenRequestBody
    {
        public required string Role { get; set; }
        public required string Username { get; set; }
    }
}