using AdministrationTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationTool.Data.Services
{
    public class TestUserData : IUserData
    {
        private List<User> users;

        public TestUserData()
        {
            var owner = new User { Id = new Guid("4f7327d3-5278-4497-ac96-9fcd360b3c5a"), PrincipalName = "jowner", FirstName = "James", LastName = "Owner", Email = "jowner@company.com", TelephoneNumber = "3214321111", Manager = null, Title = "Owner", Enabled = true };
            owner.Manager = owner;
            var cio = new User { Id = new Guid("69a31a71-9c94-4e6a-9161-6f49bc98786d"), PrincipalName = "sinformation", FirstName = "Sumit", LastName = "Information", Email = "sinformation@company.com", TelephoneNumber = "3214323333", Manager = owner, Title = "CIO", Enabled = true };
            var webmanager = new User { Id = new Guid("6b015f71-60e8-4c34-8b0d-95cbb62b849b"), PrincipalName = "dweb", FirstName = "David", LastName = "Web", Email = "dweb@company.com", TelephoneNumber = "3214324444", Manager = cio, Title = "Director of Development", Enabled = true };
            var systemmanager = new User { Id = new Guid("31c24468-bd7a-4388-a4bd-c2096cf6057d"), PrincipalName = "lsystems", FirstName = "Lauren", LastName = "Systems", Email = "lsystems@company.com", TelephoneNumber = "3214325555", Manager = cio, Title = "Director of Technology", Enabled = true };
            var operationsmanager = new User { Id = new Guid("04afe5fd-d85f-4903-9976-661ba15ca538"), PrincipalName = "soperations", FirstName = "Saunak", LastName = "Operations", Email = "soperations@company.com", TelephoneNumber = "3214326666", Manager = cio, Title = "Director of Operations", Enabled = true };
            users = new List<User>
            {
                owner,
                new User { Id = new Guid("b7a5405b-5771-409e-8587-1e0323a6100c"), PrincipalName = "mexecutive", FirstName = "Michael", LastName = "Executive", Email = "mexecutive@company.com", TelephoneNumber="3214322222", Manager = owner, Title = "CEO", Enabled = true },
                cio,
                webmanager,
                systemmanager,
                operationsmanager,
                new User { Id = new Guid("b4e24687-9c1a-4b53-8287-ecec7752e1ca"), PrincipalName = "atechone", FirstName = "Anthony", LastName = "Techone", Email = "atechone@company.com", TelephoneNumber = "3214327777", Manager = operationsmanager, Title = "Network Technician", Enabled = true},
                new User { Id = new Guid("4629f2ce-239f-40ea-9167-28d27c29a163"), PrincipalName = "stechtwo", FirstName = "Scott", LastName = "Techtwo", Email = "stechone@company.com", TelephoneNumber = "3214328888", Manager = operationsmanager, Title = "Desktop Support Specialist", Enabled = true},
                new User { Id = new Guid("82ce462b-5240-431e-8374-ec499a6bbf6b"), PrincipalName = "aanalystone", FirstName = "Alice", LastName = "Analystone", Email = "aanalystone@company.com", TelephoneNumber = "3214329999", Manager = systemmanager, Title = "Business Analyst", Enabled = true},
                new User { Id = new Guid("83f637d4-fc19-4cc6-bc6e-81c987bc859a"), PrincipalName = "nanalysttwo", FirstName = "Nat", LastName = "Analysttwo", Email = "nanalysttwo@company.com", TelephoneNumber = "3214320010", Manager = systemmanager, Title = "Business Analyst", Enabled = true},
                new User { Id = new Guid("28b8869a-89d7-4808-b276-eb92bce0df59"), PrincipalName = "sanalystthree", FirstName = "Susan", LastName = "Analystthree", Email = "sanalystthree@company.com", TelephoneNumber = "3214320011", Manager = systemmanager, Title = "Business Analyst", Enabled = true},
                new User { Id = new Guid("8a2809b0-370c-4fb5-9e33-971471eb9f52"), PrincipalName = "cdevone", FirstName = "Clarissa", LastName = "Developerone", Email = "cdevone@company.com", TelephoneNumber = "3214320012", Manager = webmanager, Title = "Web Developer", Enabled = true},
                new User { Id = new Guid("ac8bdb92-73b0-4570-987d-5d5322c43d77"), PrincipalName = "auxuiexpert", FirstName = "Ashley", LastName = "Uxuiexpert", Email = "auxuiexpert@company.com", TelephoneNumber = "3214320013", Manager = webmanager, Title = "UX/UI Analyst", Enabled = true},
                new User { Id = new Guid("693e2258-bbdc-4a54-b36c-2da4e3401fb9"), PrincipalName = "jdevtwo", FirstName = "Jeffrey", LastName = "Developertwo", Email = "jdevtwo@company.com", TelephoneNumber = "3214320014", Manager = webmanager, Title = "Web Developer", Enabled = true},
                new User { Id = new Guid("63d65fb6-6938-4a0f-ae35-ff5054be30c8"), PrincipalName = "jdevthree", FirstName = "JR", LastName = "Developerthree", Email = "jdevthree@company.com", TelephoneNumber = "3214320015", Manager = webmanager, Title = "Web Developer", Enabled = true},
                new User { Id = new Guid("c419eacc-882d-4c66-a0bc-efdc0b7a8259"), PrincipalName = "jdevfour", FirstName = "James", LastName = "Developerfour", Email = "jdevfour@company.com", TelephoneNumber = "3214320016", Manager = webmanager, Title = "Web Developer", Enabled = true},
                new User { Id = new Guid("c419eacc-882d-4c66-a0bc-efdc0b7a8259"), PrincipalName = "jdevfive", FirstName = "Connie", LastName = "Developerfive", Email = "cdevfour@company.com", TelephoneNumber = "3214320016", Manager = webmanager, Title = "Web Developer", Enabled = false}
                //new User { Id = new Guid(), PrincipalName = "", FirstName = "", LastName = "", Email = "", TelephoneNumber = "", Manager = null, Title = "", Enabled = true}
            };
        }

        public void Add(User user)
        {
            user.Id = new Guid();
            users.Add(user);
        }

        public void Delete(User user)
        {
            users.RemoveAll(u => u.Id == user.Id);
        }

        public User Get(Guid id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string principalName)
        {
            return users.FirstOrDefault(u => u.PrincipalName == principalName);
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return users;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return true;
        }

        public void Update(User usermod)
        {
            var user = Get(usermod.Id);
            if(user != null)
            {
                user.PrincipalName = usermod.PrincipalName;
                user.FirstName = usermod.FirstName;
                user.LastName = usermod.LastName;
                user.Email = usermod.Email;
                user.TelephoneNumber = usermod.TelephoneNumber;
                user.Enabled = usermod.Enabled;
                user.Title = usermod.Title;
                user.Manager = usermod.Manager;
            }
        }
    }
}
