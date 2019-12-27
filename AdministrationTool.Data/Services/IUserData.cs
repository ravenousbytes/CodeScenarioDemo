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
        Task<IEnumerable<User>> GetAllAsync();

        User Get(Guid id);
        //Task<User> GetAsync(Guid id);

        User Get(string principalName);
        //Task<User> GetAsync(string principalName);

        void Add(User user);
        void Update(User user);
        void Delete(User user);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
