using Microsoft.AspNetCore.Mvc;
using GymMembershipWebApp.Models;
using GymMembershipWebApp.Services;
using System.Collections.Generic;

namespace GymMembershipWebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberManager _memberManager;
        private readonly AttendanceService _attendanceService;


        public MemberController(MemberManager memberManager, AttendanceService attendanceService)
        {
            _memberManager      = memberManager;
            _attendanceService  = attendanceService;

        }

      // Displays the list of all members
        public IActionResult Index()
        {
            var members = _memberManager.GetAllMembers();
            return View(members);
        }

       // Shows create member form
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Adds a new member if data is valid
        public IActionResult Create(Member member)
        {
            if (!ModelState.IsValid)
                return View(member);

            _memberManager.AddMember(member);
            return RedirectToAction(nameof(Index));
        }

         // Displays Edit form 
        public IActionResult Edit(int id)
        {
            var member = _memberManager.GetMemberById(id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         // Updates an existing member
        public IActionResult Edit(Member member)
        {
            if (!ModelState.IsValid)
                return View(member);

            _memberManager.UpdateMember(member);
            return RedirectToAction(nameof(Index));
        }

       // Displays the Delete confirmation page
        public IActionResult Delete(int id)
        {
            var member = _memberManager.GetMemberById(id);
            if (member == null) return NotFound();
            return View(member);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _memberManager.DeleteMember(id);
            return RedirectToAction(nameof(Index));
        }

        // Displays detailed member info
        public IActionResult Details(int id)
        {
            var member = _memberManager.GetMemberById(id);
            if (member == null) return NotFound();
            return View(member);
        }
        public IActionResult Attendance(int id)
        {
            var member = _memberManager.GetMemberById(id);
            if (member == null) return NotFound();

            ViewBag.Member     = member;
            var attendanceList = _attendanceService.GetAttendance(id);
            return View(attendanceList);
        }
    }
}
