using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using GymMembershipWebApp.Services;
using System.IO;
using System.Linq;

namespace GymMembershipWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReportService _rs;
        private readonly IWebHostEnvironment _env;

        public HomeController(ReportService rs, IWebHostEnvironment env)
      {
            _rs = rs;  // format ReportService
            _env = env; // format IWebHostEnvironment
        }

        public IActionResult Dashboard()
        {
            // set file paths for members/attendance data
            var membersPath = Path.Combine(_env.ContentRootPath, "App_Data", "members.txt");
            var attendancePath = Path.Combine(_env.ContentRootPath, "App_Data", "attendance.txt");

            // Read file lines
            var memberLines = System.IO.File.ReadAllLines(membersPath);
            var attendanceLines = System.IO.File.ReadAllLines(attendancePath);

            // Set ViewBag variables for dashboard data
            ViewBag.TotalMembers = memberLines.Length;
            ViewBag.ActiveMembers = memberLines.Count(line => line.Contains("Active"));
            ViewBag.MonthlyAttendance = attendanceLines.Length;

            return View(); // Return to dashboard
        }

        public IActionResult Index()
        {
            // Get data from the ReportService& assign to ViewBag
            ViewBag.MonthlyAttendance = _rs.MonthlyAttendance();
            ViewBag.TotalMembers = _rs.TotalMembers();
            ViewBag.ActiveMembers = _rs.ActiveMembers();
            return View();
        }

         public IActionResult Login()
        {
           return View("~/Views/Account/Login.cshtml"); // Return the login view
        }

    }


}
