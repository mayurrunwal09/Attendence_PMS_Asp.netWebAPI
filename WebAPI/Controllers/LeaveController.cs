using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositroy_And_Services.Services.CustomService.LeaveServices;
using Repositroy_And_Services.Services.CustomService.UserServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _customerService;
        public LeaveController(ILeaveService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("ApplyLeave")]
        public async Task<IActionResult> ApplyLeave([FromBody] InsertLeave leaveModel)
        {
            var result = await _customerService.ApplyLeave(leaveModel);
            if (result)
            {
                return Ok("Leave application submitted successfully");
            }
            else
            {
                return BadRequest("Failed to submit leave application");
            }
        }


        [HttpGet("GetAllLeaveForHR")]
        public async Task<IActionResult> GetAllLeaveForHR()
        {
                
            var leaveApplications = await _customerService.GetAllLeaveForHR();
            return Ok(leaveApplications);
        }
        [HttpPut("ApproveLeave/{leaveId}")]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> ApproveLeave(int leaveId)
        {
            var result = await _customerService.ApproveLeave(leaveId);
            if (result)
            {
                return Ok("Leave application approved successfully");
            }
            else
            {
                return BadRequest("Failed to approve leave application");
            }
        }

        [HttpPut("RejectLeave/{leaveId}")]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> RejectLeave(int leaveId)
        {
            var result = await _customerService.RejectLeave(leaveId);
            if (result)
            {
                return Ok("Leave application rejected successfully");
            }
            else
            {
                return BadRequest("Failed to reject leave application");
            }
        }
        [HttpGet("GetLeaveByUserId/{userId}")]
        public async Task<IActionResult> GetLeaveByUserId(int userId)
        {
            var leaveData = await _customerService.GetLeaveByUserId(userId);

            if (leaveData != null)
            {
                return Ok(leaveData);
            }
            else
            {
                return NotFound($"Leave data not found for user with ID {userId}");
            }
        }



        [Route("DeleteLeave")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLeave(int Id)
        {
            var result = await _customerService.Delete(Id);
            if (result == true)
                return Ok("Category Deleted SUccessfully...!");
            else
                return BadRequest("Category is not deleted, Please Try again later...!");
        }


       


    }
}
