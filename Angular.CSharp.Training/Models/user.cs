using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Models
{
	public class User
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } // Store hashed passwords

        [Required]
        public string Role { get; set; } // Optional: User roles (e.g., Admin, User)
    }
}