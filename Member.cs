public class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string MembershipTier { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public static Member FromString(string line)
    {
        var parts = line.Split(',');
        return new Member
        {
            MemberId = int.Parse(parts[0]),
            Name = parts[1],
            Email = parts[2],
            Phone = parts[3],
            MembershipTier = parts[4],
            IsActive = bool.Parse(parts[5])
        };
    }

    public override string ToString()
    {
        return $"{MemberId},{Name},{Email},{Phone},{MembershipTier},{IsActive}";
    }
}
