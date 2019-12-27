using System;
using AdministrationTool.Data.Models;
using AdministrationTool.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdministrationTool.Data.Test
{
    [TestClass]
    public class UserDataUnitTest
    {
        //IUserData db = new SqlUserData(new AdministrationDbContext());
        IUserData db = new TestUserData();

        [TestMethod]
        public void AddUser()
        {
            var newuser = new User()
            {
                PrincipalName = "adduser",
                FirstName = "New",
                LastName = "User",
                Email = "newuser@domain.com",
                TelephoneNumber = "3334445555",
                Enabled = true,
                Title = "Sales Rep",
                Manager = null
            };
            db.Add(newuser);
            db.SaveChanges();

            Assert.IsTrue(db.Get(newuser.PrincipalName) != null);
        }

        //TODO: Test GetAll
        //TODO: Test Update
        //TODO: Test Delete
    }
}
