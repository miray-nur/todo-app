using Business.ResultPattern.Result;
using Data.Entities;
using DataAccess.DTO.UserDto;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<Result> Register(AddUserDto dto);
        Task<Result> Update(UpdateUserDto dto);
        Task<Result> Delete(int id);
        Task<bool> Login(LoginUserDto dto);
        Task<DataResult<List<User>>> GetAll();
        Task<DataResult<User>> GetById(int Id);
        Task<DataResult<User>> GetByEmail(string email);
        Task<Result> UpgradeUserToAdmin(int UserId);

    }
}
