using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowOsc.Domain.Entities;

namespace StackOverflow.data
{

    public class StackOverflowContext : DbContext
    {
        public StackOverflowContext()
            : base(ConnectionString.get())
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions {get; set;}
        public DbSet<Answer> Answers { get; set; } 

    }

    public static class ConnectionString
    {
        public static string get()
       {
            var environment = ConfigurationManager.AppSettings["Environment"];
            return string.Format("name = {0}", environment);
        }
    }
}
