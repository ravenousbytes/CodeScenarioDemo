using AdministrationTool.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdministrationTool.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Principal name")]
        public string PrincipalName { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter valid email address.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}", ErrorMessage = "Please enter phone as (xxx)xxx-xxxx")]
        [Display(Name = "Telephone number")]
        public string TelephoneNumber { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public string Title { get; set; }

        public User Manager { get; set; }
    }
}
