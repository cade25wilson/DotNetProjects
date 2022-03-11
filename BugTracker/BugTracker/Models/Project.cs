using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    [Index(nameof(Name), Name = "AK_Projects_Name", IsUnique = true)]
    public partial class Project
    {
        public Project()
        {
            Issues = new HashSet<Issue>();
            ProjectAccesses = new HashSet<ProjectAccess>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
        [InverseProperty(nameof(ProjectAccess.Project))]
        public virtual ICollection<ProjectAccess> ProjectAccesses { get; set; }
    }
}
