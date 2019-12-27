using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationTool.Web.Models
{
    public class UsersViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public int UsersPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Users.Count() / (double)UsersPerPage));
        }
        public IEnumerable<UserViewModel> PaginatedUsers()
        {
            int start = (CurrentPage - 1) * UsersPerPage;
            return Users.OrderBy(b => b.Id).Skip(start).Take(UsersPerPage);
        }
    }
}