using Minimal_API_Swagger_JWT.Interfaces;
using Minimal_API_Swagger_JWT.Models;
using Minimal_API_Swagger_JWT.Repositories;

namespace Minimal_API_Swagger_JWT.Services
{
    public class UserService : IUserService
    {
        public User Get(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase)
            && o.Password.Equals(userLogin.Password));

            return user;
        }
    }
}
