using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.EventServices;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _customerService;
        public EventController(IEventService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllEvents")]
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        [Route("GetEventById")]
        [HttpGet]
        public async Task<IActionResult> GetEventById(int Id)
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
        [Route("InsertEvent")]
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertEvent(InsertEvent categoryModel)
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
        [Route("UpdateEvent")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(UpdateEvent categoryModel)
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

        [Route("DeleteEvent")]
        [HttpDelete]
        
        public async Task<IActionResult> DeleteEvent(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }
    }
}
