



using Microsoft.AspNetCore.Mvc;
using Domain.ViewModels;
using Repositroy_And_Services.Services.CustomService.UserServices;
using WebAPI.Middleware.Auth;
using Domain.Models;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;
using Repositroy_And_Services.Services.GenericService;
using Repositroy_And_Services.common;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IJWTAuthManager _authManager;
        private readonly IService<UserType> _serviceUserType;
        private readonly IService<User> _userService;

        public LoginController(
         ILogger<LoginController> logger,
         IService<UserType> serviceUserType,
         IService<User> userService,
         IJWTAuthManager authManager)
        {
            _logger = logger;
            _serviceUserType = serviceUserType;
            _userService = userService;
            _authManager = authManager;
        }

        /* [HttpPost("Register")]
         public async Task<IActionResult> Register(InsertUser userModel)
         {
             try
             {

                 var newUser = new User
                 {
                     UserName = userModel.UserName,
                     Password = Encryptor.EncryptString(userModel.Password),
                     Email = userModel.Email,
                     MobileNo = userModel.MobileNo ,
                     City  = userModel.City ,
                     UserTypeId = userModel.UserTypeId,
                     Role = userModel.Role,

                 };


                 await Task.Run(() => _userService.Insert(newUser));

                 return Ok("Registration successful");
             }
             catch (Exception ex)
             {
                 _logger.LogError($"Error during user registration: {ex.Message}");
                 return StatusCode(500, "Internal server error");
             }
         }*/


        [HttpPost("Register")]
        public async Task<IActionResult> Register(InsertUser userModel)
        {
            try
            {
                // Check if the username already exists
                var existingUser = await _userService.Find(u => u.UserName == userModel.UserName);
                if (existingUser != null)
                {
                    return BadRequest("Username is already taken");
                }

                var newUser = new User
                {
                    UserName = userModel.UserName,
                    Password = Encryptor.EncryptString(userModel.Password),
                    Email = userModel.Email,
                    MobileNo = userModel.MobileNo,
                    City = userModel.City,
                    UserTypeId = userModel.UserTypeId,
                    Role = userModel.Role,
                };

                await Task.Run(() => _userService.Insert(newUser));

                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during user registration: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _userService.Find(u => u.UserName  == loginModel.UserName);

                if (user == null || user.Password != Encryptor.EncryptString(loginModel.Password))
                {
                    return Unauthorized("Invalid username or password");
                }

                var token = _authManager.GenerateJWT(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during user login: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
       






    }

}

