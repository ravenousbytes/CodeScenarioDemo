using AdministrationTool.Data.Models;
using AdministrationTool.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdministrationTool.Web.Models
{
    public class UserViewModel : User, IValidatableObject
    {
        //Ignore these properties for database
        [NotMapped]
        [Required]
        public Guid ManagerId { get; set; }
        [NotMapped]
        private static IEnumerable<User> _Users { get; set; } = new List<User>();
        [NotMapped]
        public IList<SelectListItem> Managers { get; set; } = new List<SelectListItem>();
        [NotMapped]
        private IEnumerable<User> _DirectReports { get; set; } = new List<User>();
        [NotMapped]
        public IEnumerable<UserViewModel> DirectReports { get
            {
                var list = new List<UserViewModel>();
                foreach (var user in _DirectReports)
                    list.Add(new UserViewModel(user));
                return list;
            }
        }

        public UserViewModel()
            : base()
        {

        }

        public UserViewModel(IUserData db)
        {
            Initialize(db);
        }

        public UserViewModel(User user)
        {
            Id = user.Id;
            PrincipalName = user.PrincipalName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            TelephoneNumber = user.TelephoneNumber;
            Enabled = user.Enabled;
            Title = user.Title;
            Manager = user.Manager;
            ManagerId = user.Manager.Id;
        }

        public UserViewModel(User user, IUserData db)
            : this(user)
        {
            Initialize(db);
        }

        public void Initialize(IUserData db)
        {
            LoadUsers(db);
            LoadManagers();
            LoadEmployees(db);
        }

        private void LoadUsers(IUserData db)
        {
            if(_Users == null || _Users.Count() <= 0)
                _Users = db.GetAll();
        }

        private void LoadManagers()
        {
            Managers = _Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.LastName}, {u.FirstName} ({u.Title})"
            }).ToList();
        }

        private void LoadEmployees(IUserData db)
        {
            _DirectReports = _Users.Where(u => u.Manager.Id == Id).Where(u => u.Id != Id);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (_Users != null && _Users.Where(u => u.PrincipalName.Trim().Equals(PrincipalName.Trim(), StringComparison.OrdinalIgnoreCase)).Where(u => u.Id != Id).Count() > 0)
                yield return new ValidationResult("Principal name is used.");
        }
    }
}