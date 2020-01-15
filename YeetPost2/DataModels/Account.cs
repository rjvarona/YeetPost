using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeetPost2.DataModels
{
    public class AccountContext : DbContext
    {
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=aspnet-YeetPost2-36F61C10-5290-4BAB-B65E-640EBFC4B720;Integrated Security=True");
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
        public string PasswordHash { get; set; }
    }


}
