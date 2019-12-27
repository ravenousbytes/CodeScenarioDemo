using AdministrationTool.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdministrationTool.Data.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class User
    {
        [Required]
        [Key]
        public Guid Id { get; internal set; }

        /// <summary>
        /// The unique system user name.
        /// </summary>
        [Required]
        [Display(Name = "Principal name")]
        [MaxLength(64)]
        public string PrincipalName { get; set; }

        [Required]
        [Display(Name = "First name")]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter valid email address.")]
        [Display(Name = "E-mail")]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}", ErrorMessage = "Please enter phone as (xxx)xxx-xxxx")]
        [Display(Name = "Telephone number")]
        [MaxLength(32)]
        public string TelephoneNumber { get; set; }

        public bool Enabled { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        public Guid ManagerId { get; set; }

        private User _manager;
        [ForeignKey("ManagerId")]
        public User Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
                if(value != null) ManagerId = value.Id;
            }
        }
    }
}
