using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;
using Repositroy_And_Services.Services.CustomService.LeaveServices;
using Repositroy_And_Services.Services.CustomService.UserServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _customerService;
        private readonly MainDBContext _context;
        public LeaveController(ILeaveService customerService, MainDBContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        [HttpPost("ApplyLeave")]
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLeaveForHR()
        {

            var leaveApplications = await _customerService.GetAllLeaveForHR();
            return Ok(leaveApplications);
        }


        [HttpPut("ApproveLeave/{leaveId}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Route("GetLeaveByUserId")]
        [HttpGet]
       // [Authorize]
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


        [Route("GetLeaveHistory")]
        [HttpGet]
        public IActionResult GetLeaveHistory(int userId)
        {

            var leaveHistory = _context.Leaves
                .Include(l => l.Users)
                .Where(l => l.UserId == userId)
                .Select(l => new
                {
                    UserId = l.UserId,
                    Username = l.Users.UserName,

                    LeaveRequestTime = l.RequestTime,
                    StartLeaveDate = l.StartLeaveDate,
                    EndLeaveDate = l.EndLeaveDate,
                    Status = l.IsApproved,
                    LeaveStatusTime = l.ApprovalTime,
                    Reason = l.Reason,
                    LeaveType = l.LeaveType,
                })
                .ToList();

            if (leaveHistory.Any())
            {
                return Ok(leaveHistory);
            }


            return NotFound($"No leave history found for user with ID {userId}");
        }


    }
}












