using AdministrationTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User Get(Guid id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
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
