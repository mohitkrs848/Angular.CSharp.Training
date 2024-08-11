using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ProjectName { get; set; }

        [Required]
        public int ProjectManagerID { get; set; } // FK

        [Required]
        public string ProjectStatus { get; set; }

        public virtual Manager Manager { get; set; }
    }
}