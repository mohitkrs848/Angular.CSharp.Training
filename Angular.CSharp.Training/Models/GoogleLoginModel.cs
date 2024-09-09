using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Models
{
    public class GoogleLoginModel
    {
        [Required]
        public string Token { get; set; }
    }
}