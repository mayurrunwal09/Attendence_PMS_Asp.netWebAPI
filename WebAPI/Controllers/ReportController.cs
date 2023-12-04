/*


using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Repositroy_And_Services.context;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly MainDBContext _context;

        public ReportController(MainDBContext context)
        {
            _context = context;
        }

        [HttpGet("UserClockInHistory/{userId}")]
        public async Task<IActionResult> GetUserClockInHistory(int userId)
        {
            try
            {
                var clockInHistory = await _context.Attenants
                    .Where(a => a.UserId == userId)
                    .OrderBy(a => a.CheckInTime)
                    .Select(a => new
                    {
                        UserId = userId,
                        ClockInTime = a.CheckInTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(clockInHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UserClockOutHistory/{userId}")]
        public async Task<IActionResult> GetUserClockOutHistory(int userId)
        {
            try
            {
                var clockOutHistory = await _context.ClockOuts
                    .Where(c => c.UserId == userId)
                    .OrderByDescending(c => c.ClockOutTime)
                    .Select(c => new
                    {
                        UserId = userId,
                        ClockOutTime = c.ClockOutTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(clockOutHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UserBreakTimeHistory/{userId}")]
        public async Task<IActionResult> GetUserBreakTimeHistory(int userId)
        {
            try
            {
                var breakTimeHistory = await _context.Breaks
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.RequestTime)
                    .Select(b => new
                    {
                        UserId = userId,
                        StartBreakTime = b.RequestTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(breakTimeHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UserFinishBreakHistory/{userId}")]
        public async Task<IActionResult> GetUserFinishBreakHistory(int userId)
        {
            try
            {
                var finishBreakHistory = await _context.FinishBreaks
                    .Where(f => f.UserId == userId)
                    .OrderByDescending(f => f.FinishTime)
                    .Select(f => new
                    {
                        UserId = userId,
                        FinishBreakTime = f.FinishTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(finishBreakHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("UserHours/{userId}")]
        public async Task<IActionResult> GetUserHours(int userId)
        {
            try
            {
                var clockInTime = await _context.Attenants
                    .Where(a => a.UserId == userId)
                    .OrderBy(a => a.CheckInTime)
                    .Select(a => a.CheckInTime)
                    .FirstOrDefaultAsync();

                var clockOutTime = await _context.ClockOuts
                    .Where(c => c.UserId == userId)
                    .OrderByDescending(c => c.ClockOutTime)
                    .Select(c => c.ClockOutTime)
                    .FirstOrDefaultAsync();

                var startBreakTime = await _context.Breaks
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.RequestTime)
                    .Select(b => b.RequestTime)
                    .FirstOrDefaultAsync();

                var finishBreakTime = await _context.FinishBreaks
                    .Where(f => f.UserId == userId)
                    .OrderByDescending(f => f.FinishTime)
                    .Select(f => f.FinishTime)
                    .FirstOrDefaultAsync();

                var totalHours = clockOutTime - clockInTime;
                var productiveHours = await CalculateProductiveHours(userId);

                var result = new
                {
                    UserId = userId,
                    ClockInTime = clockInTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    ClockOutTime = clockOutTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    StartBreakTime = startBreakTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    FinishBreakTime = finishBreakTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    ActualHours = totalHours.ToString(@"hh\:mm\:ss"), // Format for HH:mm:ss
                    ProductiveHours = productiveHours?.ToString(@"hh\:mm\:ss")
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        private async Task<TimeSpan?> CalculateProductiveHours(int userId)
        {
            try
            {
                var breaks = await _context.Breaks
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.RequestTime)
                    .ToListAsync();

                var finishBreaks = await _context.FinishBreaks
                    .Where(f => f.UserId == userId)
                    .OrderByDescending(f => f.FinishTime)
                    .ToListAsync();

                // Consider any other criteria or business rules to calculate productive hours
                // For now, subtract break times from total hours
                var totalBreakTime = TimeSpan.Zero;

                foreach (var breakItem in breaks)
                {
                    var finishBreak = finishBreaks.FirstOrDefault(f => f.Id == breakItem.Id);
                    if (finishBreak != null)
                    {
                        totalBreakTime += (finishBreak.FinishTime - breakItem.RequestTime);
                    }
                }

                return totalBreakTime;
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                Console.WriteLine($"Error calculating productive hours: {ex.Message}");
                return null;
            }
        }

    }

}


*/












