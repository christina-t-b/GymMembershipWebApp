using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GymMembershipWebApp.Services
{
  public class ReportService
    {
        private readonly string _attendanceFilePath;
        private readonly string _memberFilePath;

        public ReportService()
        {
            
            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            Directory.CreateDirectory(dataDir);

            
            _attendanceFilePath = Path.Combine(dataDir, "attendance.txt");
            _memberFilePath   = Path.Combine(dataDir, "members.txt");

          
            if (!File.Exists(_attendanceFilePath))
                File.WriteAllText(_attendanceFilePath, string.Empty);
            if (!File.Exists(_memberFilePath))
                File.WriteAllText(_memberFilePath, string.Empty);
        }
        private List<AttendanceRecord> GetAttendanceData()
        {
            var attendanceRecords = new List<AttendanceRecord>();
            foreach (var line in File.ReadLines(_attendanceFilePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var memberId = int.Parse(parts[0]);
                    var attendanceDate = DateTime.Parse(parts[1]);
                    attendanceRecords.Add(new AttendanceRecord
                    {
                        MemberId = memberId,
                        AttendanceDate = attendanceDate
                    });
                }
            }
            return attendanceRecords;
        }

        
        private List<MemberRecord> GetMemberData()
        {
            var memberRecords = new List<MemberRecord>();
            foreach (var line in File.ReadLines(_memberFilePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 7)
                {
                    var memberId = int.Parse(parts[0]);
                    var name = parts[1];
                    var email = parts[2];
                    var phone = parts[3];
                    var membershipType = parts[4];
                    var isActive = bool.Parse(parts[5]);
                    var registrationDate = DateTime.Parse(parts[6]);

                    memberRecords.Add(new MemberRecord
                    {
                        MemberId = memberId,
                        Name = name,
                        Email = email,
                        Phone = phone,
                        MembershipType = membershipType,
                        IsActive = isActive,
                        RegistrationDate = registrationDate
                    });
                }
            }
            return memberRecords;
        }

        // Monthly Attendance Calculation
        public int MonthlyAttendance()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var attendanceData = GetAttendanceData();
            var monthlyAttendance = attendanceData.Count(a => a.AttendanceDate.Month == currentMonth && a.AttendanceDate.Year == currentYear);
            
            return monthlyAttendance;
        }

        // Method to return the total number of members
        public int TotalMembers()
        {
            var memberData = GetMemberData();
            return memberData.Count;
        }

        // Method to return the number of active members
        public int ActiveMembers()
        {
            var memberData = GetMemberData();
            return memberData.Count(m => m.IsActive);
        }
    }

    // Class to hold attendance record
    public class AttendanceRecord
    {
        public int MemberId { get; set; }
        public DateTime AttendanceDate { get; set; }
    }

    // Class to hold member record
    public class MemberRecord
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MembershipType { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
