using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeetPostV1_4.DataModels
{
    public class AccountContext : DbContext
    {
        public DbSet<AspNetUsers> AspNetUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(" Data Source=Users");
        }
    }


    public class Account
    {
        public string username { get; set; }
        public DateTime dateCreated { get; set; }
        public string role { get; set; }
    }

    public class AspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }


}
