using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    [Table("UserProfile")]
    [Index(nameof(DisplayName), Name = "AK_UserProfile_DisplayName", IsUnique = true)]
    public partial class UserProfile
    {
        public UserProfile()
        {
            IssueIssueClosedByNavigations = new HashSet<Issue>();
            IssueIssueCreatedByNavigations = new HashSet<Issue>();
            ProjectAccesses = new HashSet<ProjectAccess>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string DisplayName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? EmailAddress { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string AllowEmailNotification { get; set; } = null!;

        public virtual ICollection<Issue> IssueIssueClosedByNavigations { get; set; }
        public virtual ICollection<Issue> IssueIssueCreatedByNavigations { get; set; }
        public virtual ICollection<ProjectAccess> ProjectAccesses { get; set; }
    }
}
