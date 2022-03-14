using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public partial class Issue
    {
        [Key]
        public int Id { get; set; }
        [Column("Issue_Title")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssueTitle { get; set; } = null!;
        [Column("Issue_Description")]
        [Unicode(false)]
        public string IssueDescription { get; set; } = null!;
        [Column("Issue_Type")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssueType { get; set; } = null!;
        [Column("Issue_Priority")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssuePriority { get; set; } = null!;
        [Column("Issue_CreatedBy")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssueCreatedBy { get; set; } = null!;
        [Column("Issue_CreatedOn", TypeName = "datetime")]
        public DateTime IssueCreatedOn { get; set; } = DateTime.Now;
        [Column("Issue_ClosedBy")]
        [StringLength(50)]
        [Unicode(false)]
        public string? IssueClosedBy { get; set; } = null!;
        [Column("Issue_ClosedOn", TypeName = "datetime")]
        public DateTime? IssueClosedOn { get; set; }
        [Column("Issue_ResolutionSummary")]
        [Unicode(false)]
        public string? IssueResolutionSummary { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Project { get; set; } = null!;

        public virtual UserProfile? IssueClosedByNavigation { get; set; } 
        public virtual UserProfile? IssueCreatedByNavigation { get; set; } 
        public virtual IssuePriority? IssuePriorityNavigation { get; set; } 
        public virtual IssueStatus? IssueTypeNavigation { get; set; } 
        public virtual Project? ProjectNavigation { get; set; } 
    }
}
