using System;

namespace GymMembershipWebApp.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Summary { get; set; }
    }
}
