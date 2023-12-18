/*
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Repositroy_And_Services.context;
using Microsoft.AspNetCore.Authorization;
using Repositroy_And_Services.Services.CustomService.ReportServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly MainDBContext _context;
        private readonly IReportService _reportService;
        public ReportController(MainDBContext context, IReportService reportService)
        {
            _context = context;
            _reportService = reportService;
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

            var allStartBreakTime = await _context.Breaks
                .Where(c => c.UserId == userId)
                .Select(c => c.RequestTime)
                .ToListAsync();

            var allFinishBreakTime = await _context.FinishBreaks
                .Where(c => c.UserId == userId)
                .Select(c => c.FinishTime)
                .ToListAsync();

            var clockInTimes = allClockInTimes
                .Where(c => c != null)
                .ToList();

            var groupedByDate = clockInTimes
                .GroupBy(checkInTime => checkInTime.Date);

            var result = new List<object>();

            foreach (var group in groupedByDate)
            {
                var date = group.Key;

                var clockInTimesForDate = group.ToList();
                var startBreakTimes = allStartBreakTime
                    .Where(s => s.Date == date)
                    .ToList();

                var finishBreakTimes = allFinishBreakTime
                    .Where(f => f.Date == date)
                    .ToList();

                // Filter clock-out times based on the date
                var clockOutTimesForDate = allClockOutTimes
                    .Where(c => c.Date == date)
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
                            ClockOutTimes = clockOutTimesForDate,  // Use filtered clock-out times
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
            // Get today's date
            DateTime today = DateTime.Today;

            // Retrieve clock-in times directly from the database for the specific date
            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime.Date == today)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            // Retrieve clock-out times directly from the database for the specific date
            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime.Date == today)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            if (!userClockInTimes.Any())
            {
                return NotFound($"Attendances for user with id {userId} on {today.ToShortDateString()} not found.");
            }

            TimeSpan totalTime = TimeSpan.Zero;

            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                var clockinTime = userClockInTimes[i];
                var clockoutTime = i < userClockOutTimes.Count ? userClockOutTimes[i] : default;

                if (clockoutTime != default)
                {
                    var timeDiff = clockoutTime - clockinTime;
                    totalTime += timeDiff;
                }
                else
                {
                    Console.WriteLine($"Clock-out time not found for user ID: {userId}, Attendance index: {i}");
                }
            }

            TimeSpan absoluteTime = totalTime.Duration();

            return Ok(absoluteTime);
        }

        [HttpGet("CalculateProductiveHours/{userId}")]
        public async Task<ActionResult<TimeSpan>> CalculateProductiveHours(int userId)
        {

            DateTime today = DateTime.Today;


            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime.Date == today)
                .Select(a => a.CheckInTime)
                .ToListAsync();


            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime.Date == today)
                .Select(c => c.ClockOutTime)
                .ToListAsync();


            var userStartBreakTimes = await _context.Breaks
                .Where(s => s.UserId == userId && s.RequestTime.Date == today)
                .Select(s => s.RequestTime)
                .ToListAsync();


            var userFinishBreakTimes = await _context.FinishBreaks
                .Where(f => f.UserId == userId && f.FinishTime.Date == today)
                .Select(f => f.FinishTime)
                .ToListAsync();

            if (!userClockInTimes.Any() || !userClockOutTimes.Any())
            {
                return NotFound($"Clock-in or Clock-out times for user with id {userId} on {today.ToShortDateString()} not found.");
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



        [Route("GetUserBreakDuration")]
        [HttpGet]
        public async Task<IActionResult> GetUserBreakDuration(int userId)
        {
            try
            {
                var userBreakDetails = await _context.Breaks
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.RequestTime)
                    .ToListAsync();

                if (userBreakDetails == null || userBreakDetails.Count == 0)
                {
                    return NotFound($"Break details for user with id {userId} not found.");
                }

                var breakDetails = new List<object>();

                foreach (var breakDetail in userBreakDetails)
                {
                    var finishBreak = await _context.FinishBreaks
                        .Where(f => f.UserId == userId && f.FinishTime > breakDetail.RequestTime)
                        .OrderBy(f => f.FinishTime)
                        .FirstOrDefaultAsync();

                    var startBreakTime = breakDetail.RequestTime;
                    var finishBreakTime = finishBreak?.FinishTime;
                    var breakDuration = finishBreakTime != null ? (finishBreakTime.Value - startBreakTime) : TimeSpan.Zero;

                    var productiveHours = await CalculateProductiveHoursForBreak(userId, startBreakTime, finishBreakTime);

                    var breakObject = new
                    {
                        UserId = userId,
                        BreakStartTime = startBreakTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        FinishBreakTime = finishBreakTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                        BreakDuration = breakDuration.ToString(@"hh\:mm\:ss"),
                        ProductiveHours = productiveHours
                    };

                    breakDetails.Add(breakObject);
                }

                return Ok(breakDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<TimeSpan?> CalculateProductiveHoursForBreak(int userId, DateTime startBreakTime, DateTime? finishBreakTime)
        {
            if (finishBreakTime == null)
            {
                return null;
            }

            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime >= startBreakTime && a.CheckInTime <= finishBreakTime)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime >= startBreakTime && c.ClockOutTime <= finishBreakTime)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            var userStartBreakTimes = await _context.Breaks
                .Where(b => b.UserId == userId && b.RequestTime >= startBreakTime && b.RequestTime <= finishBreakTime)
                .Select(b => b.RequestTime)
                .ToListAsync();

            var userFinishBreakTimes = await _context.FinishBreaks
                .Where(f => f.UserId == userId && f.FinishTime >= startBreakTime && f.FinishTime <= finishBreakTime)
                .Select(f => f.FinishTime)
                .ToListAsync();

            var totalTime = TimeSpan.Zero;

            // Add productive hours for check-in and clock-out
            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                var clockInTime = userClockInTimes[i];
                var clockOutTime = i < userClockOutTimes.Count ? userClockOutTimes[i] : default;

                if (clockOutTime != default)
                {
                    totalTime += clockOutTime - clockInTime;
                }
            }

            // Subtract break durations
            for (int i = 0; i < userStartBreakTimes.Count; i++)
            {
                var startBreakTimeWithinRange = userStartBreakTimes[i];
                var finishBreakTimeWithinRange = userFinishBreakTimes.ElementAtOrDefault(i);

                if (finishBreakTimeWithinRange != default)
                {
                    totalTime -= finishBreakTimeWithinRange - startBreakTimeWithinRange;
                }
            }

            return totalTime.Duration();
        }








        [Route("DataByUserId")]
        [HttpGet]
        public async Task<ActionResult<int>> DataByUserId(int userId)
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

            var allStartBreakTime = await _context.Breaks
                .Where(c => c.UserId == userId)
                .Select(c => c.RequestTime)
                .ToListAsync();

            var allFinishBreakTime = await _context.FinishBreaks
                .Where(c => c.UserId == userId)
                .Select(c => c.FinishTime)
                .ToListAsync();

            var clockInTimes = allClockInTimes
                .Where(c => c != null)
                .ToList();

            var groupedByDate = clockInTimes
                .GroupBy(checkInTime => checkInTime.Date);

            var result = new List<object>();

            foreach (var group in groupedByDate)
            {
                var date = group.Key;

                var clockInTimesForDate = group.ToList();
                var startBreakTimes = allStartBreakTime
                    .Where(s => s.Date == date)
                    .ToList();

                var finishBreakTimes = allFinishBreakTime
                    .Where(f => f.Date == date)
                    .ToList();

                // Filter clock-out times based on the date
                var clockOutTimesForDate = allClockOutTimes
                    .Where(c => c.Date == date)
                    .ToList();

                var totalProductiveHoursResult = await CalculateProductiveHours(userId);
                if (totalProductiveHoursResult.Result is OkObjectResult okResult)
                {
                    var totalProductiveHours = (TimeSpan)okResult.Value;

                    var totalHoursResult = await CalculateTotalHours(userId);
                    if (totalHoursResult.Result is OkObjectResult totalHoursOkResult)
                    {
                        var totalHours = (TimeSpan)totalHoursOkResult.Value;

                        // Calculate break durations
                        var breakDurations = new List<TimeSpan>();
                        for (int i = 0; i < startBreakTimes.Count; i++)
                        {
                            if (i < finishBreakTimes.Count)
                            {
                                var breakStart = startBreakTimes[i];
                                var breakFinish = finishBreakTimes[i];
                                var breakDuration = breakFinish - breakStart;
                                breakDurations.Add(breakDuration);
                            }
                        }

                        var resultObject = new
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Date = date,
                            ClockInTimes = clockInTimesForDate,
                            ClockOutTimes = clockOutTimesForDate, 
                            StartBreakTimes = startBreakTimes,
                            FinishBreakTimes = finishBreakTimes,
                            BreakDurations = breakDurations,  
                            TotalProductiveHours = totalProductiveHours,
                            TotalHours = totalHours,
                        };

                        result.Add(resultObject);
                    }
                }
            }

            return Ok(result);
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
using Microsoft.AspNetCore.Authorization;
using Repositroy_And_Services.Services.CustomService.ReportServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly MainDBContext _context;
        private readonly IReportService _reportService;
        public ReportController(MainDBContext context, IReportService reportService)
        {
            _context = context;
            _reportService = reportService;
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

            var allStartBreakTime = await _context.Breaks
                .Where(c => c.UserId == userId)
                .Select(c => c.RequestTime)
                .ToListAsync();

            var allFinishBreakTime = await _context.FinishBreaks
                .Where(c => c.UserId == userId)
                .Select(c => c.FinishTime)
                .ToListAsync();

            var clockInTimes = allClockInTimes
                .Where(c => c != null)
                .ToList();

            var groupedByDate = clockInTimes
                .GroupBy(checkInTime => checkInTime.Date);

            var result = new List<object>();

            foreach (var group in groupedByDate)
            {
                var date = group.Key;

                var clockInTimesForDate = group.ToList();
                var startBreakTimes = allStartBreakTime
                    .Where(s => s.Date == date)
                    .ToList();

                var finishBreakTimes = allFinishBreakTime
                    .Where(f => f.Date == date)
                    .ToList();

                // Filter clock-out times based on the date
                var clockOutTimesForDate = allClockOutTimes
                    .Where(c => c.Date == date)
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
                            ClockOutTimes = clockOutTimesForDate,  // Use filtered clock-out times
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
            // Get today's date
            DateTime today = DateTime.Today;

            // Retrieve clock-in times directly from the database for the specific date
            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime.Date == today)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            // Retrieve clock-out times directly from the database for the specific date
            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime.Date == today)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            if (!userClockInTimes.Any())
            {
                return NotFound($"Attendances for user with id {userId} on {today.ToShortDateString()} not found.");
            }

            TimeSpan totalTime = TimeSpan.Zero;

            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                var clockinTime = userClockInTimes[i];
                var clockoutTime = i < userClockOutTimes.Count ? userClockOutTimes[i] : default;

                if (clockoutTime != default)
                {
                    var timeDiff = clockoutTime - clockinTime;
                    totalTime += timeDiff;
                }
                else
                {
                    Console.WriteLine($"Clock-out time not found for user ID: {userId}, Attendance index: {i}");
                }
            }

            TimeSpan absoluteTime = totalTime.Duration();

            return Ok(absoluteTime);
        }

        [HttpGet("CalculateProductiveHours/{userId}")]
        public async Task<ActionResult<TimeSpan>> CalculateProductiveHours(int userId)
        {

            DateTime today = DateTime.Today;


            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime.Date == today)
                .Select(a => a.CheckInTime)
                .ToListAsync();


            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime.Date == today)
                .Select(c => c.ClockOutTime)
                .ToListAsync();


            var userStartBreakTimes = await _context.Breaks
                .Where(s => s.UserId == userId && s.RequestTime.Date == today)
                .Select(s => s.RequestTime)
                .ToListAsync();


            var userFinishBreakTimes = await _context.FinishBreaks
                .Where(f => f.UserId == userId && f.FinishTime.Date == today)
                .Select(f => f.FinishTime)
                .ToListAsync();

            if (!userClockInTimes.Any() || !userClockOutTimes.Any())
            {
                return NotFound($"Clock-in or Clock-out times for user with id {userId} on {today.ToShortDateString()} not found.");
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



        [Route("GetUserBreakDuration")]
        [HttpGet]
        public async Task<IActionResult> GetUserBreakDuration(int userId)
        {
            try
            {
                var userBreakDetails = await _context.Breaks
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.RequestTime)
                    .ToListAsync();

                if (userBreakDetails == null || userBreakDetails.Count == 0)
                {
                    return NotFound($"Break details for user with id {userId} not found.");
                }

                var breakDetails = new List<object>();

                foreach (var breakDetail in userBreakDetails)
                {
                    var finishBreak = await _context.FinishBreaks
                        .Where(f => f.UserId == userId && f.FinishTime > breakDetail.RequestTime)
                        .OrderBy(f => f.FinishTime)
                        .FirstOrDefaultAsync();

                    var startBreakTime = breakDetail.RequestTime;
                    var finishBreakTime = finishBreak?.FinishTime;
                    var breakDuration = finishBreakTime != null ? (finishBreakTime.Value - startBreakTime) : TimeSpan.Zero;

                    var productiveHours = await CalculateProductiveHoursForBreak(userId, startBreakTime, finishBreakTime);

                    var breakObject = new
                    {
                        UserId = userId,
                        BreakStartTime = startBreakTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        FinishBreakTime = finishBreakTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                        BreakDuration = breakDuration.ToString(@"hh\:mm\:ss"),
                        ProductiveHours = productiveHours
                    };

                    breakDetails.Add(breakObject);
                }

                return Ok(breakDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<TimeSpan?> CalculateProductiveHoursForBreak(int userId, DateTime startBreakTime, DateTime? finishBreakTime)
        {
            if (finishBreakTime == null)
            {
                return null;
            }

            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime >= startBreakTime && a.CheckInTime <= finishBreakTime)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime >= startBreakTime && c.ClockOutTime <= finishBreakTime)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            var userStartBreakTimes = await _context.Breaks
                .Where(b => b.UserId == userId && b.RequestTime >= startBreakTime && b.RequestTime <= finishBreakTime)
                .Select(b => b.RequestTime)
                .ToListAsync();

            var userFinishBreakTimes = await _context.FinishBreaks
                .Where(f => f.UserId == userId && f.FinishTime >= startBreakTime && f.FinishTime <= finishBreakTime)
                .Select(f => f.FinishTime)
                .ToListAsync();

            var totalTime = TimeSpan.Zero;

            // Add productive hours for check-in and clock-out
            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                var clockInTime = userClockInTimes[i];
                var clockOutTime = i < userClockOutTimes.Count ? userClockOutTimes[i] : default;

                if (clockOutTime != default)
                {
                    totalTime += clockOutTime - clockInTime;
                }
            }

            // Subtract break durations
            for (int i = 0; i < userStartBreakTimes.Count; i++)
            {
                var startBreakTimeWithinRange = userStartBreakTimes[i];
                var finishBreakTimeWithinRange = userFinishBreakTimes.ElementAtOrDefault(i);

                if (finishBreakTimeWithinRange != default)
                {
                    totalTime -= finishBreakTimeWithinRange - startBreakTimeWithinRange;
                }
            }

            return totalTime.Duration();
        }


        [Route("DataByUserId")]
        [HttpGet]
        public async Task<ActionResult<int>> DataByUserId(int userId)
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

            var allStartBreakTime = await _context.Breaks
                .Where(c => c.UserId == userId)
                .Select(c => c.RequestTime)
                .ToListAsync();

            var allFinishBreakTime = await _context.FinishBreaks
                .Where(c => c.UserId == userId)
                .Select(c => c.FinishTime)
                .ToListAsync();

            var clockInTimes = allClockInTimes
                .Where(c => c != null)
                .ToList();

            var groupedByDate = clockInTimes
                .GroupBy(checkInTime => checkInTime.Date);

            var result = new List<object>();

            foreach (var group in groupedByDate)
            {
                var date = group.Key;

                var clockInTimesForDate = group.ToList();
                var startBreakTimes = allStartBreakTime
                    .Where(s => s.Date == date)
                    .ToList();

                var finishBreakTimes = allFinishBreakTime
                    .Where(f => f.Date == date)
                    .ToList();

                // Filter clock-out times based on the date
                var clockOutTimesForDate = allClockOutTimes
                    .Where(c => c.Date == date)
                    .ToList();

                var totalProductiveHoursResult = await CalculateProductiveHours(userId, date);
                if (totalProductiveHoursResult.Result is OkObjectResult okResult)
                {
                    var totalProductiveHours = (TimeSpan)okResult.Value;

                    var totalHoursResult = await CalculateTotalHours(userId, date);
                    if (totalHoursResult.Result is OkObjectResult totalHoursOkResult)
                    {
                        var totalHours = (TimeSpan)totalHoursOkResult.Value;

                        // Calculate break durations
                        var breakDurations = new List<TimeSpan>();
                        for (int i = 0; i < startBreakTimes.Count; i++)
                        {
                            if (i < finishBreakTimes.Count)
                            {
                                var breakStart = startBreakTimes[i];
                                var breakFinish = finishBreakTimes[i];
                                var breakDuration = breakFinish - breakStart;
                                breakDurations.Add(breakDuration);
                            }
                        }

                        var resultObject = new
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Date = date,
                            ClockInTimes = clockInTimesForDate,
                            ClockOutTimes = clockOutTimesForDate,
                            StartBreakTimes = startBreakTimes,
                            FinishBreakTimes = finishBreakTimes,
                            BreakDurations = breakDurations,
                            TotalProductiveHours = totalProductiveHours,
                            TotalHours = totalHours,
                        };

                        result.Add(resultObject);
                    }
                }
            }

            return Ok(result);
        }

        private async Task<ActionResult<TimeSpan>> CalculateTotalHours(int userId, DateTime date)
        {
            // Retrieve clock-in times directly from the database for the specific date
            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime.Date == date)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            // Retrieve clock-out times directly from the database for the specific date
            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime.Date == date)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            if (!userClockInTimes.Any())
            {
                return NotFound($"Attendances for user with id {userId} on {date.ToShortDateString()} not found.");
            }

            TimeSpan totalTime = TimeSpan.Zero;

            for (int i = 0; i < userClockInTimes.Count; i++)
            {
                var clockinTime = userClockInTimes[i];
                var clockoutTime = i < userClockOutTimes.Count ? userClockOutTimes[i] : default;

                if (clockoutTime != default)
                {
                    var timeDiff = clockoutTime - clockinTime;
                    totalTime += timeDiff;
                }
                else
                {
                    Console.WriteLine($"Clock-out time not found for user ID: {userId}, Attendance index: {i}");
                }
            }

            TimeSpan absoluteTime = totalTime.Duration();

            return Ok(absoluteTime);
        }

        private async Task<ActionResult<TimeSpan>> CalculateProductiveHours(int userId, DateTime date)
        {
            var userClockInTimes = await _context.Attenants
                .Where(a => a.UserId == userId && a.CheckInTime.Date == date)
                .Select(a => a.CheckInTime)
                .ToListAsync();

            var userClockOutTimes = await _context.ClockOuts
                .Where(c => c.UserId == userId && c.ClockOutTime.Date == date)
                .Select(c => c.ClockOutTime)
                .ToListAsync();

            var userStartBreakTimes = await _context.Breaks
                .Where(s => s.UserId == userId && s.RequestTime.Date == date)
                .Select(s => s.RequestTime)
                .ToListAsync();

            var userFinishBreakTimes = await _context.FinishBreaks
                .Where(f => f.UserId == userId && f.FinishTime.Date == date)
                .Select(f => f.FinishTime)
                .ToListAsync();

            if (!userClockInTimes.Any() || !userClockOutTimes.Any())
            {
                return NotFound($"Clock-in or Clock-out times for user with id {userId} on {date.ToShortDateString()} not found.");
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


