using Accounts.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Data
{
    public class AccountsContext:DbContext
    {

        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        { }

        public DbSet<Transactions> Transactions { get; set; }
    }
}
