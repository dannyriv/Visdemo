using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Visportfolio.Models
{
    public class VisUser
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Middle Initial (Optional)")]
        public string MiddleInitial { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter User Password")]
        public string UserPassword { get; set; }
    }
}
