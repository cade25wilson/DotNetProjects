using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    [Table("IssuePriority")]
    [Index(nameof(IssuePriorityType), Name = "AK_IssuePriority_Issue_Priority_Type", IsUnique = true)]
    public partial class IssuePriority
    {
        public IssuePriority()
        {
            Issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Issue_Priority_Type")]
        [StringLength(50)]
        [Unicode(false)]
        public string IssuePriorityType { get; set; } = null!;

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
