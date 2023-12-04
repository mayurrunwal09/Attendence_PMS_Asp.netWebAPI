using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;
using Repositroy_And_Services.Services.CustomService.ClockOutServices;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClockOutController : ControllerBase
    {
        private readonly IClockOutService _customerService;
        public ClockOutController(IClockOutService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllClockOut")]
        [HttpGet]
        public async Task<IActionResult> GetAllClockOut()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        [Route("GetClockOut")]
        [HttpGet]
        public async Task<IActionResult> GetClockOut(int Id)
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
        [Route("InserClockOut")]
        [HttpPost]
        public async Task<IActionResult> InserClockOut(InserClockOut categoryModel)
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
       

      
    }
}
