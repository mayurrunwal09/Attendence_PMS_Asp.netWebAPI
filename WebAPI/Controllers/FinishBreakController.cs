using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.ClockOutServices;
using Repositroy_And_Services.Services.CustomService.FinishBreakService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishBreakController : ControllerBase
    {

        private readonly IFinishBreakService _customerService;
        public FinishBreakController(IFinishBreakService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllFinishBreak")]
        [HttpGet]
        public async Task<IActionResult> GetAllFinishBreak()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
        [Route("GetFinishBreak")]
        [HttpGet]
        public async Task<IActionResult> GetFinishBreak(int Id)
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
        [Route("InsertFinishBreak")]
        [HttpPost]
        public async Task<IActionResult> InsertFinishBreak(InsertFinishBreak categoryModel)
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
