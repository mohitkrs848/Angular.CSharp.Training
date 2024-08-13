using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Angular.CSharp.Training.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpFirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpLastName { get; set; }

        [Required]
        [Range(18, 60)]
        public int EmpAge { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string EmpEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpDesignation { get; set; }

        [Required]
        [Range(10000, 10000000)]
        public decimal EmpSalary { get; set; }

        [Required]
        public string EmpLocation { get; set; }

        [Required]
        public string EmpStatus { get; set; }

        public int? EmpManagerID { get; set; } // Nullable FK

        [Required]
        public string EmpDeptName { get; set; } // FK

        //public virtual Manager Manager { get; set; }
        //public virtual Department Department { get; set; }
    }
}