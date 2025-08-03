using System;

class Program
{
    static void Main(string[] args)
    {
        MemberManager manager = new MemberManager();
        while (true)
        {
            Console.WriteLine("\n=== Gym Membership System ===");
            Console.WriteLine("1. Register New Member");
            Console.WriteLine("2. Update Membership Plan");
            Console.WriteLine("3. Validate Gym Access");
            Console.WriteLine("4. Admin Update Member Info");
            Console.WriteLine("5. Generate Reports");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option (1-6): ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    manager.RegisterNewMember();
                    break;
                case "2":
                    manager.UpdateMembership();
                    break;
                case "3":
                    manager.ValidateGymAccess();
                    break;
                case "4":
                    manager.AdminUpdateMember();
                    break;
                case "5":
                    manager.GenerateReports();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}

