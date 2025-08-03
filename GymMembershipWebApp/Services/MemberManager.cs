#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GymMembershipWebApp.Models;

namespace GymMembershipWebApp.Services
{
    public class MemberManager
    {
        private readonly string _filePath;

        public MemberManager()
        {
            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            Directory.CreateDirectory(dataDir);
            _filePath = Path.Combine(dataDir, "members.txt");
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, string.Empty);
        }

        public List<string> GetAttendanceForMember(int memberId) => new()
        {
            "2025-04-01: Checked In",
            "2025-04-03: Checked In",
            "2025-04-06: Checked In"
        };

        public List<Member> GetAllMembers()
        {
            var members = new List<Member>();
            foreach (var line in File.ReadAllLines(_filePath).Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var parts = line.Contains("|") ? line.Split('|') : line.Split(',');
                Member? m = null;

                
                if (parts.Length == 7
                    && int.TryParse(parts[0], out var id)
                    && bool.TryParse(parts[5], out var isActive)
                    && DateTime.TryParse(parts[6], out var joinDate1))
                {
                    m = new Member
                    {
                        MemberId      = id,
                        Name          = parts[1],
                        Email         = parts[2],
                        Phone         = parts[3],
                        MembershipType= parts[4],
                        IsActive      = isActive,
                        JoinDate      = joinDate1,
                        RenewalDate   = joinDate1.AddMonths(1)
                    };
                }
                
                else if (parts.Length == 6
                         && int.TryParse(parts[0], out var id2)
                         && bool.TryParse(parts[5], out var isActive2))
                {
                    var joinDate2 = DateTime.Now; 
                    m = new Member
                    {
                        MemberId      = id2,
                        Name          = parts[1],
                        Email         = parts[2],
                        Phone         = parts[3],
                        MembershipType= parts[4],
                        IsActive      = isActive2,
                        JoinDate      = joinDate2,
                        RenewalDate   = joinDate2.AddMonths(1)
                    };
                }

                if (m != null)
                    members.Add(m);
            }
            return members;
        }

        public Member? GetMemberById(int id)
            => GetAllMembers().FirstOrDefault(m => m.MemberId == id);

        public void AddMember(Member newMember)
        {
            var list = GetAllMembers();
            newMember.MemberId    = list.Any() ? list.Max(m => m.MemberId) + 1 : 1;
            newMember.JoinDate    = DateTime.Now;
            newMember.RenewalDate = newMember.JoinDate.AddMonths(1);
            newMember.IsActive    = true;
            list.Add(newMember);
            SaveAllMembers(list);
        }

        public void UpdateMember(Member updated)
        {
            var list = GetAllMembers();
            var idx  = list.FindIndex(m => m.MemberId == updated.MemberId);
            if (idx >= 0)
            {
                list[idx] = updated;
                SaveAllMembers(list);
            }
        }

        public void DeleteMember(int id)
        {
            var list = GetAllMembers();
            var item = list.FirstOrDefault(m => m.MemberId == id);
            if (item != null)
            {
                list.Remove(item);
                SaveAllMembers(list);
            }
        }

        private void SaveAllMembers(List<Member> members)
        {
            var lines = members.Select(m =>
                $"{m.MemberId}|{m.Name}|{m.Email}|{m.Phone}|{m.MembershipType}|{m.IsActive}|{m.JoinDate:O}");
            File.WriteAllLines(_filePath, lines);
        }

        public void RenewSubscriptions()
        {
            var list    = GetAllMembers();
            var changed = false;
            foreach (var m in list)
            {
                if (m.IsActive && m.RenewalDate <= DateTime.Now)
                {
                    m.RenewalDate = m.RenewalDate.AddMonths(1);
                    changed = true;
                }
            }
            if (changed) SaveAllMembers(list);
        }
    }
}
