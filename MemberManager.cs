using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MemberManager
{
    private const string FilePath = "members.txt";

    /// <summary>
    /// Loads members from the text file, creating it if necessary.
    /// </summary>
    public List<Member> LoadMembers()
    {
        if (!File.Exists(FilePath))
            File.Create(FilePath).Close();

        return File.ReadAllLines(FilePath)
                   .Where(line => !string.IsNullOrWhiteSpace(line))
                   .Select(Member.FromString)
                   .ToList();
    }

    /// <summary>
    /// Saves the list of members back to the text file.
    /// </summary>
    public void SaveMembers(List<Member> members)
    {
        File.WriteAllLines(FilePath, members.Select(m => m.ToString()));
    }

    /// <summary>
    /// Registers a new member, auto-incrementing the MemberId.
    /// </summary>
    public void RegisterNewMember()
    {
        var members = LoadMembers();

        Console.WriteLine("\n-- Register New Member --");
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Email: ");
        var email = Console.ReadLine() ?? string.Empty;
        Console.Write("Phone: ");
        var phone = Console.ReadLine() ?? string.Empty;
        Console.Write("Membership Tier (Basic/Premium/VIP): ");
        var tier = Console.ReadLine() ?? string.Empty;

        int newId = members.Count > 0 ? members.Max(m => m.MemberId) + 1 : 1;

        var newMember = new Member
        {
            MemberId = newId,
            Name = name,
            Email = email,
            Phone = phone,
            MembershipTier = tier,
            IsActive = true
        };

        members.Add(newMember);
        SaveMembers(members);

        Console.WriteLine("\nRegistration successful! Member ID: {0}\n", newMember.MemberId);
    }

    /// <summary>
    /// Updates an existing member's membership tier.
    /// </summary>
    public void UpdateMembership()
    {
        var members = LoadMembers();

        Console.WriteLine("\n-- Update Membership Plan --");
        Console.Write("Enter Member ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.\n");
            return;
        }

        var member = members.FirstOrDefault(m => m.MemberId == id);
        if (member == null)
        {
            Console.WriteLine("Member not found.\n");
            return;
        }

        Console.Write("New Membership Tier (Basic/Premium/VIP): ");
        var newTier = Console.ReadLine() ?? member.MembershipTier;

        member.MembershipTier = newTier;
        SaveMembers(members);

        Console.WriteLine("\nMembership updated successfully for Member ID: {0}\n", member.MemberId);
    }

    /// <summary>
    /// Validates gym access by MemberId.
    /// </summary>
    public void ValidateGymAccess()
    {
        var members = LoadMembers();

        Console.WriteLine("\n-- Gym Access Validation --");
        Console.Write("Enter Member ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format. Access Denied.\n");
            return;
        }

        var member = members.FirstOrDefault(m => m.MemberId == id);
        if (member == null)
        {
            Console.WriteLine("Access Denied: Member not found.\n");
            return;
        }

        if (!member.IsActive)
        {
            Console.WriteLine("Membership inactive. Please renew.\n");
            return;
        }

        Console.WriteLine($"Access Granted! Welcome, {member.Name}.\n");
    }

    /// <summary>
    /// Allows an admin to update member contact details.
    /// </summary>
    public void AdminUpdateMember()
    {
        var members = LoadMembers();

        Console.WriteLine("\n-- Admin Update Member --");
        Console.Write("Enter Member ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.\n");
            return;
        }

        var member = members.FirstOrDefault(m => m.MemberId == id);
        if (member == null)
        {
            Console.WriteLine("Member not found.\n");
            return;
        }

        Console.WriteLine($"Editing Member: {member.Name} (ID: {member.MemberId})");
        Console.Write("New Phone (leave blank to keep current): ");
        var phone = Console.ReadLine();
        Console.Write("New Email (leave blank to keep current): ");
        var email = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(phone))
            member.Phone = phone;
        if (!string.IsNullOrWhiteSpace(email))
            member.Email = email;

        SaveMembers(members);

        Console.WriteLine("\nMember info updated successfully!\n");
    }

    /// <summary>
    /// Generates a simple report of membership statistics.
    /// </summary>
    public void GenerateReports()
    {
        var members = LoadMembers();

        Console.WriteLine("\n-- Membership Report --");
        Console.WriteLine($"Total Members: {members.Count}");
        Console.WriteLine($"Active Members: {members.Count(m => m.IsActive)}");
        Console.WriteLine($"Inactive Members: {members.Count(m => !m.IsActive)}");

        var tierGroups = members.GroupBy(m => m.MembershipTier);
        foreach (var group in tierGroups)
        {
            Console.WriteLine($"{group.Key}: {group.Count()} members");
        }
    }
}
