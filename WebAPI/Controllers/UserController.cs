/*using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.UserServices;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService<User> _customerService;
        public UserController(IUserService<User> customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int Id)
        {
            if (Id != null)
            {
                var result = await _customerService.GetById(Id);
                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");
                return Ok(result);
            }
            else
                return NotFound("Invalid Category Id, Please Entering a Valid One...!");

        }





     
        [Route("UpdateUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUser categoryModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.Update(categoryModel);
                if (result == true)
                    return Ok(categoryModel);
                else
                    return BadRequest("Something Went Wrong, Please Try After Sometime...!");
            }
            else
                return BadRequest("Invalid Category Information, Please Entering a Valid One...!");
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }



        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!string.IsNullOrEmpty(resetPasswordModel.Email) && !string.IsNullOrEmpty(resetPasswordModel.NewPassword))
            {
                var success = await _customerService.ResetPassword(resetPasswordModel.Email, resetPasswordModel.NewPassword);

                if (success)
                {
                    return Ok("Password reset successful.");
                }
                else
                {
                    return BadRequest("Invalid email address.");
                }
            }

            return BadRequest("Invalid input.");
        }


        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            // Check if the email is registered
            var user = await _customerService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest("Email not registered.");
            }

            // Generate and send OTP to the user's email
            string otp = await _customerService.GenerateOTP(email);

            // Optionally, you can send a response indicating that the OTP has been sent
            return Ok("OTP sent to the registered email.");
        }

        [HttpPost("VerifyOTPAndResetPassword")]
        public async Task<IActionResult> VerifyOTPAndResetPassword(string email, string enteredOTP, string newPassword)
        {
            // Verify the entered OTP
            if (!_customerService.VerifyOTP(email, enteredOTP))
            {
                return BadRequest("Invalid OTP.");
            }

            // Reset the user's password
            var success = await _customerService.ResetPassword(email, newPassword);

            if (success)
            {
                // Remove the OTP from the dictionary after successful verification and password reset
                _customerService.RemoveOTP(email);

                return Ok("Password reset successful.");
            }

            return BadRequest("Failed to reset the password.");
        }
    }
}
*/








using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.UserServices;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService<User> _customerService;
        public UserController(IUserService<User> customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int Id)
        {
            if (Id != null)
            {
                var result = await _customerService.GetById(Id);
                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");
                return Ok(result);
            }
            else
                return NotFound("Invalid Category Id, Please Entering a Valid One...!");

        }






        [Route("UpdateUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUser categoryModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.Update(categoryModel);
                if (result == true)
                    return Ok(categoryModel);
                else
                    return BadRequest("Something Went Wrong, Please Try After Sometime...!");
            }
            else
                return BadRequest("Invalid Category Information, Please Entering a Valid One...!");
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }



        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!string.IsNullOrEmpty(resetPasswordModel.Email) && !string.IsNullOrEmpty(resetPasswordModel.NewPassword))
            {
                var success = await _customerService.ResetPassword(resetPasswordModel.Email, resetPasswordModel.NewPassword);

                if (success)
                {
                    return Ok("Password reset successful.");
                }
                else
                {
                    return BadRequest("Invalid email address.");
                }
            }

            return BadRequest("Invalid input.");
        }


     /*   [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            // Check if the email is registered
            var user = await _customerService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest("Email not registered.");
            }

            // Generate and send OTP to the user's email
            string otp = await _customerService.GenerateOTP(email);

            // Optionally, you can send a response indicating that the OTP has been sent
            return Ok("OTP sent to the registered email.");
        }

        [HttpPost("VerifyOTPAndResetPassword")]
        public async Task<IActionResult> VerifyOTPAndResetPassword(string email, string enteredOTP, string newPassword)
        {
            // Verify the entered OTP
            if (!_customerService.VerifyOTP(email, enteredOTP))
            {
                return BadRequest("Invalid OTP.");
            }

            // Reset the user's password
            var success = await _customerService.ResetPassword(email, newPassword);

            if (success)
            {
                // Remove the OTP from the dictionary after successful verification and password reset
                _customerService.RemoveOTP(email);

                return Ok("Password reset successful.");
            }

            return BadRequest("Failed to reset the password.");
        }*/
    }
}
