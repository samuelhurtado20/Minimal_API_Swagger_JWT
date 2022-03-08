using Minimal_API_Swagger_JWT.Models;

namespace Minimal_API_Swagger_JWT.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new User { Username = "samuel123", EmailAddress = "Samuel@gmail.com", Password="samuel123", Role = "Administrator" }
        };
    }
}
