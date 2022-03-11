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
        public int IssueType { get; set; }
        [Column("Issue_Priority")]
        public int IssuePriority { get; set; }
        [Column("Issue_CreatedBy")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssueCreatedBy { get; set; } = null!;
        [Column("Issue_CreatedOn", TypeName = "datetime")]
        public DateTime IssueCreatedOn { get; set; }
        [Column("Issue_ClosedBy")]
        [StringLength(50)]
        [Unicode(false)]
        public string? IssueClosedBy { get; set; }
        [Column("Issue_ClosedOn", TypeName = "datetime")]
        public DateTime? IssueClosedOn { get; set; }
        [Column("Issue_ResolutionSummary")]
        public int IssueResolutionSummary { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Project { get; set; } = null!;

        public virtual UserProfile? IssueClosedByNavigation { get; set; }
        public virtual UserProfile IssueCreatedByNavigation { get; set; } = null!;
        [ForeignKey(nameof(IssueResolutionSummary))]
        [InverseProperty(nameof(IssueStatus.Issues))]
        public virtual IssueStatus IssueResolutionSummaryNavigation { get; set; } = null!;
        [ForeignKey(nameof(IssueType))]
        [InverseProperty("Issues")]
        public virtual IssuePriority IssueTypeNavigation { get; set; } = null!;
        public virtual Project ProjectNavigation { get; set; } = null!;
    }
}
