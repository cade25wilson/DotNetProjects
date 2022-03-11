using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    [Table("ProjectAccess")]
    public partial class ProjectAccess
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? User { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AccessType { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectAccesses")]
        public virtual Project Project { get; set; } = null!;
        public virtual UserProfile? UserNavigation { get; set; }
    }
}
