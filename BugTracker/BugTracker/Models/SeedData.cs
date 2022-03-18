namespace BugTracker.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BugTrackerDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BugTrackerDbContext>>()))
            {
                if (context.IssueStatuses.Any())
                {
                    return;
                }

                context.IssueStatuses.AddRange(
                    new IssueStatus
                    {
                        IssueStatusType = "Abandoned"
                    },

                    new IssueStatus
                    {
                        IssueStatusType = "Active"
                    },

                    new IssueStatus
                    {
                        IssueStatusType = "Completed"
                    },

                    new IssueStatus
                    {
                        IssueStatusType = "Under Review"
                    }
                );
                context.SaveChanges();

                if (context.IssuePriorities.Any())
                {
                    return;
                }

                context.IssuePriorities.AddRange(
                    new IssuePriority
                    {
                        IssuePriorityType = "Emergency"
                    },

                    new IssuePriority
                    {
                        IssuePriorityType = "High Priority"
                    },

                    new IssuePriority
                    {
                        IssuePriorityType = "Low Priority"
                    },

                    new IssuePriority
                    {
                        IssuePriorityType = "Medium Priority"
                    },

                    new IssuePriority
                    {
                        IssuePriorityType = "Urgent"
                    }

                );
            }
        }
    }
}
