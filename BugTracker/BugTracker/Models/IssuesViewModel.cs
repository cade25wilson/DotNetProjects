using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BugTracker.Models
{
    public class IssuesViewModel
    {
        public List<Issue> Issues { get; set; }
        public SelectList Projects { get; set; }  
        public SelectList IssueTypes { get; set; }
        public SelectList IssueStatuses { get; set; }
        public SelectList IssueCreators { get; set; }
        public SelectList IssueClosers { get; set; }
        public string ProjectIssue { get; set; }
        public string IssueType { get; set; }
        public string IssueStatus { get; set; }
        public string IssueCreator { get; set; }
        public string IssueCloser { get; set; }
        public string SearchString { get; set; }
        public int OpenIssues { get; set; }
        public int  ClosedIssues { get; set; }
    }
}
