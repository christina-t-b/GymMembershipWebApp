using Microsoft.AspNetCore.Mvc;
using GymMembershipWebApp.Services;

namespace GymMembershipWebApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportService _rs;

        public ReportController(ReportService rs)
        {
            _rs = rs; // Assigned ReportService to the private field _rs
        }


        public IActionResult Index()
    {
        ViewBag.MonthlyAttendance = _rs.MonthlyAttendance();   
        ViewBag.TotalMembers = _rs.TotalMembers();             
        ViewBag.ActiveMembers = _rs.ActiveMembers();           

        return View(); // Return to the view to display the data 
    }

        }
}
