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
        [StringLength(50)]
        [Unicode(false)]
        public string Id { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string ProjectId { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? User { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AccessType { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectAccesses")]
        public virtual Project Project { get; set; } = null!;
        [ForeignKey(nameof(User))]
        [InverseProperty(nameof(UserProfile.ProjectAccesses))]
        public virtual UserProfile? UserNavigation { get; set; }
    }
}
