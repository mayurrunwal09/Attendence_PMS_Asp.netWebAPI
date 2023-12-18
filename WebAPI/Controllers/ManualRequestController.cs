









using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManualRequestController : ControllerBase
    {

        private readonly MainDBContext _context;

        public ManualRequestController(MainDBContext context)
        {
            _context = context;
        }


        [HttpPost("InsertManualRequest")]
        public IActionResult InsertManualRequest([FromBody] InsertManualRequestViewModel requestModel)
        {
            try
            {
                var user = _context.Users.Find(requestModel.UserId);
                if (user == null)
                {
                    return NotFound(new { error = "User not found" });
                }

                var manualRequest = new ManualRequest
                {
                    UserId = requestModel.UserId,
                    AttendenceType = requestModel.AttendenceType,
                    ClockInTime = requestModel.ClockInTime,
                    ClockOutTime = requestModel.ClockOutTime,
                    status = string.IsNullOrWhiteSpace(requestModel.status) ? "Pending" : requestModel.status,
                    EmployeeRemart = requestModel.EmployeeRemart
                };

                _context.ManualRequests.Add(manualRequest);
                _context.SaveChanges();

                return Ok(new { message = "Manual request inserted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }


        [Route("GetManualRequestByUserId")]
        [HttpGet]
        public IActionResult GetManualRequestByUserId(int userId)
        {
            try
            {
                var manualRequests = _context.ManualRequests
                    .Include(m => m.User) 
                    .Where(m => m.UserId == userId)
                    .ToList();

                var manualRequestsDto = manualRequests.Select(m => new
                {
                    id = m.Id,
                    UserId = m.UserId,
                    UserName = m.User.UserName, 
                    AttendenceType = m.AttendenceType,
                    ClockInTime = m.ClockInTime,
                    ClockOutTime = m.ClockOutTime,
                    Status = m.status,
                    EmployeeRemart = m.EmployeeRemart
                });

                return Ok(manualRequestsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }



        [HttpGet("GetAllManualRequests")]
        public IActionResult GetAllManualRequests()
        {
            try
            {
                var manualRequests = _context.ManualRequests
                    .Include(m => m.User) 
                    .ToList();

               
                var manualRequestsDto = manualRequests.Select(m => new
                {
                    id = m.Id,
                    UserId = m.UserId,
                    UserName = m.User.UserName, 
                    AttendenceType = m.AttendenceType,
                    ClockInTime = m.ClockInTime,
                    ClockOutTime = m.ClockOutTime,
                    Status = m.status,
                    EmployeeRemart = m.EmployeeRemart
                });

                return Ok(manualRequestsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }


        [HttpPut("UpdateManualRequestStatus")]
        public IActionResult UpdateManualRequestStatus([FromQuery] int manualRequestId, [FromBody] UpdateStatusModel updateModel)
        {
            try
            {
                var manualRequest = _context.ManualRequests.Find(manualRequestId);

                if (manualRequest == null)
                {
                    return NotFound(new { error = "Manual request not found" });
                }

                manualRequest.status = updateModel.NewStatus;

                _context.SaveChanges();

                return Ok(new { message = "Manual request status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }
        public class UpdateStatusModel
        {
            public string NewStatus { get; set; }
        }
    }
}
