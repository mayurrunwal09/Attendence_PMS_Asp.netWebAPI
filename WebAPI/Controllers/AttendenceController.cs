using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;
using Repositroy_And_Services.Services.CustomService.AttendenceServices;
using Repositroy_And_Services.Services.CustomService.BreakServices;
using Repositroy_And_Services.Services.CustomService.UserServices;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceService _customerService;
        public AttendenceController(IAttendenceService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllAttendence")]
        [HttpGet]
        public async Task<IActionResult> GetAllAttendence()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        /*[Route("GetAttendence")]
        [HttpGet]
        public async Task<IActionResult> GetAttendence(int Id)
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

        }*/
        [Route("ClockIn")]
        [HttpPost]
        public async Task<IActionResult> ClockIn(InsertAttendence categoryModel)
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
        [Route("UpdateAttendence")]
        [HttpPut]
        public async Task<IActionResult> UpdateAttendence(UpdateAttendence categoryModel)
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

        [Route("DeleteAttendence")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAttendence(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }

    }
}


