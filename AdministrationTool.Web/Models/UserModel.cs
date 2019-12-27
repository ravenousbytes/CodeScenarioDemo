using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdministrationTool.Web.Models
{
    public class UserModel
    {
        /// <summary>
        /// Unique id assigned to the user.
        /// </summary>
        [Required]
        public string PrincipalName { get; set; }
        /// <summary>
        /// First name of the user.
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user.
        /// </summary>
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        [Required]
        public bool Enabled { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ManagerPrincipalName { get; set; }
        public string ManagerFullName { get; set; }
    }
}