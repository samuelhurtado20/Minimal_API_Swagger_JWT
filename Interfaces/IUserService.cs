using Minimal_API_Swagger_JWT.Models;

namespace Minimal_API_Swagger_JWT.Interfaces
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
