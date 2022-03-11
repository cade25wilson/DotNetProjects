using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    [Table("IssueStatus")]
    public partial class IssueStatus
    {
        public IssueStatus()
        {
            Issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Issue_Status_Type")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssueStatusType { get; set; } = null!;

        [InverseProperty(nameof(Issue.IssueResolutionSummaryNavigation))]
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
