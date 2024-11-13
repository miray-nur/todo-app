using Business.Abstract;
using Data.Enums;
using DataAccess.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Concrete
{
    public class AuthenticateService : IAuthenticateService
    {
        #region Members
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region Constructor
        public AuthenticateService(IUserRepository userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        #endregion

        #region Methods
        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user == null || !_encryptionService.Hash(password).SequenceEqual(user.Password))
            {
                return false;
            }
            return true;
        }

        public string GenerateJwtToken(string email, UserRole role)
        {
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SP10kO8dP4ia0YL6Hfu6jFVaXMiray647291"));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                Audience = "http://localhost:8080",
                Issuer = "http://localhost:8080",
                SigningCredentials = signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion

    }
}
