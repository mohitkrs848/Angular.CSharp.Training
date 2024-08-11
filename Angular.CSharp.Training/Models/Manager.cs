using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Models
{
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ManagerName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}