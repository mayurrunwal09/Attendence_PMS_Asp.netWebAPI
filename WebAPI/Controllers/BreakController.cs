using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.BreakServices;
using Repositroy_And_Services.Services.CustomService.ReportServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakController : ControllerBase
    {
        private readonly IBreakService _customerService;
        public BreakController(IBreakService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAllBreak")]
        [HttpGet]
        public async Task<IActionResult> GetAllBreak()
        {
            var res = await _customerService.GetAll();
            if (res == null)
                return BadRequest("No records found");
            return Ok(res);
        }
       
        [Route("InsertBreak")]
        [HttpPost]
        public async Task<IActionResult> InsertBreak(InsertBreak categoryModel)
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
   
        [Route("DeleteBreak")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBreak(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }
    }
}