using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Repositroy_And_Services.context;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly MainDBContext _context;
        public ReportController(MainDBContext context)
        {
            _context = context;
        }


        [HttpGet("UserClockInHistory/{userId}")]
        public async Task<IActionResult> GetUserClockInHistory(int userId)
        {
            try
            {
                var clockInHistory = await _context.Attenants
                    .Where(a => a.UserId == userId)
                    .OrderBy(a => a.CheckInTime)
                    .Select(a => new
                    {
                        UserId = userId,
                        ClockInTime = a.CheckInTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(clockInHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UserClockOutHistory/{userId}")]
        public async Task<IActionResult> GetUserClockOutHistory(int userId)
        {
            try
            {
                var clockOutHistory = await _context.ClockOuts
                    .Where(c => c.UserId == userId)
                    .OrderByDescending(c => c.ClockOutTime)
                    .Select(c => new
                    {
                        UserId = userId,
                        ClockOutTime = c.ClockOutTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(clockOutHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UserBreakTimeHistory/{userId}")]
        public async Task<IActionResult> GetUserBreakTimeHistory(int userId)
        {
            try
            {
                var breakTimeHistory = await _context.Breaks
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.RequestTime)
                    .Select(b => new
                    {
                        UserId = userId,
                        StartBreakTime = b.RequestTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(breakTimeHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UserFinishBreakHistory/{userId}")]
        public async Task<IActionResult> GetUserFinishBreakHistory(int userId)
        {
            try
            {
                var finishBreakHistory = await _context.FinishBreaks
                    .Where(f => f.UserId == userId)
                    .OrderByDescending(f => f.FinishTime)
                    .Select(f => new
                    {
                        UserId = userId,
                        FinishBreakTime = f.FinishTime.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(finishBreakHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("DataById/{userId}")]
        public async Task<ActionResult<int>> DataById(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid User ID, Please provide a valid ID...!");

            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return BadRequest("User with the specified ID does not exist...!");

            var allClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            var allClockInTimes = await _context.Attenants
                .Where(c => c.UserId == userId)
                .Select(c => c.CheckInTime)
                .ToListAsync();

            var allStartbreakTime = await _context.Breaks
                .Where(c => c.UserId == userId)
                .Select(c => c.RequestTime)
                .ToListAsync();

            var allFinishbreakTime = await _context.FinishBreaks
                .Where(c => c.UserId == userId)
                .Select(c => c.FinishTime)
                .ToListAsync();

            var clockInTimes = allClockInTimes
                .Where(c => c != null)
                .ToList();

            var clockOutTimes = allClockOutTimes
                .ToList();

            var groupedByDate = clockInTimes
                .GroupBy(checkInTime => checkInTime.Date);

            var result = new List<object>();

            foreach (var group in groupedByDate)
            {
                var date = group.Key;

                var clockInTimesForDate = group.ToList();
                var startBreakTimes = allStartbreakTime
                    .Where(s => s.Date == date)
                    .Select(s => s)
                    .ToList();

                var finishBreakTimes = allFinishbreakTime
                    .Where(f => f.Date == date)
                    .Select(f => f)
                    .ToList();

                var totalProductiveHoursResult = await CalculateProductiveHours(userId);
                if (totalProductiveHoursResult.Result is OkObjectResult okResult)
                {
                    var totalProductiveHours = (TimeSpan)okResult.Value;

                    var totalHoursResult = await CalculateTotalHours(userId);
                    if (totalHoursResult.Result is OkObjectResult totalHoursOkResult)
                    {
                        var totalHours = (TimeSpan)totalHoursOkResult.Value;

                        var resultObject = new
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Date = date,
                            ClockInTimes = clockInTimesForDate,
                            ClockOutTimes = clockOutTimes,
                            StartBreakTimes = startBreakTimes,
                            FinishBreakTimes = finishBreakTimes,
                            TotalProductiveHours = totalProductiveHours,
                            TotalHours = totalHours,
                        };

                        result.Add(resultObject);
                    }
                }
            }

            return Ok(result);
        }
        [HttpGet("CalculateTotalHours/{userId}")]
        public async Task<ActionResult<TimeSpan>> CalculateTotalHours(int userId)
        {
            // Retrieve clock-in times directly from the database
            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            // Retrieve clock-out times directly from the database
            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            // Check if there are any attendances
            if (!userClockInTimes.Any())
            {
                return NotFound($"Attendances for user with id {userId} not found.");
            }

            TimeSpan totalTime = TimeSpan.Zero;

            // Iterate over each attendance
            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                // Retrieve clock-in and clock-out times directly from the lists
                var clockinTime = userClockInTimes[i];
                var clockoutTime = i < userClockOutTimes.Count ? userClockOutTimes[i] : default;

                if (clockoutTime != default)
                {
                    // Log information
                    Console.WriteLine($"Check-in: {clockinTime}, Clock-out: {clockoutTime}");

                    var timeDiff = clockoutTime - clockinTime;
                    totalTime += timeDiff;
                }
                else
                {
                    // Log if clock-out time is not found
                    Console.WriteLine($"Clock-out time not found for user ID: {userId}, Attendance index: {i}");
                }
            }

            // Log total time
            Console.WriteLine($"Total time: {totalTime}");

            TimeSpan absoluteTime = totalTime.Duration();

            return Ok(absoluteTime);
        }






        [HttpGet("CalculateProductiveHours/{userId}")]
        public async Task<ActionResult<TimeSpan>> CalculateProductiveHours(int userId)
        {
            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            var userStartBreakTimes = await _context.Breaks
                .Where(s => s.UserId == userId)
                .Select(s => s.RequestTime)
                .ToListAsync();

            var userFinishBreakTimes = await _context.FinishBreaks
                .Where(f => f.UserId == userId)
                .Select(f => f.FinishTime)
                .ToListAsync();

            if (!userClockInTimes.Any() || !userClockOutTimes.Any())
            {
                return NotFound($"Clock-in or Clock-out times for user with id {userId} not found.");
            }

            TimeSpan totalTime = TimeSpan.Zero;


            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                var clockInTime = userClockInTimes[i];
                var clockOutTime = userClockOutTimes.ElementAtOrDefault(i);

                if (clockOutTime != default)
                {
                    totalTime += clockOutTime - clockInTime;
                }


                var startBreakTime = userStartBreakTimes.ElementAtOrDefault(i);
                var finishBreakTime = userFinishBreakTimes.ElementAtOrDefault(i);

                if (startBreakTime != default && finishBreakTime != default)
                {
                    totalTime -= finishBreakTime - startBreakTime;
                }
            }

            TimeSpan absoluteTime = totalTime.Duration();

            return Ok(absoluteTime);
        }




    }

}


