using Business.Abstract;
using Business.ResultPattern.Result;
using Business.ValidationServices;
using Data.Entities;
using Data.Enums;
using DataAccess.Abstract;
using DataAccess.DTO.UserDto;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        #region Members

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _hashService;
        private readonly UserValidatorService _userValidatorService;
        private readonly IEmailService emailService;
        private readonly IQueueService _emailQueue;

        #endregion

        #region Contructor
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IEncryptionService hashService, UserValidatorService userValidatorService, IEmailService notificationService, IQueueService emailQueue)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _hashService = hashService;
            _userValidatorService = userValidatorService;
            emailService = notificationService;
            _emailQueue = emailQueue;
        }

        #endregion

        #region Methods
        public async Task<Result> Register(AddUserDto dto)
        {
            var errorMessages = await _userValidatorService.ValidateUserInfo(dto);

            if (errorMessages.Any())
            {
                return Result.Fail(string.Join(", ", errorMessages));
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = _hashService.Hash(dto.Password),
                Role = UserRole.Member,
                Email = dto.Email,
                CreatedDate = DateTime.Now
            };

            await _userRepository.Add(user);
            await _unitOfWork.Save();

            _emailQueue.Publisher(new EmailModel { ToMail = user.Email, Subject = "registry", Body = "registration completed successfully" });
            return Result.Ok("registration completed successfully");
        }

        public async Task<Result> Update(UpdateUserDto dto)
        {
            var errorMessages = await _userValidatorService.ValidateUserInfo(dto);

            if (errorMessages.Any())
            {
                return Result.Fail(string.Join(", ", errorMessages));
            }

            var existingUser = await _userRepository.GetById(dto.Id);

            if (existingUser == null)
            {
                return Result.Fail("Kullanıcı bulunamadı.");
            }

            existingUser.FirstName = dto.FirstName;
            existingUser.LastName = dto.LastName;
            existingUser.Email = dto.Email;

            await _userRepository.Update(existingUser);
            await _unitOfWork.Save();

            _emailQueue.Publisher(new EmailModel { ToMail = existingUser.Email, Subject = "update", Body = "update completed successfully" });
            return Result.Ok("update complated successfully");

        }

        public async Task<Result> Delete(int id)
        {
            if (id > 0)
            {
                var isDeleted = await _userRepository.Delete(id);
                if (isDeleted == true)
                {
                    await _unitOfWork.Save();
                    return Result.Ok("user deleted");
                }
            }

            return Result.Fail("invalid user id");

        }

        public async Task<bool> Login(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email);

            if (user == null)
            {
                return false;
            }

            var hashedPassword = _hashService.Hash(dto.Password);
            bool isValidPassword = hashedPassword.SequenceEqual(user.Password);

            if (isValidPassword)
            {
                _emailQueue.Publisher(new EmailModel { ToMail = user.Email, Subject = "login", Body = "login completed successfully" });
            }
            return isValidPassword;
        }

        public async Task<DataResult<List<User>>> GetAll()
        {
            var user = await _userRepository.GetAll();
            return new DataResult<List<User>>(user, true, "Bütün kullanıcılar listelendi.");
        }

        public async Task<DataResult<User>> GetById(int Id)
        {
            var result = await _userRepository.GetById(Id);
            return new DataResult<User>(result, true, "user found");
        }

        public async Task<DataResult<User>> GetByEmail(string email)
        {
            var result = await _userRepository.GetByEmail(email);
            return new DataResult<User>(result, true, "user found");
        }

        public async Task<Result> UpgradeUserToAdmin(int UserId)
        {
            var user = await _userRepository.GetById(UserId);

            if (user == null)
            {
                return Result.Fail("user not found");
            }

            user.Role = UserRole.Admin;

            await _userRepository.Update(user);
            await _unitOfWork.Save();

            return Result.Ok("role upgraded successfully");
        }

        #endregion
    }
}
