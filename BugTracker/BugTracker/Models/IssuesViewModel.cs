using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BugTracker.Models
{
    public class IssuesViewModel
    {
        public List<Issue> Issues { get; set; }
        public SelectList Projects { get; set; }  
        public string ProjectIssue { get; set; }
        public string SearchString { get; set; }
    }
}
