using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _customerService;
        public UserTypeController(IUserTypeService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllUserType")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserType()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        [Route("GetUserType")]
        [HttpGet]
        public async Task<IActionResult> GetUserType(int Id)
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
        [Route("InsertUserType")]
        [HttpPost]
        public async Task<IActionResult> InsertUserType(InsertUserType categoryModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.Insert(categoryModel);
                if (result == true)
                    return Ok("Category Inserted Successfully...!");
                else
                    return BadRequest("Something Went Wrong, Category Is Not Inserted, Please Try After Sometime...!");
            }
            else
                return BadRequest("Invalid Category Information, Please Entering a Valid One...!");

        }
        [Route("UpdateUserType")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserType(UpdateUserType categoryModel)
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

        [Route("DeleteUserType")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserType(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }
    }
}
