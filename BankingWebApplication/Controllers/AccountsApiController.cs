using BankingWebApplication.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankingWebApplication.Controllers
{
    public class AccountsApiController : ApiController
    {
        private static ConcurrentQueue<DepositViewModel> depositQueue = new ConcurrentQueue<DepositViewModel>();
        private static ConcurrentQueue<WithdrawViewModel> withdrawQueue = new ConcurrentQueue<WithdrawViewModel>();
        private static ConcurrentQueue<TransferFunds> transferFundsQueue = new ConcurrentQueue<TransferFunds>();
        private KirJemBankingEntities db = new KirJemBankingEntities();
 
        [HttpPost]
        /*[Authorize]*/

        [Route("api/AccountsApi/Deposit")]
        public IHttpActionResult Deposit([FromBody] DepositViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            depositQueue.Enqueue(model);
            Console.WriteLine(depositQueue.Count);
            return Ok(new { message = "Deposit in progress. You can check the status later." });
        }


        [HttpPost]
        [Route("api/AccountsApi/Withdraw")]
        /*[Authorize]*/
        public IHttpActionResult Withdraw([FromBody] WithdrawViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            withdrawQueue.Enqueue(model);
            return Ok(new { message = "Withdraw in progress. You can check the status later." });
        }

        [HttpPost]
        [Route("api/AccountsApi/TransferFunds")]
        /*[Authorize]*/
        public IHttpActionResult TransferFunds([FromBody] TransferFunds model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            transferFundsQueue.Enqueue(model);
            return Ok(new { message = "Transfer in progress. You can check the status later." });
        }




        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

       /* // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }*/

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public static ConcurrentQueue<DepositViewModel> GetDepositQueue()
        { 

                return depositQueue;
        }
        public static ConcurrentQueue<WithdrawViewModel> GetWithdrawQueue()
        {

            return withdrawQueue;
        }

        public static ConcurrentQueue<TransferFunds> GetTransferFundsQueue()
        {

            return transferFundsQueue;
        }
    }
} 