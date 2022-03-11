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
        public int IssueCreatedBy { get; set; }
        [Column("Issue_CreatedOn", TypeName = "datetime")]
        public DateTime IssueCreatedOn { get; set; }
        [Column("Issue_ClosedBy")]
        public int IssueClosedBy { get; set; }
        [Column("Issue_ClosedOn", TypeName = "datetime")]
        public DateTime IssueClosedOn { get; set; }
        [Column("Issue_ResolutionSummary")]
        public int IssueResolutionSummary { get; set; }

        [ForeignKey(nameof(IssueClosedBy))]
        [InverseProperty(nameof(UserProfile.IssueIssueClosedByNavigations))]
        public virtual UserProfile IssueClosedByNavigation { get; set; } = null!;
        [ForeignKey(nameof(IssueCreatedBy))]
        [InverseProperty(nameof(UserProfile.IssueIssueCreatedByNavigations))]
        public virtual UserProfile IssueCreatedByNavigation { get; set; } = null!;
        [ForeignKey(nameof(IssueResolutionSummary))]
        [InverseProperty(nameof(IssueStatus.Issues))]
        public virtual IssueStatus IssueResolutionSummaryNavigation { get; set; } = null!;
        [ForeignKey(nameof(IssueType))]
        [InverseProperty("Issues")]
        public virtual IssuePriority IssueTypeNavigation { get; set; } = null!;
    }
}
