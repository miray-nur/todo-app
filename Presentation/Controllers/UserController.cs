using Business.Abstract;
using Business.Concrete;
using DataAccess.DTO.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Members

        private readonly IUserService _userService;
        private readonly AuthenticateService _authService;

        #endregion

        #region Constructor
        public UserController(IUserService userService, AuthenticateService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Create(AddUserDto AddUser)
        {
            var result = await _userService.Register(AddUser);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var user = await _userService.GetByEmail(dto.Email);

            if (!user.Success)
            {
                return Unauthorized(new { message = "Geçersiz kullanıcı adı/şifre" });
            }

            var isValidUser = await _userService.Login(dto);

            if (isValidUser)
            {
                var token = _authService.GenerateJwtToken(user.Data.Email, user.Data.Role);
                return Ok(new { token });
            }

            return Unauthorized(new { message = "Geçersiz kullanıcı adı/şifre" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpPut]
        [Authorize]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            var currentUser = await _userService.Update(userDto);
            if (!currentUser.Success)
            {
                return BadRequest("could not be updated");
            }
            else
            {
                return Ok("Successful");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (!result.Success)
            {
                return NotFound("could not found");
            }
            else
            {
                return Ok("successful");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("UpgradeRole")]
        public async Task<IActionResult> UpgradeUserToAdmin(int userId)
        {
            var result = await _userService.UpgradeUserToAdmin(userId);
            if (!result.Success)
            {
                return NotFound("could not upgrade");
            }
            else
            {
                return Ok("successful");
            }
        }

        #endregion

    }
}
