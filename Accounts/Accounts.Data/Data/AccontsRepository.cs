using Accounts.Data.Interfaces;
using Accounts.Data.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Data
{
    public class AccontsRepository:IAccountsRepository
    {
        private readonly AccountsContext _context;
        private readonly ILogger<AccontsRepository> _accountReposLogger;
        /// <summary>
        /// Constructor for Account Repository
        /// </summary>
        /// <param name="accountsContext"></param>
        /// <param name="accountReposLogger"></param>
        public AccontsRepository(AccountsContext accountsContext, ILogger<AccontsRepository> accountReposLogger)
        { 
            _context = accountsContext;
            _accountReposLogger=accountReposLogger;
        }

        /// <summary>
        /// Add account transaction to Database for user
        /// </summary>
        /// <param name="acct"></param>
        /// <returns></returns>
        public Guid AddTrans(Transactions acct)
        {
            Guid guid;
            try
            {
                acct.TransactionDate = DateTime.UtcNow;
                _context.Transactions.Add(acct);
                _context.SaveChanges();
                
                guid = acct.TransactionID;

            }
            catch (Exception ex)
            {
                guid = Guid.Empty;
                _accountReposLogger.LogError(String.Format("Exception  {0}"),ex.Message);
            }
            return guid;
        }

        /// <summary>
        /// Reverse a transaction by passing added transaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ReverseTrans(Guid id)
        {
            bool isSuccess = false;

            try {
                Transactions entitytodelete = _context.Transactions.Where(x => x.TransactionID == id).First();
                if (entitytodelete != null)
                {
                    _context.Transactions.Remove(entitytodelete);
                    _context.SaveChanges();
                    isSuccess= true;
                }
            }
            catch (Exception ex) { 
            _accountReposLogger.LogError(String.Format("Exception  {0}"), ex.Message);
                isSuccess = false;
            }
            return isSuccess;
        }



    }
}
