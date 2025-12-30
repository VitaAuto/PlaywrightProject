using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaywrightProject.API.Models;

namespace PlaywrightProject.API.TestData
{
    public static class UserTestData
    {
        public static User GetValidUser()
        {
            return new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = $"ivan{Guid.NewGuid()}@mail.com",
                IsActive = true
            };
        }
    }
}
