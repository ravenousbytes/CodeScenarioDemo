using AdministrationTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationTool.Data.Services
{
    public interface IUserData
    {
        IEnumerable<User> GetAll();
        User Get(Guid id);
        void Add(User user);
        void Update(User user); 
    }
}
