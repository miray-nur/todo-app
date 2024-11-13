using DataAccess.Abstract;
using DataAccess.DTO.UserDto;

namespace Business.ValidationServices
{
    public class UserValidatorService
    {
        #region Members

        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor
        public UserValidatorService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Methods
        public async Task<List<string>> ValidateUserInfo(AddUserDto userDto)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(userDto.FirstName))
            {
                errors.Add("The name field cannot be empty.");
            }

            var userMail = await _userRepository.GetByEmail(userDto.Email);
            if (userMail != null)
            {
                errors.Add("There is such a user.");
            }

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                errors.Add("Password cannot be empty.");
            }
            else if (userDto.Password.Length < 8)
            {
                errors.Add("Password must be at least 8 characters.");
            }

            return errors;
        }

        public async Task<List<string>> ValidateUserInfo(UpdateUserDto userDto)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(userDto.FirstName))
            {
                errors.Add("The name field cannot be empty.");
            }

            var userMail = await _userRepository.GetByEmail(userDto.Email);
            if (userMail != null)
            {
                errors.Add("There is such a user.");
            }

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                errors.Add("Password cannot be empty.");
            }
            else if (userDto.Password.Length < 8)
            {
                errors.Add("Password must be at least 8 characters.");
            }

            return errors;
        }

        #endregion

    }
}
