using Data.Enums;

namespace Business.Abstract
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email, string password);
        string GenerateJwtToken(string email, UserRole role);
    }
}
