using Accounts.Data.Interfaces;
using Accounts.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly ILogger<AccountController> _accountLogger;

        /// <summary>
        /// Constructor for accountController and created dependencies for Logger and repository
        /// </summary>
        /// <param name="accountsRepository"></param>
        /// <param name="accountLogger"></param>
        public AccountController(IAccountsRepository accountsRepository, ILogger<AccountController> accountLogger)
        {

            _accountsRepository = accountsRepository;
            _accountLogger = accountLogger;
        }

        /// <summary>
        ///  Call for Adding a transaction to db
        /// </summary>
        /// <param name="Acct"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddTranaction([FromBody] Transactions Acct)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (_accountsRepository.AddTrans(Acct) != Guid.Empty)
                    {
                        _accountLogger.LogInformation("Tranaction added with id " + Acct.TransactionID);
                        return Ok(new { Transactionid = Acct.TransactionID });
                    }
                    else
                    {
                        _accountLogger.LogError("Request not completed");
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                _accountLogger.LogError("exception :-" + ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult ReverseTransaction([FromQuery] string TransactionID)
        {
            Guid tranid;
            if (string.IsNullOrWhiteSpace(TransactionID))
            {
                return BadRequest("Transaction id can not be null");
            }
            bool isguid=Guid.TryParse(TransactionID, out tranid);

            if (!isguid)
            {
                _accountLogger.LogError("Invalid Transaction id");
                return BadRequest("Invalid Transaction id");
            }
            try
            {
                if (_accountsRepository.ReverseTrans(tranid))
                {
                    _accountLogger.LogInformation("Transaction Reversed");
                    return Ok(new { TransactionReversed = true });
                }
                else {
                    _accountLogger.LogInformation("Transaction not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            { 
                _accountLogger.LogInformation("exception:- "+ex.Message);
                return BadRequest("Server Error");
            }
        }
    }
}

