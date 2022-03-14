using Accounts.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Interfaces
{
    public interface IAccountsRepository
    {
        public Guid AddTrans(Transactions acct);

        public bool ReverseTrans(Guid id);
    }
}
