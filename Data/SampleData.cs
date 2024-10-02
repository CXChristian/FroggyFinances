using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_transactions.Models;

namespace expense_transactions.Data
{
    public class SampleData
    {

        public static List<User> GetUsers()
        {
            List<User> patients = new List<User>() {
                new User() {
                    Id = 1,
                    Email="conrad@email.com",
                    Password="password",
                    Role="Admin"
                },
                new User() {
                    Id = 2,
                    Email="mika@email.com",
                    Password="password",
                    Role="Admin"
                },
                new User() {
                    Id = 3,
                    Email="user@email.com",
                    Password="password",
                    Role="User"
                },
            };
            return patients;
        }
    }
}