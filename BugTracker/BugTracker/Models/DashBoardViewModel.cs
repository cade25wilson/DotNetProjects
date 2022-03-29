namespace BugTracker.Models
{
    public class DashBoardViewModel
    {
        public int OpenIssues { get; set; }
        public int ClosedIssues { get; set; }
        public int AbandonedIssues { get; set; }
        public int TotalIssues { get; set; }
        public int LowPriority { get; set; }
        public int MediumPriority { get; set; }
        public int HighPriority { get; set; }
        public int UrgentPriority { get; set; }
        public int EmergencyPriority { get; set; }
        public int NotStarted { get; set; }


    }
}
