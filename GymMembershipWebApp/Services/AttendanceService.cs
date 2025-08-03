using System.Collections.Generic;
using GymMembershipWebApp.Services;

namespace GymMembershipWebApp.Services
{
    public class AttendanceService
    {
        private readonly MemberManager _mm;
        public AttendanceService(MemberManager mm) => _mm = mm; // Constructor formating MemberManager

        public List<string> GetAttendance(int memberId)// Gets attendance data
            => _mm.GetAttendanceForMember(memberId);// Calls MemberManager to get attendance
    }
}
