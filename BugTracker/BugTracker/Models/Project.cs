using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectAccesses = new HashSet<ProjectAccess>();
        }

        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string Id { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(10)]
        public string Description { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        [InverseProperty(nameof(ProjectAccess.Project))]
        public virtual ICollection<ProjectAccess> ProjectAccesses { get; set; }
    }
}
