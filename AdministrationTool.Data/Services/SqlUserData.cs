using AdministrationTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationTool.Data.Services
{
    public class SqlUserData : IUserData
    {
        private readonly AdministrationDbContext db;

        public SqlUserData(AdministrationDbContext db)
        {
            this.db = db;
        }

        public void Add(User user)
        {
            user.Id = new Guid();
            db.Users.Add(user);
        }

        public void Delete(User user)
        {
            db.Users.Remove(user);
        }

        public User Get(Guid id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string principalName)
        {
            return db.Users.FirstOrDefault(u => u.PrincipalName == principalName);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            //TODO: Implement async properly for SQLUserData
            return db.Users;
        }

        public bool SaveChanges()
        {
            return db.SaveChanges() != 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await db.SaveChangesAsync() == 0;
        }

        public void Update(User user)
        {
            //TODO: Optimistic Concurrency for multi user 
            var entry = db.Entry(user);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
